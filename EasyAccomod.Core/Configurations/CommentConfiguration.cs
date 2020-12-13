using EasyAccomod.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAccomod.Core.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.CommentId);
            builder.Property(x => x.CommentId).ValueGeneratedOnAdd();
            builder.Property(x => x.ReviewContent).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Star).IsRequired();
            builder.Property(x => x.IsConfirm).HasDefaultValue(false);
        }
    }
}