using EasyAccomod.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    public class GetUserPagingModel : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
