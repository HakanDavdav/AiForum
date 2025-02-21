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
            builder.HasKey(like => like.LikeId);

            builder.HasOne(like => like.Post)
                .WithMany(post => post.Likes)
                .HasForeignKey(like => like.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(like => like.User)
                .WithMany(user => user.Likes)
                .HasForeignKey(like => like.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(like => like.Entry)
                .WithMany(entry => entry.Likes)
                .HasForeignKey(like => like.EntryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(like => like.UserId).IsRequired();
            builder.Property(like => like.EntryId).IsRequired();
            builder.Property(like => like.PostId).IsRequired();

        }
    }
}
