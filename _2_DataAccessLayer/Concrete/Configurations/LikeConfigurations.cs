using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2_DataAccessLayer.Concrete.Configurations
{
    public class LikeConfigurations : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(l => l.LikeId);
            builder.HasIndex(l => new { l.ActorId, l.ContentItemId }).IsUnique();
            builder.Property(l => l.ReactionType).IsRequired();
            builder.Property(l => l.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(l => l.Actor)
                   .WithMany()
                   .HasForeignKey(l => l.ActorId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(l => l.ContentItem)
                     .WithMany()
                     .HasForeignKey(l => l.ContentItemId)
                     .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
