using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Entities
{
    public class Comment
    {
        public long CommentId { get; set; }
        public string Title { get; set; }
        public int Star { get; set; }
        public string ReviewContent { get; set; }
        public bool IsConfirm { get; set; }

    }
}
