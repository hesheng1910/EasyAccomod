using EasyAccomod.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Configurations
{
    public class AddressNearByConfiguration : IEntityTypeConfiguration<AddressNearBy>
    {
        public void Configure(EntityTypeBuilder<AddressNearBy> builder)
        {
            builder.ToTable("AddressNearBies");
            builder.HasKey(x => x.Id);
        }
    }
}
