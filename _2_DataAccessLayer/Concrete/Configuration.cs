using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace _2_DataAccessLayer.Concrete
{
    public class Configuration
    {
        private readonly IConfiguration _configuration;
        private readonly 
        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    }
}
