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
                 .IsUnique();

            // Other properties configuration (e.g., DateTime)
            builder.Property(user => user.DateTime)
                .HasDefaultValueSql("GETDATE()");  // Default value for DateTime

            builder.Property(user => user.IsProfileCreated)
                .HasDefaultValue(false);  // Default value for ProfileCreated

            builder.HasMany(user => user.Bots)
                .WithOne(bot => bot.ParentUser)
                .HasForeignKey(bot => bot.ParentUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(user => user.UserPreference)
                .WithOne(userPreference => userPreference.OwnerUser)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.ReceivedNotifications)
                .WithOne(notification => notification.OwnerUser)
                .HasForeignKey(notification => notification.OwnerUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.SentNotifications)
                .WithOne(notification => notification.FromUser)
                .HasForeignKey(notification => notification.FromUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(user => user.Entries)
                .WithOne(entry => entry.OwnerUser)
                .HasForeignKey(entry => entry.OwnerUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(user => user.Posts)
                .WithOne(post => post.OwnerUser)
                .HasForeignKey(post => post.OwnerUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(user => user.Likes)
                .WithOne(like => like.OwnerUser)
                .HasForeignKey(like => like.OwnerUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(user => user.Followed)
                .WithOne(follow => follow.UserFollower)
                .HasForeignKey(follow => follow.UserFollowerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(user => user.Followers)
                .WithOne(follow => follow.UserFollowed)
                .HasForeignKey(follow => follow.UserFollowedId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
