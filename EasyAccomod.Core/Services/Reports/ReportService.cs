using AGID.Core.Exceptions;
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

        public async Task<Report> AddReport (AddReportModel model)
        {
            var post = await context.Posts.FindAsync(model.PostId);
            if (post == null) throw new ServiceException("Bài đăng không tồn tại");
            Report report = new Report()
            {
                UserId = model.UserId,
                Reason = model.Reason,
                PostId = model.PostId
            };
            await context.Reports.AddAsync(report);
            await context.SaveChangesAsync();
            return report;
        }
        public List<Report> GetAllReport()
        {
            return context.Reports.ToList();
        }
    }
}
