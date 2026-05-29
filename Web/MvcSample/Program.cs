


using Domain;
using Infrastructure;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Services;
using Services.Automapper;
using System.Reflection;


namespace MvcSample
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            var _configuration = builder.Configuration;
            // Add services to the container.
            builder.Services.AddServices();
            builder.Services.AddRepositories(_configuration);
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            //configura user id como GUID
            builder.Services.AddIdentity<AppUser,IdentityRole>(options =>
            {
                // Configuraci�n de opciones (opcional)
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
            })

           
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/Login"; ;
                options.SlidingExpiration = true;
            });


 

            // Configuración básica de Mapster con DI
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(MappingProfile).Assembly); // Escanea perfiles IRegister

            builder.Services.AddSingleton(config);                    // Configuración global
            builder.Services.AddScoped<IMapper, ServiceMapper>();    // Registra el mapper



            //builder.Services.AddCors(p => p.AddPolicy("CORS_Policy", builder =>
            //{
            //    CorsPolicyBuilder corsPolicyBuilder = builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); //builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            //}));

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           // app.UseCors("CORS_Policy");
           
           // app.MapControllers();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedRolesAndAdminUser(services).Wait();
                SeedDemoData(services).Wait();
            }


            app.Run();
        }


        static async Task SeedDemoData(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<Infrastructure.AppDbContext>();

            // Solo sembrar si no hay datos
            if (db.Galpones.Any()) return;

            // ── Galpones ─────────────────────────────────────────────
            var g1 = new Domain.Galpon { Id = Guid.NewGuid(), Nombre = "Galpón Norte",  Descripcion = "Ponedoras de raza Hy-Line Brown, ventilación forzada" };
            var g2 = new Domain.Galpon { Id = Guid.NewGuid(), Nombre = "Galpón Sur",    Descripcion = "Ponedoras Lohmann White, sistema de jaulas enriquecidas" };
            var g3 = new Domain.Galpon { Id = Guid.NewGuid(), Nombre = "Galpón Este",   Descripcion = "Engorde Ross 308, piso de viruta, ciclo 49 días" };
            db.Galpones.AddRange(g1, g2, g3);

            // ── Lotes ─────────────────────────────────────────────────
            var hoy = DateTime.UtcNow.Date;
            var l1 = new Domain.Lote { Id = Guid.NewGuid(), GalponId = g1.Id, Raza = "Hy-Line Brown",   Proposito = "Postura",  Estado = "Activo",     FechaRecepcion = DateTime.SpecifyKind(hoy.AddDays(-90), DateTimeKind.Utc), CantidadInicial = 1200 };
            var l2 = new Domain.Lote { Id = Guid.NewGuid(), GalponId = g2.Id, Raza = "Lohmann White",   Proposito = "Postura",  Estado = "Activo",     FechaRecepcion = DateTime.SpecifyKind(hoy.AddDays(-60), DateTimeKind.Utc), CantidadInicial = 950  };
            var l3 = new Domain.Lote { Id = Guid.NewGuid(), GalponId = g3.Id, Raza = "Ross 308",        Proposito = "Engorde",  Estado = "Finalizado", FechaRecepcion = DateTime.SpecifyKind(hoy.AddDays(-120),DateTimeKind.Utc), CantidadInicial = 2000 };
            var l4 = new Domain.Lote { Id = Guid.NewGuid(), GalponId = g1.Id, Raza = "ISA Brown",       Proposito = "Postura",  Estado = "Activo",     FechaRecepcion = DateTime.SpecifyKind(hoy.AddDays(-30), DateTimeKind.Utc), CantidadInicial = 800  };
            db.Lotes.AddRange(l1, l2, l3, l4);

            // ── Producción diaria ─────────────────────────────────────
            var rng = new Random(42);
            var producciones = new List<Domain.ProduccionDiaria>();

            // L1: 90 días con tasa ~85 %
            for (int d = 89; d >= 0; d--)
            {
                int aves = 1200 - (d > 70 ? 2 : d > 40 ? 1 : 0);
                int comerciales = (int)(aves * (0.82 + rng.NextDouble() * 0.08));
                producciones.Add(new Domain.ProduccionDiaria
                {
                    Id = Guid.NewGuid(), LoteId = l1.Id,
                    Fecha = DateTime.SpecifyKind(hoy.AddDays(-d), DateTimeKind.Utc),
                    HuevosAA = (int)(comerciales * 0.20),
                    HuevosA  = (int)(comerciales * 0.60),
                    HuevosAAA  = comerciales - (int)(comerciales * 0.20) - (int)(comerciales * 0.60),
                    Rotos    = rng.Next(2, 18),
                    Mortalidad = d % 15 == 0 ? 1 : 0,
                    AlimentoConsumidoKg = Math.Round(aves * 0.115m + (decimal)(rng.NextDouble() * 5), 1)
                });
            }

            // L2: 60 días con tasa ~78 %
            for (int d = 59; d >= 0; d--)
            {
                int aves = 950 - (d > 30 ? 1 : 0);
                int comerciales = (int)(aves * (0.75 + rng.NextDouble() * 0.08));
                producciones.Add(new Domain.ProduccionDiaria
                {
                    Id = Guid.NewGuid(), LoteId = l2.Id,
                    Fecha = DateTime.SpecifyKind(hoy.AddDays(-d), DateTimeKind.Utc),
                    HuevosAA = (int)(comerciales * 0.15),
                    HuevosA  = (int)(comerciales * 0.62),
                    HuevosAAA  = comerciales - (int)(comerciales * 0.15) - (int)(comerciales * 0.62),
                    Rotos    = rng.Next(1, 12),
                    Mortalidad = d % 20 == 0 ? 1 : 0,
                    AlimentoConsumidoKg = Math.Round(aves * 0.112m + (decimal)(rng.NextDouble() * 4), 1)
                });
            }

            // L4: 30 días arranque
            for (int d = 29; d >= 0; d--)
            {
                int aves = 800;
                double tasa = 0.40 + (29 - d) * 0.015; // curva de arranque
                int comerciales = (int)(aves * Math.Min(tasa, 0.80));
                producciones.Add(new Domain.ProduccionDiaria
                {
                    Id = Guid.NewGuid(), LoteId = l4.Id,
                    Fecha = DateTime.SpecifyKind(hoy.AddDays(-d), DateTimeKind.Utc),
                    HuevosAA = (int)(comerciales * 0.10),
                    HuevosA  = (int)(comerciales * 0.55),
                    HuevosAAA  = comerciales - (int)(comerciales * 0.10) - (int)(comerciales * 0.55),
                    Rotos    = rng.Next(1, 8),
                    Mortalidad = 0,
                    AlimentoConsumidoKg = Math.Round(aves * 0.110m + (decimal)(rng.NextDouble() * 3), 1)
                });
            }

            db.ProduccionesDiarias.AddRange(producciones);

            // ── Vacunaciones ─────────────────────────────────────────
            db.RegistrosVacunacion.AddRange(
                new Domain.RegistroVacunacion { Id = Guid.NewGuid(), LoteId = l1.Id, Fecha = DateTime.SpecifyKind(hoy.AddDays(-85), DateTimeKind.Utc), Vacuna = "Newcastle (HB1)",      MetodoAplicacion = "Agua de bebida",  AvesVacunadas = 1200, Observaciones = "Sin incidencias" },
                new Domain.RegistroVacunacion { Id = Guid.NewGuid(), LoteId = l1.Id, Fecha = DateTime.SpecifyKind(hoy.AddDays(-56), DateTimeKind.Utc), Vacuna = "Bronquitis Infecciosa", MetodoAplicacion = "Spray",           AvesVacunadas = 1198, Observaciones = "Leve estrés post-aplicación" },
                new Domain.RegistroVacunacion { Id = Guid.NewGuid(), LoteId = l1.Id, Fecha = DateTime.SpecifyKind(hoy.AddDays(-28), DateTimeKind.Utc), Vacuna = "Gumboro (D78)",         MetodoAplicacion = "Agua de bebida",  AvesVacunadas = 1195, Observaciones = "Refuerzo programado" },
                new Domain.RegistroVacunacion { Id = Guid.NewGuid(), LoteId = l2.Id, Fecha = DateTime.SpecifyKind(hoy.AddDays(-55), DateTimeKind.Utc), Vacuna = "Newcastle (Clone 30)",  MetodoAplicacion = "Ocular",          AvesVacunadas = 950,  Observaciones = "" },
                new Domain.RegistroVacunacion { Id = Guid.NewGuid(), LoteId = l2.Id, Fecha = DateTime.SpecifyKind(hoy.AddDays(-25), DateTimeKind.Utc), Vacuna = "Marek",                 MetodoAplicacion = "Subcutánea",      AvesVacunadas = 949,  Observaciones = "Al ingreso" },
                new Domain.RegistroVacunacion { Id = Guid.NewGuid(), LoteId = l4.Id, Fecha = DateTime.SpecifyKind(hoy.AddDays(-28), DateTimeKind.Utc), Vacuna = "Newcastle + Bronquitis", MetodoAplicacion = "Spray",          AvesVacunadas = 800,  Observaciones = "Primera dosis" }
            );

            // ── Inventario de alimento ────────────────────────────────
            db.InventariosAlimento.AddRange(
                new Domain.InventarioAlimento { Id = Guid.NewGuid(), LoteId = l1.Id, TipoAlimento = "Balanceado postura",   CantidadKg = 1850m, FechaRegistro = DateTime.SpecifyKind(hoy.AddDays(-5), DateTimeKind.Utc) },
                new Domain.InventarioAlimento { Id = Guid.NewGuid(), LoteId = l1.Id, TipoAlimento = "Suplemento cálcico",  CantidadKg = 320m,  FechaRegistro = DateTime.SpecifyKind(hoy.AddDays(-5), DateTimeKind.Utc) },
                new Domain.InventarioAlimento { Id = Guid.NewGuid(), LoteId = l2.Id, TipoAlimento = "Balanceado postura",  CantidadKg = 1200m, FechaRegistro = DateTime.SpecifyKind(hoy.AddDays(-3), DateTimeKind.Utc) },
                new Domain.InventarioAlimento { Id = Guid.NewGuid(), LoteId = l4.Id, TipoAlimento = "Balanceado arranque", CantidadKg = 2100m, FechaRegistro = DateTime.SpecifyKind(hoy.AddDays(-2), DateTimeKind.Utc) }
            );

            // ── Precios ───────────────────────────────────────────────
            if (!db.PreciosHuevo.Any())
            {
                db.PreciosHuevo.AddRange(
                    new Domain.PrecioHuevo { Id = Guid.NewGuid(), Clasificacion = "AA", PrecioUnitario = 0.28m, PrecioPorDocena = 3.10m, Descripcion = "Huevo extra grande, doble yema, calibre >73g", Disponible = true,  FechaActualizacion = DateTime.UtcNow },
                    new Domain.PrecioHuevo { Id = Guid.NewGuid(), Clasificacion = "A",  PrecioUnitario = 0.22m, PrecioPorDocena = 2.50m, Descripcion = "Huevo grande sin defectos, calibre 63-73g",    Disponible = true,  FechaActualizacion = DateTime.UtcNow },
                    new Domain.PrecioHuevo { Id = Guid.NewGuid(), Clasificacion = "AAA",  PrecioUnitario = 0.16m, PrecioPorDocena = 1.80m, Descripcion = "Huevo estándar comercial, calibre 53-63g",    Disponible = true,  FechaActualizacion = DateTime.UtcNow }
                );
            }

            await db.SaveChangesAsync();
        }


        static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            // 1. Crear roles si no existen
            string[] roles = { "Admin", "Owner", "Veterinarian", "Operator", "Customer" };

            foreach (var roleName in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Crear usuario admin si no existe
            var adminEmail = configuration["AdminUser:Email"] ?? "admin@example.com";
            var adminPassword = configuration["AdminUser:Password"] ?? "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Administrador",
                    LastName = "Del Sistema",
                    IdCardNumber = "00000000",
                    EmailConfirmed = true
                };

                var createUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createUser.Succeeded)
                {
                    await userManager.AddToRolesAsync(adminUser, roles);
                }
            }
            else
            {
                // Asegurar que tenga los roles
                var userRoles = await userManager.GetRolesAsync(adminUser);
                foreach (var role in roles.Where(role => !userRoles.Contains(role)))
                {
                    await userManager.AddToRoleAsync(adminUser, role);
                }
            }
        }


    }
}
