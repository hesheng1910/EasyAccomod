using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.Reports
{
    public interface IReportService
    {
        Task<Report> AddReport(long userId,AddReportModel model);
        Task<List<Report>> GetAllReport(long accessId);
        Task<bool> DeleteReport(long reportId,long accessId);
    }
}
