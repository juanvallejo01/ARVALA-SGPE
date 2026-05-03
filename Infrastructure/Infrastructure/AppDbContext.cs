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
    }

}
