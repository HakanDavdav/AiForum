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
            // Configuring the primary key
            builder.HasKey(entry => entry.EntryId);

            // Configuring the "Context" property with a maximum length of 1000 characters
            builder.Property(entry => entry.Context)
                .HasMaxLength(1000)  // Setting the max length to 1000 characters
                .IsRequired();        // Optionally, you can make this field required by adding this line

            // Configuring other properties if needed
            builder.Property(entry => entry.DateTime)
                .HasDefaultValueSql("GETDATE()");  // Default value for DateTime if needed

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

            builder.HasOne(entry => entry.Bot)
                .WithMany(bot => bot.Entries)
                .HasForeignKey(entry => entry.BotId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
