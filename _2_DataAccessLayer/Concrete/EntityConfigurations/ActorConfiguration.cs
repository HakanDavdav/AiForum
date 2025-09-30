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
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(actor => actor.ActorId);

            builder.HasMany(actor => actor.SentNotifications)
                .WithOne(notification => notification.FromActor)
                .HasForeignKey(notification => notification.FromActorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(actor => actor.Posts)   
                .WithOne(post => post.ActorOwner)
                .HasForeignKey(post => post.ActorOwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(actor => actor.Entries)
                .WithOne(entry => entry.ActorOwner)
                .HasForeignKey(entry => entry.ActorOwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(actor => actor.Likes)
                .WithOne(like => like.ActorOwner)
                .HasForeignKey(like => like.ActorOwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(actor => actor.Followed)
                .WithOne(follow => follow.ActorFollower)
                .HasForeignKey(follow => follow.ActorFollowerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(actor => actor.Followers)
                .WithOne(follow => follow.ActorFollowed)
                .HasForeignKey(follow => follow.ActorFollowedId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(actor => actor.Bots)
                .WithOne(bot => bot.OwnerActor)
                .HasForeignKey(bot => bot.OwnerActorId)
                .OnDelete(DeleteBehavior.SetNull);



        }
    }
}
