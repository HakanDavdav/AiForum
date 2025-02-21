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
    public class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasKey(entry => entry.EntryId);

            builder.HasMany(entry => entry.Likes)
                .WithOne(like => like.Entry)
                .HasForeignKey(like => like.EntryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entry => entry.Post)
                .WithMany(post => post.Entries)
                .HasForeignKey(entry => entry.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entry => entry.User)
                .WithMany(user => user.Entries)
                .HasForeignKey(entry => entry.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(entry => entry.Context).IsRequired();
            builder.Property(entry => entry.UserId).IsRequired();
            builder.Property(entry => entry.PostId).IsRequired();

        }
    }
}
