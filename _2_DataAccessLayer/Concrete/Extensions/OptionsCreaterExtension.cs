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
            configuration = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "_2_DataAccessLayer"))  // Bu, çalıştırdığınız projedeki dizini alır
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // appsettings.json dosyasını ekliyoruz
                    .Build();

            services.Configure<MyConfig>(configuration.GetSection("ConnectionStrings"));
        }
    }
}
