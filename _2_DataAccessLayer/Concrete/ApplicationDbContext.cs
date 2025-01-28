﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using _2_DataAccessLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace _2_DataAccessLayer.Concrete
{
    public class ApplicationDbContext : IdentityDbContext
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

        public DbSet<User> users { get; set; }
        public DbSet<Post> posts {  get; set; }
        public DbSet<Like> likes { get; set; }
        public DbSet<Entry> entries {  get; set; }
        public DbSet<Follow> follows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
