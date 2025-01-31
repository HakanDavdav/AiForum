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


            builder.HasMany(user => user.entries)
                .WithOne(entry => entry.user)
                .HasForeignKey(entry => entry.userId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.posts)
                .WithOne(post => post.user)
                .HasForeignKey(post => post.userId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.likes)
                .WithOne(like => like.user)
                .HasForeignKey(like => like.userId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.followings)
                .WithOne(follow => follow.followee)
                .HasForeignKey(follow => follow.followeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.followers)
                .WithOne(follow => follow.followed)
                .HasForeignKey(follow => follow.followedId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.Property(user => user.name).IsRequired();

        }
    }
}
