using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    // • Many-to-many relationships (Follow, TribeMembership):
    // Navigation properties are defined on both sides, as access from either direction is required.
    //
    // • One-to-one relationships (UserIdentity, UserSettings, BotSettings):
    // Navigation properties are defined only on the overarching (owning) entity,
    // since only the main entity should include the subordinate one.
    public class ContentItemChainConfiguration : IEntityTypeConfiguration<ContextForBotItemChain>
    {
        public void Configure(EntityTypeBuilder<ContextForBotItemChain> builder)
        {
            builder.HasKey(cic => cic.ContentItemChainId);
            builder.HasIndex(cic => new {cic.RootContentItemId, cic.ChildContentItemId }).IsUnique();

            builder.HasOne(cic => cic.RootContentItem)
                   .WithMany(ci => ci.ContextForBotItemChainsAsRoot)
                   .HasForeignKey(cic => cic.RootContentItemId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(cic => cic.ChildContentItem)
                   .WithMany(ci => ci.ContextForBotItemChainsAsChild)
                   .HasForeignKey(cic => cic.ChildContentItemId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
