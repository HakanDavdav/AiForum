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
            builder.HasKey(like => like.likeID);

            builder.HasOne(like => like.post)
                .WithMany(post => post.likes)
                .HasForeignKey(like => like.postId);

            builder.HasOne(like => like.user)
                .WithMany(user => user.likes)
                .HasForeignKey(like => like.userID);

            builder.HasOne(like => like.entry)
                .WithMany(entry => entry.likes)
                .HasForeignKey(like => like.entryId);
        }
    }
}
