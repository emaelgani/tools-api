using Tools.Data;
using Tools.Service.Interfaces;
using Tools.Service.Mappings.Configuration;
using Tools.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Tools.Service
{


    public static class DependencyInjections
    {
        public static IServiceCollection AddHerramientasServiceLayerDependencyInjections(this IServiceCollection service, IConfiguration configuration)
        {
            // Add Herramientas Data Layer Dependency Injections.
            service.AddHerramientaDataLayerDependencyInjections(configuration);
            // Add automapper
            service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            /* Automapper Mappings */
            service.AddSingleton(ProfilesConfiguration.MapProfiles());

            // Add services.
            service.AddScoped<IClienteService, ClienteService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IProveedorService, ProveedorService>();
            service.AddScoped<IProductoService, ProductoService>();


            return service;
        }
    }
}