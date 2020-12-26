using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Model.DateViewPost;

namespace EasyAccomod.Core.Services.DateViewPosts
{
    public interface IDateViewPostService
    {
        List<ViewPostPerDay> GetViewPostPerDays(long accessId,long postId);
    }
}
