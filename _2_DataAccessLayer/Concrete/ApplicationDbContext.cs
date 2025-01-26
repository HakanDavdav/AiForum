using System;
using System.Collections.Generic;
using System.Linq;
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
            optionsBuilder.UseSqlServer(_config.Default);
        }

        DbSet<User> users { get; set; }


    }
}
