using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Autopujcovna_DreamRide.Models; 

namespace Autopujcovna_DreamRide.Data
{
    /// <summary>
    /// Databázový kontext pro aplikaci Autopujcovna DreamRide
    /// <remarks>
    /// Dědí z IdentityDbContextu, jelikož stránka využívá Dependency Injection při správě uživatelů a jejich rolí
    /// </remarks>
    /// </summary>
    public class AppDbContext : IdentityDbContext
    {
        /// <summary>
        /// Inicializuje novou instanci <see cref="AppDbContext"/> s poskytnutými možnostmi.
        /// </summary>
        /// <param name="options">Možnosti pro konfiguraci databázového kontextu.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// DbSet pro klienty.
        /// </summary>
        public DbSet<Client> Clients { get; set; }
        
        /// <summary>
        /// DbSet pro auta.
        /// </summary>
        public DbSet<Car> Cars { get; set; }

        /// <summary>
        /// DbSet pro požadavky.
        /// </summary>
        public DbSet<Request> Requests { get; set; }

    }
}
