using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Common
{
    [Serializable]
    public class AuthenResult
    {
        public long UserId { get; set; }
        public string Role { get; set; }
    }
}
