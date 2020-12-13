using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Common
{
    public class PagingRequestBase // Phân trang
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
