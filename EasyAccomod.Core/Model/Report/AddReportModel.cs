using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.Report
{
    public class AddReportModel
    {
        public string Reason { get; set; }
        /// <summary>
        /// Người dùng nào report
        /// </summary>
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
