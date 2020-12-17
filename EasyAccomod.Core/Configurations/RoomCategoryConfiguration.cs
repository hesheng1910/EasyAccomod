using EasyAccomod.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Configurations
{
    public class RoomCategoryConfiguration : IEntityTypeConfiguration<RoomCategory>
    {
        public void Configure(EntityTypeBuilder<RoomCategory> builder)
        {
            builder.ToTable("RoomCategorys");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
