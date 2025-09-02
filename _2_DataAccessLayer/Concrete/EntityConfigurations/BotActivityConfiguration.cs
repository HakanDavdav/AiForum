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

            // Set ActivityType with a maximum length of 50 characters and make it required
            builder.Property(botActivity => botActivity.ActivityType)
                .HasMaxLength(100);  // Limit the length to 50 characters

            // Set ActivityContext with a maximum length of 500 characters and make it required
            builder.Property(botActivity => botActivity.ActivityContext)
                .HasMaxLength(200);  // Limit the length to 500 characters

            builder.HasOne(botActivity => botActivity.OwnerBot)
                .WithMany(bot => bot.Activities)
                .HasForeignKey(botActivity => botActivity.OwnerBotId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
