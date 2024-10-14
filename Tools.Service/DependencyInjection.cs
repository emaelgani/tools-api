using Tools.Data;
using Tools.Service.Interfaces;
using Tools.Service.Mappings.Configuration;
using Tools.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tools.Data.Repositories;
using Tools.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using PdfSharp.Charting;


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

            service.AddScoped<IProveedorRepository, ProveedorRepository>();

            service.AddScoped<IProveedorRepository>(serviceProvider =>
               new CachedProveedorRepository(
                   serviceProvider.GetRequiredService<ProveedorRepository>(),
                   serviceProvider.GetRequiredService<IMemoryCache>()
               ));

            // Add services.
            service.AddScoped<IClienteService, ClienteService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IProveedorService, ProveedorService>();
            service.AddScoped<IProductoService, ProductoService>();
            service.AddScoped<IPedidoService, PedidoService>();
            service.AddScoped<IPagoService, PagoService>();
            service.AddScoped<IVentaService, VentaService>();
            service.AddScoped<ICompromisoService, CompromisoService>();


            return service;
        }
    }
}