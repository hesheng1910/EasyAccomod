using EasyAccomod.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Configurations
{
    public class InfrastructureConfiguration : IEntityTypeConfiguration<Infrastructure>
    {
        public void Configure(EntityTypeBuilder<Infrastructure> builder)
        {
            builder.ToTable("Infrastructures");
            builder.HasKey(x => x.Id);
        }
    }
}
