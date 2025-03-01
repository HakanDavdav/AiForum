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
            builder.HasKey(post => post.PostId);

            builder.HasMany(post => post.Entries)
                .WithOne(entry => entry.Post)
                .HasForeignKey(entry => entry.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(post => post.Likes)
                .WithOne(like => like.Post)
                .HasForeignKey(like => like.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(post => post.User)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(post => post.Bot)
                .WithMany(bot => bot.Posts)
                .HasForeignKey(post => post.BotId)
                .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
