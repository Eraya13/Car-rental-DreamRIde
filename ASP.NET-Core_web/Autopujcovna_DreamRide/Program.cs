using Autopujcovna_DreamRide.Data;
using Microsoft.EntityFrameworkCore;            // všechny tyto knihovny musí být přítomny!!!
using Microsoft.AspNetCore.Identity;
using Autopujcovna_DreamRide.Models;

namespace Autopujcovna_DreamRide
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Konfigurace připojení databáze
            // connectionString = bere cestu databáze z appsettings
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Konfigurace DI ASPN .NET Core Identity - userManager, signManager
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {       // zde stanovíme requirements na uživatele
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
              .AddEntityFrameworkStores<AppDbContext>();


            // Nastavení povolených uživatelských jmen z appsettings.json
            // builder vybere sekci a z ní vybere "proměnnou" - tedy její obsah
            string? adminUsername = builder.Configuration.GetSection("AllowedUsernames")["Admin"];
            string? managerUsername = builder.Configuration.GetSection("AllowedUsernames")["Manager"];

            // Vypsání uživatelských jmen pro kontrolu
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
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Konfigurace Scopu pro uživatele admin a manager
            using (IServiceScope scope = app.Services.CreateScope())    // vytvoření vlastního Scopu (služba s životností rámce, potom neplatná)
                                                                        // při požádání se vytvoří instance...
            {
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Vytvoření rolí, pokud neexistují
                await EnsureRoleExists(roleManager, UserRoles.Admin);
                await EnsureRoleExists(roleManager, UserRoles.RequestManager);
            }


            app.Run();
        }

        // Functions for role setting
        private static async Task EnsureRoleExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}

