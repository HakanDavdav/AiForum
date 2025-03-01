using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using _2_DataAccessLayer.Concrete.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace _2_DataAccessLayer.Concrete
{
    public class ApplicationDbContext : IdentityDbContext<User,UserRole,int>
    {
        private readonly MyConfig _config;


        public ApplicationDbContext(IOptions<MyConfig> config)
        {
            _config = config.Value;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.DefaultConnection);

        }

        public DbSet<Post> posts {  get; set; }
        public DbSet<Like> likes { get; set; }
        public DbSet<Entry> entries {  get; set; }
        public DbSet<Follow> follows { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<Bot> bots { get; set; }
        public DbSet<UserPreference> userPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new FollowConfiguration());
            modelBuilder.ApplyConfiguration(new EntryConfiguration());
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new BotConfiguration());
            modelBuilder.ApplyConfiguration(new UserPreferenceConfiguration());

            base.OnModelCreating(modelBuilder);
            
        }
    }
}
