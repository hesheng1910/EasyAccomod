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
            var user = await _userManager.FindByIdAsync(accessId.ToString());
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");

            return await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<AuthenResult> Authencate(LoginModel model)
        {
            var user = _userManager.Users.Where(x => x.UserName == model.UserName && x.IsConfirm).FirstOrDefault();
            if (user == null) throw new ServiceException("Tài khoản không tồn tại hoặc chưa được confirm");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password,false, true);
            if (!result.Succeeded)
            {
                throw new ServiceException ("Đăng nhập không đúng");
            }
            AuthenResult authenResult = new AuthenResult()
            {
                UserId = user.Id,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return authenResult;
        }
        public async Task<AppUser> Delete(long id,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ServiceException("User không tồn tại");
            }
            var reult = await _userManager.DeleteAsync(user);
            if (reult.Succeeded)
                return user;
            return null;
        }

        public async Task<UserViewModel> GetById(long id,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var user = _userManager.Users.Where(x => x.Id == id && x.IsConfirm).FirstOrDefault();
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
                Roles = roles
            };
            return userVm;
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
                Roles = roles
            };
            return userVm;
        }

        public async Task<List<AppUser>> GetAllUsers(long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false && await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            return _userManager.Users.ToList();
        }

        public async Task<AppUser> Register(RegisterModel model)
        {
            bool isConfirm = true;
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
                IsConfirm = false 
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            
            if (result.Succeeded)
            {
                return appUser;
            }
            if (result.Errors != null) throw new ServiceException(result.Errors.ToString());
            return null;
        }
        public async Task<AppUser> RegisterForRenter(RegisterModel model)
        {
            // Kiem tra xem nguoi dung da ton tai chua
            var user = _userManager.Users.Where(x => x.UserName == model.UserName && x.IsConfirm);
            if (user != null) throw new ServiceException("User's already exist");
            // Kiem tra xem email da ton tai chua
            if (_userManager.Users.Where(x => x.Email == model.Email && x.IsConfirm) != null) throw new ServiceException("Email's already exist");
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
            Console.WriteLine(result.Errors);
            if (result.Succeeded)
            {
                return appUser;
            }
            return null;
        }
        public async Task<IList<string>> RoleAssign(RoleAssignModel model)
        {
            /*if (await CheckUserAndRole(model.AccessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");*/
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                throw new ServiceException("User doesn't exist");
            }
            var removedRoles = model.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = model.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                Console.WriteLine(roleName);
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return await _userManager.GetRolesAsync(user);
        }
        public async Task<AppUser> Update(long userId, UserUpdateModel model)
        {
            if (await CheckUserAndRole(model.AccessId, CommonConstants.ADMIN) == false)
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

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return user;
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
        public async Task<AppUser> ConfirmUser(long userId,long accessId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || user.IsConfirm) throw new ServiceException("Nguoi dung khong ton tai hoac da duoc confirm");
            user.IsConfirm = true;
            await _userManager.UpdateAsync(user);
            return user;
        }
        public async Task<List<AppUser>> GetUsersNeedConfirm(long accessId)
        {
            return _userManager.Users.Where(x => x.IsConfirm == false).ToList();
        }
    }
}
