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
        Task<Report> AddReport(AddReportModel model);
        List<Report> GetAllReport();
    }
}
