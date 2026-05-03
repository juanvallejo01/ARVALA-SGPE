using Domain;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {

            var c = configuration.GetConnectionString("DefaultConnection");
            //aca van los repositorios
            services.AddScoped<IFarmRepository, FarmRepository>();

            // Repositorios del modulo Avicola
            services.AddScoped<IGalponRepository, GalponRepository>();
            services.AddScoped<ILoteRepository, LoteRepository>();
            services.AddScoped<IProduccionRepository, ProduccionRepository>();
            services.AddScoped<IVacunacionRepository, VacunacionRepository>();
            services.AddScoped<IInventarioAlimentoRepository, InventarioAlimentoRepository>();

            services.AddDbContext<AppDbContext>(options =>
            {


                options.UseSqlServer(c);

            });
            return services;
        }
    }
}
