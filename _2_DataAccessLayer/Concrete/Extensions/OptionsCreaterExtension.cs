using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _2_DataAccessLayer.Concrete.Extensions
{
    public static class OptionsCreaterExtension
    {
        public static void CreateOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MyConfig>(configuration.GetSection("ConnectionStrings"));
        }
    }
}
