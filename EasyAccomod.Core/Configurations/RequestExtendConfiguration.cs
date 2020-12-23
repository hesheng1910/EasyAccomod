using EasyAccomod.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Configurations
{
    public class RequestExtendConfiguration : IEntityTypeConfiguration<RequestExtend>
    {
        public void Configure(EntityTypeBuilder<RequestExtend> builder)
        {
            builder.ToTable("RequestExtends");
            builder.HasKey(x => x.Id);
        }
    }
}
