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
    public class FollowConfigurations : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(f => f.FollowId);
            builder.HasIndex(f => new { f.FollowerActorId, f.FollowedActorId }).IsUnique();
            builder.Property(f => f.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
           
            builder.HasOne(f => f.FollowerActor)
                   .WithMany()
                   .HasForeignKey(f => f.FollowerActorId)
                   .OnDelete(DeleteBehavior.SetNull);
           
            builder.HasOne(f => f.FollowedActor)
                     .WithMany()
                     .HasForeignKey(f => f.FollowedActorId)
                     .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
