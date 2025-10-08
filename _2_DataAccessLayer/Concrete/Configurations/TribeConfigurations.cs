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




        }
    }
}
