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
        Task<long> Authencate(LoginModel model);
        Task<AppUser> Delete(long id, long userId);
        Task<UserViewModel> GetById(long id,long userId);
        Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingModel model);
        Task<IList<string>> RoleAssign(RoleAssignModel model);
        Task<AppUser> Update(long userId, UserUpdateModel model);
        List<AppUser> GetUsersNeedConfirm();
        Task<AppUser> ConfirmUser(long userId);
    }
}
