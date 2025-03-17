using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2_DataAccessLayer.Concrete.EntityConfigurations
{
    public class BotConfiguration : IEntityTypeConfiguration<Bot>
    {
        public void Configure(EntityTypeBuilder<Bot> builder)
        {
            builder.HasKey(bot => bot.BotId);
            builder.HasIndex(bot => bot.BotProfileName).IsUnique();        
            
            builder.Property(bot => bot.Mode).HasDefaultValue("Default");
            builder.Property(bot => bot.BotGrade).HasDefaultValue(0);
            builder.Property(bot => bot.DailyBotActionCount).HasDefaultValue(5);


            builder.HasMany(bot => bot.Posts)
                .WithOne(post => post.Bot)
                .HasForeignKey(post => post.BotId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(bot => bot.Entries)
                .WithOne(entry => entry.Bot)
                .HasForeignKey(entry => entry.BotId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(bot => bot.Likes)
                .WithOne(like => like.Bot)
                .HasForeignKey(like => like.BotId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(bot => bot.SentNotifications)
                .WithOne(notification => notification.FromBot)
                .HasForeignKey(notification => notification.FromBotId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(bot => bot.Followers)
                .WithOne(following => following.BotFollowed)
                .HasForeignKey(following => following.BotFollowedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(bot => bot.Followings)
                .WithOne(following => following.BotFollowee)
                .HasForeignKey (following => following.BotFolloweeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(bot => bot.Activities)
                .WithOne(activity => activity.Bot)
                .HasForeignKey(activity => activity.BotId)
                .OnDelete(DeleteBehavior.NoAction);
          
        }
    }
}
