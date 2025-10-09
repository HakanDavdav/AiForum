using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.Entities._2_DataAccessLayer.Concrete.Enums.OtherEnums;
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
    class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        private readonly MyConfig _config;
        public ActorConfiguration(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }

        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.UseTphMappingStrategy();
            builder.HasKey(a => a.ActorId);
            builder.HasDiscriminator<string>("ActorType")
                .HasValue<Actor>(nameof(Actor))
                .HasValue<Bot>(nameof(Bot))
                .HasValue<User>(nameof(User));

            builder.HasIndex(a => a.ProfileName).IsUnique();
            builder.Property(a => a.ProfileName).HasMaxLength(_config.MaxProfileNameLength).IsRequired();
            builder.Property(a => a.ImageUrl).HasMaxLength(_config.MaxImageUrlLength);
            builder.Property(a => a.Bio).HasMaxLength(_config.MaxBioLength);
            builder.Property(a => a.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(a => a.Interests).HasDefaultValue(0);

            builder.HasMany(a => a.Bots)
                   .WithOne(b => b.Actor)
                   .HasForeignKey(b => b.ActorId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(a => a.TribeMemberships)
                   .WithOne(tm => tm.Actor)
                   .HasForeignKey(tm => tm.ActorId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }


    class UserConfigurations : IEntityTypeConfiguration<User>
    {
        private readonly MyConfig _config;
        public UserConfigurations(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(a => a.UserIdentity)
                   .WithOne()
                   .HasForeignKey<UserIdentity>(ui => ui.Id)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(a => a.UserSettings)
                     .WithOne()
                     .HasForeignKey<UserSettings>(us => us.ActorId)
                     .OnDelete(DeleteBehavior.SetNull);

        }
    }

    class UserSettingsConfigurations : IEntityTypeConfiguration<UserSettings>
    {
        private readonly MyConfig _config;
        public UserSettingsConfigurations(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }
        public void Configure(EntityTypeBuilder<UserSettings> builder)
        {
            builder.HasKey(us => us.ActorId);
            builder.Property(us => us.PostPerPage).HasDefaultValue(30);
            builder.Property(us => us.EntryPerPage).HasDefaultValue(10);
            builder.Property(us => us.Theme).HasDefaultValue(ThemeOptions.Light);
            builder.Property(us => us.PremiumFeatures).HasDefaultValue(0);
            builder.Property(us => us.IsProfileCreated).HasDefaultValue(false);
            builder.Property(us => us.SocialNotificationPreference).HasDefaultValue(true);
            builder.Property(us => us.SocialEmailPreference).HasDefaultValue(true);
        }
    }

    class BotConfigurations : IEntityTypeConfiguration<Bot>
    {
        private readonly MyConfig _config;
        public BotConfigurations(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }
        public void Configure(EntityTypeBuilder<Bot> builder)
        {
            builder.HasOne(b => b.Actor)
                   .WithMany()
                   .HasForeignKey(b => b.ActorId)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(b => b.BotSettings)
                     .WithOne()
                     .HasForeignKey<BotSettings>(bs => bs.ActorId)
                     .OnDelete(DeleteBehavior.SetNull);

        }
    }

    class BotSettingsConfigurations : IEntityTypeConfiguration<BotSettings>
    {
        private readonly MyConfig _config;
        public BotSettingsConfigurations(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }
        public void Configure(EntityTypeBuilder<BotSettings> builder)
        {
            builder.HasKey(bs => bs.ActorId);
            builder.Property(bs => bs.BotPersonality).HasMaxLength(_config.MaxPersonalityLength);
            builder.Property(bs => bs.Instructions).HasMaxLength(_config.MaxInstructionLength);
            builder.Property(bs => bs.BotGrade).HasDefaultValue(BotGrades.C);
            builder.Property(bs => bs.BotMode).HasDefaultValue(BotModes.Default);
            builder.Property(bs => bs.BotCapabilities).HasDefaultValue(0);
            builder.Property(bs => bs.DailyBotOperationCount).HasDefaultValue(0);
            builder.Property(bs => bs.DailyOperationCheck).HasDefaultValue(false);

        }
    }

}
