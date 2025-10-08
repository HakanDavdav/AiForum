using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace _2_DataAccessLayer.Concrete.Configurations
{

    public class ContentItemConfiguration : IEntityTypeConfiguration<ContentItem>
    {
        private readonly MyConfig _config;
        public ContentItemConfiguration(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }
        public void Configure(EntityTypeBuilder<ContentItem> builder)
        {
            builder.UseTphMappingStrategy();
            builder.HasKey(a => a.ContentItemId);
            builder.HasDiscriminator<string>("ContentType")
                .HasValue<ContentItem>(nameof(ContentItem))
                .HasValue<Post>(nameof(Post))
                .HasValue<Entry>(nameof(Entry));
            builder.Property(e => e.Content).HasMaxLength(_config.MaxContentLength);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            builder.HasOne(e => e.Actor)
                   .WithMany()
                   .HasForeignKey(e => e.ActorId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        private readonly MyConfig _config;
        public EntryConfiguration(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasOne(e => e.ParentContent)
                   .WithMany()
                   .HasForeignKey(e => e.ParentContentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }

    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        private readonly MyConfig _config;
        public PostConfiguration(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasIndex(p => p.Title).IsUnique();
            builder.Property(p => p.Title).HasMaxLength(_config.MaxPostTitleLength);

        }
    }
}
