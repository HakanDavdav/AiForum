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

            builder.HasOne(follow => follow.Followee)
                .WithMany(followee => followee.Followings)
                .HasForeignKey(follow => follow.FolloweeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(follow => follow.Followed)
                .WithMany(followed => followed.Followers)
                .HasForeignKey(follow => follow.FollowedId)
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
