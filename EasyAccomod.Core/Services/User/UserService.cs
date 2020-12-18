﻿using AGID.Core.Exceptions;
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
        public async Task<long> Authencate(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
            if (!result.Succeeded)
            {
                throw new ServiceException ("Đăng nhập không đúng");
            }
            return user.Id ; // Tra ve user Id
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
            var user = await _userManager.FindByIdAsync(id.ToString());
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

        public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingModel model)
        {
            if (await CheckUserAndRole(model.AccessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(model.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(model.Keyword)
                 || x.PhoneNumber.Contains(model.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((model.PageIndex - 1) * model.PageSize)
                .Take(model.PageSize)
                .Select(x => new UserViewModel()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName
                }).ToListAsync();

            //Select and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<AppUser> Register(RegisterModel model)
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
            if (await CheckUserAndRole(model.AccessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
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
        public async Task<AppUser> ConfirmUser(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null || user.IsConfirm) throw new ServiceException("Nguoi dung khong ton tai hoac da duoc confirm");
            user.IsConfirm = true;
            await _userManager.UpdateAsync(user);
            return user;
        }
        public List<AppUser> GetUsersNeedConfirm()
        {
            return context.AppUsers.Where(x => x.IsConfirm == false).ToList();
        }
    }
}