using Tools.Data.DbContexts;
using Tools.Data.Interfaces;
using Tools.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tools.Data
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddHerramientaDataLayerDependencyInjections(this IServiceCollection service, IConfiguration configuration)
        {
            // Add MySql Connection.
            service.AddDbContext<HerramientasDbContext>(options => options.UseMySql(connectionString: configuration.GetConnectionString("MySqlDataConnection") ?? "", ServerVersion.Parse("1.0.0.1")));

            // Add Repositories.
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IClienteRepository, ClienteRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            return service;
        }
    }
}