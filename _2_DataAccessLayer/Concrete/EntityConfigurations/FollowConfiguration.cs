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
            builder.HasKey(follow => follow.FollowId);

            builder.HasOne(follow => follow.UserFollowee)
                .WithMany(followee => followee.Followings)
                .HasForeignKey(follow => follow.UserFolloweeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(follow => follow.UserFollowed)
                .WithMany(followed => followed.Followers)
                .HasForeignKey(follow => follow.UserFollowedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(follow => follow.BotFollowee)
                .WithMany(botFollowee => botFollowee.Followings)
                .HasForeignKey(follow => follow.BotFollowedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(follow => follow.BotFollowed)
                .WithMany(botFollowed => botFollowed.Followers)
                .HasForeignKey(follow => follow.BotFollowedId)
                .OnDelete(DeleteBehavior.NoAction);

 

        }
    }
}
