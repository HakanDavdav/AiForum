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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(post => post.postId);

            builder.HasMany(post => post.entries)
                .WithOne(entry => entry.post)
                .HasForeignKey(entry => entry.postId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(post => post.likes)
                .WithOne(like => like.post)
                .HasForeignKey(like => like.postId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(post => post.user)
                .WithMany(user => user.posts)
                .HasForeignKey(post => post.userId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(post => post.title).IsRequired();
            builder.Property(post => post.context).IsRequired();
            builder.Property(post => post.userId).IsRequired();


        }
    }
}
