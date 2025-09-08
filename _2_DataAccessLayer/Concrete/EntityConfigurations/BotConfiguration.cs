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

            builder.HasKey(bot => bot.Id);  // Set the primary key for the ParentBot entity

            builder.HasIndex(bot => bot.BotProfileName).IsUnique();  // Create a unique index on BotProfileName to ensure no duplicates

            // BotProfileName, maximum length of 20 characters
            builder.Property(bot => bot.BotProfileName)
                .HasMaxLength(20);  // Limit the length of BotProfileName to 20 characters

            // ImageUrl, should be in URL format
            builder.Property(bot => bot.ImageUrl)
                .HasMaxLength(500);  // Limit the length of ImageUrl to 500 characters (appropriate for URLs)

            // BotPersonality, maximum length of 100 characters
            builder.Property(bot => bot.BotPersonality)
                .HasMaxLength(100);  // Limit BotPersonality to 100 characters

            // Instructions, maximum length of 100 characters
            builder.Property(bot => bot.Instructions)
                .HasMaxLength(100);  // Limit Instructions to 100 characters

            // Mode, can only have 3 specific values: "DEFAULT", "OPPOSING", "INDEPENDENT"
            builder.Property(bot => bot.Mode)
                .IsRequired()  // Mark this field as required
                .HasMaxLength(50)  // Limit the length of Mode to 50 characters (adequate for the 3 values)
                .HasConversion(
                    v => v.ToString(),  // Convert Mode to string when saving to the database
                    v => v.ToUpper()    // Ensure the stored value is uppercase
                )
                .HasDefaultValue("DEFAULT");  // Set the default value of Mode to "DEFAULT"

            // BotGrade, can be a number with a maximum value of 5
            builder.Property(bot => bot.BotGrade)
                .HasDefaultValue(1)  // Set the default value of BotGrade to 5
                .HasConversion<int>();  // Store BotGrade as a numerical value in the database

            // BotDateTime, default value is the current date
            builder.Property(bot => bot.DateTime)
                .HasDefaultValueSql("GETDATE()");  // Set the default value of DateTime to the current SQL Server date

            builder.Property(bot => bot.BotGrade)
                .HasDefaultValue(1);  // Set the default value of BotGrade to 0 if not provided

            builder.Property(bot => bot.DailyBotOperationCount)
                .HasDefaultValue(5);  // Set the default value of DailyBotOperationCount to 5 if not provided


            builder.HasMany(bot => bot.Posts)
                .WithOne(post => post.OwnerBot)
                .HasForeignKey(post => post.OwnerBotId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(bot => bot.Entries)
                .WithOne(entry => entry.OwnerBot)
                .HasForeignKey(entry => entry.OwnerBotId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(bot => bot.Likes)
                .WithOne(like => like.OwnerBot)
                .HasForeignKey(like => like.OwnerBotId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(bot => bot.SentNotifications)
                .WithOne(notification => notification.FromBot)
                .HasForeignKey(notification => notification.FromBotId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(bot => bot.ChildBots)
                .WithOne(childBot => childBot.ParentBot)
                .HasForeignKey(childBot => childBot.ParentBotId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(bot => bot.Followers)
                .WithOne(following => following.BotFollowed)
                .HasForeignKey(following => following.BotFollowedId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(bot => bot.Followed)
                .WithOne(following => following.BotFollower)
                .HasForeignKey (following => following.BotFollowerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(bot => bot.Activities)
                .WithOne(activity => activity.OwnerBot)
                .HasForeignKey(activity => activity.OwnerBotId)
                .OnDelete(DeleteBehavior.SetNull);
          
        }
    }
}
