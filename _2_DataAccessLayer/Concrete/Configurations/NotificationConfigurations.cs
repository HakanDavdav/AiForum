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
    public class NotificationConfigurations : IEntityTypeConfiguration<Notifications>
    {
        public void Configure(EntityTypeBuilder<Notifications> builder)
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
