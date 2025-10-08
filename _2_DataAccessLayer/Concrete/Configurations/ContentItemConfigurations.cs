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
    // Navigation properties were configured according to relationship type and access pattern:
    //
    // • One-to-many relationships (Post, Like, Entry):
    // Navigation properties are defined only on the child side (e.g., Like.Post.Entry),
    // since these entities are queried separately.
    // (Exception: Self referencing Bot entity, where both sides are included.)
    // (Exception: Self referencing ContentItem entity, where both sides are included.)
    //
    // • Many-to-many relationships (Follow, TribeMembership, TribeRivalry):
    // Navigation properties are defined on both sides, as access from either direction is required.
    //
    // • One-to-one relationships (UserIdentity, UserSettings, BotSettings):
    // Navigation properties are defined only on the overarching (owning) entity,
    // since only the main entity should include the subordinate one.
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
            builder.HasKey(ci => ci.ContentItemId);
            builder.HasDiscriminator<string>("ContentType")
                .HasValue<ContentItem>(nameof(ContentItem))
                .HasValue<Post>(nameof(Post))
                .HasValue<Entry>(nameof(Entry));
            builder.Property(ci => ci.Content).HasMaxLength(_config.MaxContentLength);
            builder.Property(ci => ci.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(ci => ci.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            
            builder.HasOne(ci => ci.Actor)
                   .WithMany()
                   .HasForeignKey(e => e.ActorId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(ci => ci.ChildEntries)
                   .WithOne(e => e.ParentContent)
                   .HasForeignKey(e => e.ParentContentId)
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
                   .WithMany(ci => ci.ChildEntries)
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
