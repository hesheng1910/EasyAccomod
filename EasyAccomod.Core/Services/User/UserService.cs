using AGID.Core.Exceptions;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.EF;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly EasyAccDbContext context;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<bool> CheckUserAndRole(long accessId,string role)
        {
            var user = _userManager.Users.Where(x => x.Id == accessId && x.IsConfirm).FirstOrDefault();
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");

            return await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<string> CheckAuthencate(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;
            return user.UserName;
        }
        public async Task<UserViewModel> Authencate(LoginModel model)
        {
            var user = _userManager.Users.Where(x => x.UserName == model.UserName && x.IsConfirm).FirstOrDefault();
            if (user == null) throw new ServiceException("Tài khoản không tồn tại hoặc chưa được confirm");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password,false, true);
            if (!result.Succeeded)
            {
                throw new ServiceException ("Đăng nhập không đúng");
            }
            UserViewModel viewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Address = user.Address,
                IdentityNumber = user.IdentityNumber,
                IsConfirm = user.IsConfirm,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
            return viewModel;
        }
        public async Task<UserViewModel> Delete(long id,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ServiceException("User không tồn tại");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
            {
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Address = user.Address,
                IdentityNumber = user.IdentityNumber,
                IsConfirm = user.IsConfirm,
                Role = roles.FirstOrDefault()
            };
            return userVm;
            }    
               
            return null;
        }

        public async Task<UserViewModel> GetById(long id,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                throw new ServiceException("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Address = user.Address,
                IdentityNumber = user.IdentityNumber,
                IsConfirm = user.IsConfirm,
                Role = roles.FirstOrDefault()
            };
            return userVm;
        }
        public async Task<List<UserViewModel>> GetOwners(long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var users = _userManager.Users.Where(x => x.IsConfirm);
            List<UserViewModel> viewModels = new List<UserViewModel>();
            foreach(var user in users)
            {
                if(await _userManager.IsInRoleAsync(user,CommonConstants.OWNER))
                {
                    var roles = await _userManager.GetRolesAsync(user);

                var userVm = new UserViewModel()
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Address = user.Address,
                    IdentityNumber = user.IdentityNumber,
                    IsConfirm = user.IsConfirm,
                    Role = roles.FirstOrDefault()
                };
                }
              
            }
            return viewModels;
        }
        public async Task<UserViewModel> GetByAccessId(long accessId)
        {
            var user = _userManager.Users.Where(x => x.Id == accessId && x.IsConfirm).FirstOrDefault();
            if (user == null)
            {
                throw new ServiceException("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Address = user.Address,
                IdentityNumber = user.IdentityNumber,
                IsConfirm = user.IsConfirm,
                Role = roles.FirstOrDefault()
            };
            return userVm;
        }
        public async Task<List<UserViewModel>> GetAllUsers(long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false && await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var users = _userManager.Users.ToList();
            var userViewModels = new List<UserViewModel>();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserViewModel userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Address = user.Address,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    IdentityNumber = user.IdentityNumber,
                    IsConfirm = user.IsConfirm,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Role = roles.FirstOrDefault()
                };
                userViewModels.Add(userViewModel);
            }
            return userViewModels;
        }
        public async Task<UserViewModel> Register(RegisterModel model)
        {
            // Kiem tra xem nguoi dung da ton tai chua
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null ) throw new ServiceException("User's already exist");
            // Kiem tra xem email da ton tai chua
            var user2 = await _userManager.FindByEmailAsync(model.Email);
            if (user2 != null) throw new ServiceException("Email's already exist");
            var appUser = new AppUser()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Phone,
                Email = model.Email,
                Address = model.Address,
                IdentityNumber = model.IdentityNumber,
                SecurityStamp = "",
                IsConfirm = false 
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            await _userManager.AddToRoleAsync(appUser, CommonConstants.OWNER);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(appUser);
                var userVm = new UserViewModel()
                {
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    FirstName = appUser.FirstName,
                    Id = appUser.Id,
                    LastName = appUser.LastName,
                    UserName = appUser.UserName,
                    Address = appUser.Address,
                    IdentityNumber = appUser.IdentityNumber,
                    IsConfirm = appUser.IsConfirm,
                    Role = roles.FirstOrDefault()
                };
                return userVm;
            }
            return null;
        }
        public async Task<UserViewModel> RegisterForRenter(RegisterModel model)
        {
            // Kiem tra xem nguoi dung da ton tai chua
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null) throw new ServiceException("User's already exist");
            // Kiem tra xem email da ton tai chua
            if (await _userManager.FindByEmailAsync(model.Email) != null) throw new ServiceException("Email's already exist");
            var appUser = new AppUser()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Phone,
                Email = model.Email,
                Address = model.Address,
                IdentityNumber = model.IdentityNumber,
                SecurityStamp = "",
                IsConfirm = true
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            await _userManager.AddToRoleAsync(appUser, CommonConstants.RENTER);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(appUser);
                var userVm = new UserViewModel()
                {
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    FirstName = appUser.FirstName,
                    Id = appUser.Id,
                    LastName = appUser.LastName,
                    UserName = appUser.UserName,
                    Address = appUser.Address,
                    IdentityNumber = appUser.IdentityNumber,
                    IsConfirm = appUser.IsConfirm,
                    Role = roles.FirstOrDefault()
                };
                return userVm;
            }
            return null;
        }
        public async Task<string> RoleAssign(RoleAssignModel model)
        {
            if (await CheckUserAndRole(model.AccessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                throw new ServiceException("User doesn't exist");
            }
            var role = await _roleManager.FindByNameAsync(model.Role);
            if (role == null) throw new ServiceException("Role không tồn tại");
            var roleInUser = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if(roleInUser != null)
                await _userManager.RemoveFromRoleAsync(user,roleInUser);
            await _userManager.AddToRoleAsync(user,model.Role);
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }
        public async Task<UserViewModel> Update(long userId, UserUpdateModel model)
        {
            if (await CheckUserAndRole(model.AccessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(model.AccessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            if (await _userManager.Users.AnyAsync(x => x.Email == model.Email && x.Id != userId))
            {
                throw new ServiceException("Email is already exist");
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.Phone;
            user.Address = model.Address;
            user.IdentityNumber = model.IdentityNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userVm = new UserViewModel()
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Address = user.Address,
                    IdentityNumber = user.IdentityNumber,
                    IsConfirm = user.IsConfirm,
                    Role = roles.FirstOrDefault()
                };
                return userVm;
            }
            return null;
        }
        public async Task<bool> ChangePassword(long accessId,ChangePasswordModel model)
        {
            var user = _userManager.Users.Where(x => x.Id == accessId && x.IsConfirm).FirstOrDefault();
            if (user == null && user.UserName != model.UserName) throw new ServiceException("Tài khoản không tồn tại hoặc bạn chưa đăng nhập");
            if (await _userManager.CheckPasswordAsync(user, model.CurPassword) == false) throw new ServiceException("Mật khẩu hiện tại nhập không đúng");
            var result = await _userManager.ChangePasswordAsync(user, model.CurPassword, model.NewPassword);
            if (result.Succeeded)
                return true;
            return false;
        }
        public async Task<UserViewModel> ConfirmUser(long userId,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || user.IsConfirm) throw new ServiceException("Nguoi dung khong ton tai hoac da duoc confirm");
            user.IsConfirm = true;
            await _userManager.UpdateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
                Address = user.Address,
                IdentityNumber = user.IdentityNumber,
                IsConfirm = user.IsConfirm,
                Role = roles.FirstOrDefault()
            };
            return userVm;
        }
        public async Task<List<AppUser>> GetUsersNeedConfirm(long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            return _userManager.Users.Where(x => x.IsConfirm == false).ToList();
        }
    }
}
