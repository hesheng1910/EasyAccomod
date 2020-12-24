using EasyAccomod.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Entities
{
    public class RequestExtend
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public string UserName { get; set; }
        public int RequestTime { get; set; }
        public decimal CostOfExtend { get; set; }
        public RequsetExtendStatusEnum RequsetExtendStatus { get; set; }
    }
}
