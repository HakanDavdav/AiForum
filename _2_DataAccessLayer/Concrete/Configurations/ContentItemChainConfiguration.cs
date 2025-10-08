using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2_DataAccessLayer.Concrete.Configurations
{
    public class ContentItemChainConfiguration : IEntityTypeConfiguration<ContentItemChain>
    {
        public void Configure(EntityTypeBuilder<ContentItemChain> builder)
        {
            builder.HasKey(cic => cic.ContentItemChainId);

            builder.HasOne(cic => cic.RootContentItem)
                   .WithMany()
                   .HasForeignKey(cic => cic.RootContentItemId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(cic => cic.ChildContentItem)
                   .WithMany()
                   .HasForeignKey(cic => cic.ChildContentItemId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
