using AGID.Core.Exceptions;
using EasyAccomod.Core.EF;
using EasyAccomod.Core.Model.DateViewPost;
using EasyAccomod.Core.Services.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.DateViewPosts
{
    public class DateViewPostService : IDateViewPostService
    {
        private readonly EasyAccDbContext easyAccDbContext;

        public DateViewPostService(EasyAccDbContext easyAccDbContext)
        {
            this.easyAccDbContext = easyAccDbContext;
        }

        public List<ViewPostPerDay> GetViewPostPerDays(long accessId,long postId)
        {
            var post = easyAccDbContext.Posts.Where(x => x.PostId == postId && x.PostStatus == Enums.PostStatusEnum.Accepted);
            if (post == null) throw new ServiceException("Post khong ton tai");
            List<ViewPostPerDay> viewPostPerDays = new List<ViewPostPerDay>()
            { 
                new ViewPostPerDay(){ Day = new DateTime(2020,12,26), TotalViewPerDay = 123 - 10*postId},
                new ViewPostPerDay(){ Day = new DateTime(2020,12,27), TotalViewPerDay = 143 + 10*postId},
                new ViewPostPerDay(){ Day = new DateTime(2020,12,28), TotalViewPerDay = 312 - 10*postId},
                new ViewPostPerDay(){ Day = new DateTime(2020,12,29), TotalViewPerDay = 325 - 10*postId},
                new ViewPostPerDay(){ Day = new DateTime(2020,12,30), TotalViewPerDay = 125 + 10*postId}
            };
            return viewPostPerDays;
        }
    }
}
