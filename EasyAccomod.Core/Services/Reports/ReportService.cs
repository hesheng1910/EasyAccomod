using AGID.Core.Exceptions;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.EF;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Report;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.Reports
{

    public class ReportService : IReportService
    {
        private readonly EasyAccDbContext context;
        private readonly UserManager<AppUser> userManager;

        public ReportService(EasyAccDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<bool> CheckUserAndRole(long accessId, string role)
        {
            var user = userManager.Users.Where(x => x.Id == accessId && x.IsConfirm).FirstOrDefault();
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");

            return await userManager.IsInRoleAsync(user, role);
        }
        public async Task<Report> AddReport (long userId,AddReportModel model)
        {
            if (await CheckUserAndRole(userId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(userId, CommonConstants.ADMIN) == false && await CheckUserAndRole(userId, CommonConstants.RENTER) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var post = context.Posts.Where(p => p.PublicTime < DateTime.Now && p.PostStatus == Enums.PostStatusEnum.Accepted && p.PostId == model.PostId);
            if (post == null) throw new ServiceException("Bài đăng không tồn tại");
            Report report = new Report()
            {
                UserName = model.UserName,
                Reason = model.Reason,
                PostId = model.PostId,
                IsDelete = false
            };
            await context.Reports.AddAsync(report);
            await context.SaveChangesAsync();
            return report;
        }
        public async Task<List<Report>> GetAllReport(long userId)
        {
            if (await CheckUserAndRole(userId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(userId, CommonConstants.ADMIN) == false )
                throw new ServiceException("Tài khoản không có quyền truy cập");
            return context.Reports.Where(x => x.IsDelete == false).ToList();
        }
        public async Task<bool> DeleteReport(long reportId,long userId)
        {
            if (await CheckUserAndRole(userId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(userId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var report = context.Reports.Where(x => x.ReportId == reportId && x.IsDelete == false).FirstOrDefault();
            if (report == null) throw new ServiceException("Report khong ton tai");
            report.IsDelete = true;
            var result = await context.SaveChangesAsync();
            if(result != 0)
                return true;
            return false;
        }
    }
}
