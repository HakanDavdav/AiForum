using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2_DataAccessLayer.Concrete.EntityConfigurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            // Configuring the primary key
            builder.HasKey(notification => notification.NotificationId);

            // Title: Maximum length of 100 characters
            builder.Property(notification => notification.Title)
                .HasMaxLength(100);  // Max length of 100 characters

            // Context: Maximum length of 200 characters
            builder.Property(notification => notification.Context)
                .HasMaxLength(200);  // Max length of 200 characters

            // IsRead: Default value is false
            builder.Property(notification => notification.IsRead)
                .HasDefaultValue(false);  // Default value set to false

            builder.HasOne(notification => notification.User)
                .WithMany(user => user.ReceivedNotifications)
                .HasForeignKey(notification => notification.UserId)
                .OnDelete(DeleteBehavior.NoAction);
           
            builder.HasOne(notification => notification.FromUser)
                .WithMany(fromUser => fromUser.SentNotifications)
                .HasForeignKey(notification => notification.FromUserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(notification => notification.FromBot)
                .WithMany(fromBot => fromBot.SentNotifications)
                .HasForeignKey(notification => notification.FromBotId)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
