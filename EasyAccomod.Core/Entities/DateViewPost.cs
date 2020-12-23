using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Entities
{
    public class DateViewPost
    {
        public long Id { get; set; }
        public DateTime ViewDate { get; set; }
        public long PostId { get; set; }
    }
}
