using EasyAccomod.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Configurations
{
    public class DateViewPostConfiguration : IEntityTypeConfiguration<DateViewPost>
    {
        public void Configure(EntityTypeBuilder<DateViewPost> builder)
        {
            builder.ToTable("DateViewPosts");
            builder.HasKey(x => x.Id);
        }
    }
}
