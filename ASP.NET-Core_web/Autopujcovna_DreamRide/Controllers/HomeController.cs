using Autopujcovna_DreamRide.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Autopujcovna_DreamRide.Controllers
{
    /// <summary>
    /// Controller pro hlavní sekce webové stránky imaginární autopůjčovny DreamRide.
    /// Tento kontroler obsahuje zatím jedinou metodu a to pro zobrazení domovské stránky
    /// Plánovaná funkcionalita: Budou zde jednotlivé pohledy na různé statické pohledy, které by zobrazovali obsah pro Autopůjčovnu Dream Ride
    ///     např. O nás, Naše služby, FAQ, Obchodní podmínky, Kontakt
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
