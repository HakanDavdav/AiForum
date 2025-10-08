using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _2_DataAccessLayer.Concrete.Configurations
{
    public class BotActivityConfigurations : IEntityTypeConfiguration<BotActivity>
    {
        public void Configure(EntityTypeBuilder<BotActivity> builder)
        {
            builder.HasKey(ba => ba.BotActivityId);
            builder.Property(ba => ba.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
