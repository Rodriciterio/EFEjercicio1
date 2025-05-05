using EFEjercicio1.Service.Interfaces;
using EFEjercicio1.Service.Services;
using EFEjercicio1Data;
using EFEjercicio1Data.Interfaces;
using EFEjercicio1Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication.ExtendedProtection;

namespace EFEjercicio1.Ioc
{
    public static class DI
    {
        public static IServiceProvider ConfigureDI()
        {
            var services = new ServiceCollection();
            var connectionString = @"Data Source= .; Initial Catalog=ConfectioneryDb; Trusted_Connection=true; TrustServerCertificate=true;";

            services.AddDbContext<ConfectioneryContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IConfectioneriesRepository, ConfectioneriesRepository>();
            services.AddScoped<IConfectioneryService, ConfectioneryService>();

            services.AddScoped<IDrinksRepository, DrinksRepository>();
            services.AddScoped<IDrinkService, DrinkService>();

            return services.BuildServiceProvider();
        }
    }
}
