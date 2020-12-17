using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.Comment
{
    public class AddCommentModel
    {
        public string Title { get; set; }
        public int Star { get; set; }
        public string ReviewContent { get; set; }
    }
}
