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

            builder.HasMany(user => user.Followings)
                .WithOne(follow => follow.Followee)
                .HasForeignKey(follow => follow.FolloweeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Followers)
                .WithOne(follow => follow.Followed)
                .HasForeignKey(follow => follow.FollowedId)
                .OnDelete(DeleteBehavior.NoAction);

            //User Properties are not fully required except ID because there are temporary guest users


        }
    }
}
