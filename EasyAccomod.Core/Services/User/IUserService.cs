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
        Task<string> Authencate(LoginModel model);
        Task<AppUser> Delete(long id);
        Task<UserViewModel> GetById(long id);
        Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingModel model);
        Task<IList<string>> RoleAssign(long userId, RoleAssignModel model);
        Task<AppUser> Update(long userId, UserUpdateModel model);
    }
}
