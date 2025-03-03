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
