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
    public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> builder)
        {
            builder.HasOne(UserPreference => UserPreference.User)
               .WithOne(User => User.UserPreference)
               .HasForeignKey<UserPreference>(userpreference => userpreference.UserId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
