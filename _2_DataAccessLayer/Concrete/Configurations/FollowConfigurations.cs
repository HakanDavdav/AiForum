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
    // Navigation properties were configured according to relationship type and access pattern:
    //
    // • One-to-many relationships (Post, Like, Entry):
    // Navigation properties are defined only on the child side (e.g., Like.Post.Entry),
    // since these entities are queried separately.
    // (Exception: Self referencing Bot entity, where both sides are included.)
    // (Exception: Self referencing ContentItem entity, where both sides are included.)
    //
    // • Many-to-many relationships (Follow, TribeMembership, TribeRivalry):
    // Navigation properties are defined on both sides, as access from either direction is required.
    //
    // • One-to-one relationships (UserIdentity, UserSettings, BotSettings):
    // Navigation properties are defined only on the overarching (owning) entity,
    // since only the main entity should include the subordinate one.
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
