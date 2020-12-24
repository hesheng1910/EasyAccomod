using EasyAccomod.Core.Common;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.User
{
    public interface IUserService
    {
        Task<AppUser> Register(RegisterModel model);
        Task<AppUser> RegisterForRenter(RegisterModel model);
        Task<long> Authencate(LoginModel model);
        Task<AppUser> Delete(long id, long accessId);
        Task<UserViewModel> GetById(long id,long userId);
        Task<List<AppUser>> GetAllUsers(long accessId);
        Task<IList<string>> RoleAssign(RoleAssignModel model);
        Task<AppUser> Update(long userId, UserUpdateModel model);
        Task<List<AppUser>> GetUsersNeedConfirm(long accessId);
        Task<AppUser> ConfirmUser(long userId,long accessId);
    }
}
