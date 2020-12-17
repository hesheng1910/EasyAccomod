using EasyAccomod.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Configurations
{
    public class UserLikePostConfiguration: IEntityTypeConfiguration<UserLikePost>
    {
        public void Configure(EntityTypeBuilder<UserLikePost> builder)
        {
            builder.ToTable("UserLikePosts");
            builder.HasKey(x => new { x.UserId,x.PostId });
        }
    }
}
