using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Common
{
    /// <summary>
    /// Chưa verify
    /// </summary>
    public class PagingRequestBase // Phân trang
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
