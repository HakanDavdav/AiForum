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
    public class BotActivityConfiguration : IEntityTypeConfiguration<BotActivity>
    {
        public void Configure(EntityTypeBuilder<BotActivity> builder)
        {
            // Set the primary key
            builder.HasKey(botActivity => botActivity.ActivityId);

            builder.HasOne(botActivity => botActivity.OwnerBot)
                .WithMany(bot => bot.Activities)
                .HasForeignKey(botActivity => botActivity.OwnerBotId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
