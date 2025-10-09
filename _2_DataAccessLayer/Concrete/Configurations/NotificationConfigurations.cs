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
    public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.NotificationId);
            builder.Property(n => n.IsRead).HasDefaultValue(false);
            builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.HasOne(n => n.SenderActor)
                   .WithMany()
                   .HasForeignKey(n => n.SenderActorId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(n => n.ReceiverUser)
                    .WithMany()
                    .HasForeignKey(n => n.ReceiverUserId)
                    .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(n => n.BotActivity)
                    .WithMany()
                    .HasForeignKey(n => n.BotActivityId)
                    .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
