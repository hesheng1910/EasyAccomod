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
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            var result = await reportService.AddReport(accessId,model);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Mod
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllReport()
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await reportService.GetAllReport(accessId));
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteReport(long reportId)
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await reportService.DeleteReport(reportId,accessId));
        }
    }
}
