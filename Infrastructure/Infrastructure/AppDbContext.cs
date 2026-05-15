using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Milk> Milks { get; set; }
        public DbSet<Cow> Cows { get; set; }
        public DbSet<Farm> Farms { get; set; }

        // Modulo Avicola
        public DbSet<Galpon> Galpones { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<ProduccionDiaria> ProduccionesDiarias { get; set; }
        public DbSet<InventarioAlimento> InventariosAlimento { get; set; }
        public DbSet<RegistroVacunacion> RegistrosVacunacion { get; set; }
        public DbSet<PrecioHuevo> PreciosHuevo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<InventarioAlimento>()
                .Property(x => x.CantidadKg)
                .HasPrecision(18, 4);
            builder.Entity<ProduccionDiaria>()
                .Property(x => x.AlimentoConsumidoKg)
                .HasPrecision(18, 4);
            builder.Entity<PrecioHuevo>()
                .Property(x => x.PrecioUnitario)
                .HasPrecision(18, 4);
            builder.Entity<PrecioHuevo>()
                .Property(x => x.PrecioPorDocena)
                .HasPrecision(18, 4);
        }
    }

}
