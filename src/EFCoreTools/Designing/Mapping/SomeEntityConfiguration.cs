using EFCoreTools.SampleEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTools.Designing.Mapping
{
    public class AnEntityConfiguration : DbEntityConfiguration<AnEntity>
    {
        public override void Configure(EntityTypeBuilder<AnEntity> entity)
        {
            entity.ToTable("AnEntityTableName");
           
            // etc.
        }
    }
}
