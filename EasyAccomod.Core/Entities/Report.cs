using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Entities
{

    public class Report
    {
        public long ReportId { get; set; }
        public string Reason { get; set; }
        /// <summary>
        /// Người dùng nào report
        /// </summary>
        public Guid UserId { get; set; }
        public long PostId { get; set; }
        public AppUser User { get; set; }
        public Post Post { get; set; }

    }
}
