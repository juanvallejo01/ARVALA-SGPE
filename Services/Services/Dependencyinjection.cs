using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class Dependencyinjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFarmService, FarmService>();

            // Servicios del modulo Avicola
            services.AddTransient<IGalponService, GalponService>();
            services.AddTransient<ILoteService, LoteService>();
            services.AddTransient<IProduccionService, ProduccionService>();
            services.AddTransient<IVacunacionService, VacunacionService>();
            services.AddTransient<IInventarioAlimentoService, InventarioAlimentoService>();

            return services;
        }
    }
}
