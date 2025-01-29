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
            builder.HasKey(follow => follow.followId);

            builder.HasOne(follow => follow.followee)
                .WithMany(followee => followee.followings)
                .HasForeignKey(follow => follow.followeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(follow => follow.followed)
                .WithMany(followed => followed.followers)
                .HasForeignKey(follow => follow.followedId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(follow => follow.followeeId)
                .IsRequired(false);
            builder.Property(follow => follow.followedId)
                .IsRequired(false);
        }
    }
}
