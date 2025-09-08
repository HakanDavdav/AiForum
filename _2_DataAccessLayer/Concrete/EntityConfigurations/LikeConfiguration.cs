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
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            // Configuring the primary key
            builder.HasKey(like => like.LikeId);

            // Configuring other properties if needed
            builder.Property(like => like.DateTime)
                .HasDefaultValueSql("GETDATE()");  // Default value for DateTime if needed

            builder.HasOne(like => like.Post)
                .WithMany(post => post.Likes)
                .HasForeignKey(like => like.PostId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(like => like.Entry)
                .WithMany(entry => entry.Likes)
                .HasForeignKey(like => like.EntryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(like => like.OwnerUser)
                .WithMany(user => user.Likes)
                .HasForeignKey(like => like.OwnerUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(like => like.OwnerBot)
                .WithMany(bot => bot.Likes)
                .HasForeignKey(like => like.OwnerBotId)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}
