using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Report;
using EasyAccomod.Core.Services.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyAccomod.FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddReport(AddReportModel model)
        {
            model.UserId = Convert.ToInt64(HttpContext.Session.GetString(CommonConstants.USER_SESSION));
            var result = await reportService.AddReport(model);
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
