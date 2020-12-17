using EasyAccomod.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    /// <summary>
    /// Model phân trang user
    /// </summary>
    public class GetUserPagingModel : PagingRequestBase
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// Id người truy cập
        /// </summary>
        public long AccessId { get; set; }
    }
}
