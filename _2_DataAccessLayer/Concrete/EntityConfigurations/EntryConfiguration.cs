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
            builder.HasKey(entry => entry.entryId);

            builder.HasMany(entry => entry.likes)
                .WithOne(like => like.entry)
                .HasForeignKey(like => like.entryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entry => entry.post)
                .WithMany(post => post.entries)
                .HasForeignKey(entry => entry.postId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entry => entry.user)
                .WithMany(user => user.entries)
                .HasForeignKey(entry => entry.userId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Property(entry => entry.context).IsRequired();
            builder.Property(entry => entry.userId).IsRequired();
            builder.Property(entry => entry.postId).IsRequired();

        }
    }
}
