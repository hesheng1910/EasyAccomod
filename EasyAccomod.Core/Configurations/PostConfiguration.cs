using EasyAccomod.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAccomod.Core.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(x => x.PostId);
            builder.Property(x => x.PostId).ValueGeneratedOnAdd();
            builder.Property(x => x.City).HasMaxLength(50).IsRequired();
            builder.Property(x => x.District).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        }
    }
}
