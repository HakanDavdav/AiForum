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
    public class TribeConfigurations : IEntityTypeConfiguration<Tribe>
    {
        private readonly MyConfig _config;
        public TribeConfigurations(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }
        public void Configure(EntityTypeBuilder<Tribe> builder)
        {
            builder.HasKey(t => t.TribeId);
            builder.HasIndex(t => t.TribeName).IsUnique();
            builder.Property(t => t.TribeName).HasMaxLength(_config.MaxTribeNameLength).IsRequired();
            builder.Property(t => t.Mission).HasMaxLength(_config.MaxMissionLength).IsRequired();
            builder.Property(t => t.ImageUrl).HasMaxLength(_config.MaxImageUrlLength);
            builder.Property(t => t.PersonalityModifier).HasMaxLength(_config.MaxPersonalityModifierLength);
            builder.Property(t => t.InstructionModifier).HasMaxLength(_config.MaxInstructionModifierLength);
            builder.Property(t => t.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(t => t.TribeMemberships)
                .WithOne(tm => tm.Tribe)
                .HasForeignKey(tm => tm.TribeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.TribeRivalries)
                .WithOne(tm => tm.Tribe)
                .HasForeignKey(tm => tm.TribeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.RivalsForTribe)
                .WithOne(tm => tm.RivalTribe)
                .HasForeignKey(tm => tm.RivalTribeId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }

    public class TribeMembershipConfiguration : IEntityTypeConfiguration<TribeMembership>
    {
        public void Configure(EntityTypeBuilder<TribeMembership> builder)
        {
            builder.HasKey(tm => tm.TribeMemberId);
            builder.HasIndex(tm => new { tm.ActorId, tm.TribeId }).IsUnique();
            builder.HasOne(tm => tm.Actor)
                   .WithMany(a => a.TribeMemberships)
                   .HasForeignKey(tm => tm.ActorId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(tm => tm.Tribe)
                   .WithMany(t => t.TribeMemberships)
                   .HasForeignKey(tm => tm.TribeId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class TribeRivalryConfiguration : IEntityTypeConfiguration<TribeRivalry>
    {
        public void Configure(EntityTypeBuilder<TribeRivalry> builder)
        {
            builder.HasKey(tr => tr.TribeRivalryId);
            builder.HasIndex(tr => new { tr.TribeId, tr.RivalTribeId }).IsUnique();
            builder.HasOne(tr => tr.Tribe)
                   .WithMany(t => t.TribeRivalries)
                   .HasForeignKey(tr => tr.TribeId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(tr => tr.RivalTribe)
                   .WithMany(t => t.RivalsForTribe)
                   .HasForeignKey(tr => tr.RivalTribeId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
