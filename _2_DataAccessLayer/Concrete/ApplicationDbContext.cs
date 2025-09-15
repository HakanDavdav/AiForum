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

        public DbSet<Post> Posts {  get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Entry> Entries {  get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Bot> Bots { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<BotActivity> Activities {  get; set; }
        public DbSet<TrendingPost> TrendingPosts { get; set; }
        public DbSet<BotMemoryLog> BotMemoryLogs { get; set; }

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
            modelBuilder.ApplyConfiguration(new BotActivityConfiguration());
            modelBuilder.ApplyConfiguration(new TrendingPostConfiguration());


            base.OnModelCreating(modelBuilder);
            
        }
        
    }
}
