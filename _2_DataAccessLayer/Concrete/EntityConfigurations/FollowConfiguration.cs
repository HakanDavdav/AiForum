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
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            // Configuring the primary key
            builder.HasKey(follow => follow.FollowId);

            // Configuring other properties if needed
            builder.Property(follow =>follow.DateTime)
                .HasDefaultValueSql("GETDATE()");  // Default value for DateTime if needed

            builder.HasOne(follow => follow.UserFollower)
                .WithMany(followee => followee.Followed)
                .HasForeignKey(follow => follow.UserFollowerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(follow => follow.UserFollowed)
                .WithMany(followed => followed.Followers)
                .HasForeignKey(follow => follow.UserFollowedId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(follow => follow.BotFollower)
                .WithMany(botFollowee => botFollowee.Followed)
                .HasForeignKey(follow => follow.BotFollowedId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(follow => follow.BotFollowed)
                .WithMany(botFollowed => botFollowed.Followers)
                .HasForeignKey(follow => follow.BotFollowedId)
                .OnDelete(DeleteBehavior.SetNull);

 

        }
    }
}
