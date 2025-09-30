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
           
            builder.HasOne(follow => follow.ActorFollower)
                .WithMany(actor => actor.Followed)
                .HasForeignKey(follow => follow.ActorFollowerId)
                .OnDelete(DeleteBehavior.SetNull);
           
            builder.HasOne(follow => follow.ActorFollowed)
                .WithMany(actor => actor.Followers)
                .HasForeignKey(follow => follow.ActorFollowedId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
