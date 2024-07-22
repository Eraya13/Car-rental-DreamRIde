using Autopujcovna_DreamRide.Data;
using Microsoft.EntityFrameworkCore;            // všechny tyto knihovny musí být přítomny!!!
using Microsoft.AspNetCore.Identity;
using Autopujcovna_DreamRide.Models;

namespace Autopujcovna_DreamRide
{
    public class Program
    {
        /// <summary>
        /// Hlavní metoda celého programu - zde se inicializují všechny služby (services) včetně konfigurací a scopů
        /// </summary>
        public static async Task Main(string[] args)
        {
            /// <summary>
            /// <param name="builder">WebApplicationBuilder, který slouží ke konfiguraci jakýkoliv služeb pro aplikaci.</param>
            /// </summary
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(); // Přidá služby pro Controllery do builderu aplikace

            /// <summary>
            /// Konfigurace DbContextu, který čerpá svá data z připojené databáze
            /// Namapování databáze z <param name="ConnectionString">Určuje základní nastavení připojení</param> ze souboru appsettings.json
            /// builder.Configuration.GetConnectionString("DatabaseConnection") vyhledává sekci "ConnectionStrings" v konfiguračním souboru
            /// a načte hodnotu přiřazenou klíči DatabaseConnection".
            /// </summary>
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));            // konfigurace DBContextu, aby použil SQL Server jako databázového poskytovatele

            /// <summary>
            /// Konfiguruje služby ASP.NET Core Identity pomocí návrhového vzoru Dependency Injection.
            /// </summary>
            /// <param name="builder">WebApplicationBuilder použitý ke konfiguraci služeb.</param>
            /// <remarks>
            /// Požadavky na heslo:
            /// - Minimální délka: 8 znaků
            /// - Vyžadovat nealfanumerické znaky: false
            /// </remarks>
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {        // bezpečnostní požadavky na heslo
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
              .AddEntityFrameworkStores<AppDbContext>();        // uloží vytvořenou Entitu (uživatele) do databáze skrze AppDbContext


            /// <summary>
            /// Nastavení povolených uživatelských jmen z appsettings.json a jejich přidání do služeb pomocí Dependency Injection.
            /// </summary>
            /// <param name="builder">WebApplicationBuilder použitý ke konfiguraci služeb.</param>
            string? adminUsername = builder.Configuration.GetSection("AllowedUsernames")["Admin"];
            string? managerUsername = builder.Configuration.GetSection("AllowedUsernames")["Manager"];

            /* Debugging print uživatelských jmen pro kontrolu
            if (adminUsername != null && managerUsername != null)
            {
                Console.WriteLine($"Admin username: {adminUsername}");
                Console.WriteLine($"Manager username: {managerUsername}");
                // Přidání povolených uživatelských jmen do služeb pro Dependency Injection
                builder.Services.AddSingleton(provider => new AllowedUsernames
                {
                    AdminUsername = adminUsername,
                    ManagerUsername = managerUsername
                });
            }*/

            /// <summary>
            /// Postavení aplikace pomocí builderu.
            /// </summary>
            var app = builder.Build();

            /// <summary>
            /// Konfiguruje HTTP request pipeline.
            /// </summary>
            /// <param name="app">IApplicationBuilder použitý ke konfiguraci middleware.</param>
            if (!app.Environment.IsDevelopment())
            {
                // Nastaví stránku pro zpracování výjimek a povolí HSTS v produkčním prostředí.
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();  // Přesměruje HTTP požadavky na HTTPS.
            app.UseStaticFiles();       // Umožňuje obsluhu statických souborů.
            app.UseRouting();           // Přidává middleware pro směrování.
            app.UseAuthorization();     // Přidává middleware pro autorizaci.

            // Mapuje výchozí trasu pro kontrolery.
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            /// <summary>
            /// Konfigurace scopu pro uživatele admin a manager.
            /// </summary>
            using (IServiceScope scope = app.Services.CreateScope())    // vytvoření vlastního Scopu (služba s životností rámce, potom neplatná)
                                                                        // při požádání se vytvoří instance...
            {
                // Získání RoleManager a UserManagera služby z kontejneru služeb.
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Vytvoření rolí, pokud neexistují
                await EnsureRoleExists(roleManager, UserRoles.Admin);
                await EnsureRoleExists(roleManager, UserRoles.RequestManager);
            }

            app.Run();      // spuštění aplikace
        }

        /// <summary>
        /// Vytvoří roli, pokud neexistuje.
        /// </summary>
        /// <param name="roleManager">Správce rolí použitý k vytváření a správě rolí.</param>
        /// <param name="roleName">Název role, kterou je třeba zajistit.</param>
        private static async Task EnsureRoleExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}

