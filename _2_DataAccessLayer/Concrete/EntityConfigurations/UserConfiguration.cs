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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configuring the primary key for IdentityUser
            builder.HasKey(user => user.Id);

            // Configure ProfileName to be unique
            builder.HasIndex(user => user.ProfileName)
                .IsUnique();  // Ensures ProfileName is unique

            // Other properties configuration (e.g., DateTime)
            builder.Property(user => user.DateTime)
                .HasDefaultValueSql("GETDATE()");  // Default value for DateTime

            // Other properties configuration (e.g., DateTime)
            builder.Property(user => user.DailyOperationCount)
                .HasDefaultValue(10);  // Default value for DateTime

            builder.Property(user => user.ProfileName)
                .HasDefaultValue("qwe")  // Default to null if no value is provided
                .IsRequired();  // Make sure ProfileName is not nullable at the database level


            builder.HasMany(user => user.Bots)
                .WithOne(bot => bot.User)
                .HasForeignKey(bot => bot.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(user => user.UserPreference)
                .WithOne(userPreference => userPreference.User)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.ReceivedNotifications)
                .WithOne(notification => notification.User)
                .HasForeignKey(notification => notification.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.SentNotifications)
                .WithOne(notification => notification.FromUser)
                .HasForeignKey(notification => notification.FromUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Entries)
                .WithOne(entry => entry.User)
                .HasForeignKey(entry => entry.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Posts)
                .WithOne(post => post.User)
                .HasForeignKey(post => post.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Likes)
                .WithOne(like => like.User)
                .HasForeignKey(like => like.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Followed)
                .WithOne(follow => follow.UserFollower)
                .HasForeignKey(follow => follow.UserFollowerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Followers)
                .WithOne(follow => follow.UserFollowed)
                .HasForeignKey(follow => follow.UserFollowedId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
