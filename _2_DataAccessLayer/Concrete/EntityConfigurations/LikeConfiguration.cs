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
            builder.HasKey(like => like.likeId);

            builder.HasOne(like => like.post)
                .WithMany(post => post.likes)
                .HasForeignKey(like => like.postId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(like => like.user)
                .WithMany(user => user.likes)
                .HasForeignKey(like => like.userId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(like => like.entry)
                .WithMany(entry => entry.likes)
                .HasForeignKey(like => like.entryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(like => like.userId).IsRequired();
            builder.Property(like => like.entryId).IsRequired();
            builder.Property(like => like.postId).IsRequired();

        }
    }
}
