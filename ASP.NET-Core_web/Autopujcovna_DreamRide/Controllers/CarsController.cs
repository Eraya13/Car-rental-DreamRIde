using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Autopujcovna_DreamRide.Data;
using Autopujcovna_DreamRide.Models;
using Autopujcovna_DreamRide.Models.ViewModels;

namespace Autopujcovna_DreamRide.Controllers
{
    /// <summary>
    /// Controller pro správu aut.
    /// </summary>
    [Authorize(Roles = UserRoles.Admin)]        // Užívám zde UserRoles, kdy jen Admin má přístup ke všem metodám CarsControlleru
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Inicializuje novou instanci <see cref="CarsController"/> s poskytnutým kontextem databáze.
        /// </summary>
        /// <param name="context">Instance kontextu databáze.</param>
        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Zobrazuje seznam všech aut - tedy aktuální nabídku aut půjčovny
        /// </summary>
        /// <returns>View se seznamem aut.</returns>
        // GET: Cars
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.ToListAsync(); // získání všech záznamů (aut) z tabulky Cars
            var briefCarViewModels = new List<BriefCarViewModel>();

            // Získává na základě id z potřebného záznamu z tabulky [Cars] hodnoty atributů, které jsou nutné pro správné fungování a zobrazování jednotlivých aut v nabídce aut
            foreach (var car in cars)
            {
                var briefCarViewModel = new BriefCarViewModel(
                        car.Id,
                        car.Label,
                        car.Model,
                        car.EngineType,
                        car.EngineDisplacement,
                        car.PowerInKw,
                        car.Transmission,
                        car.DriveTrain,
                        car.TitleCarImage
                    );

                briefCarViewModels.Add(briefCarViewModel);      // přidání auta do Listu, který se bude vypisovat v Indexu -> nabídce aut
            }
            return View(briefCarViewModels);
        }
        /// <summary>
        /// Zobrazuje detaily konkrétního auta.
        /// </summary>
        /// <param name="id">ID auta.</param>
        /// <returns>View s detaily auta.</returns>
        // GET: Cars/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(model => model.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            // Získává na základě id z potřebného záznamu z tabulky [Cars] hodnoty atributů, které jsou nutné pro správné fungování a zobrazování CarDetailsViewModelu
            var CarDetail = new CarDetailViewModel(car.Id, car.Label, car.Model, car.TopSpeedKmForHour, car.PowerInKw,
                        car.Transmission, car.YearOfManufacture, car.EngineDisplacement, car.EngineType, car.DriveTrain, car.Fuel, car.Body, car.TitleCarImage);

            return View(CarDetail);
        }

        /// <summary>
        /// Zobrazuje formulář pro vytvoření nového auta.
        /// </summary>
        /// <returns>View s formulářem pro vytvoření nového auta.</returns>
        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Zpracovává POST požadavek pro vytvoření nového auta.
        /// </summary>
        /// <param name="car">Model nového auta.</param>
        /// <returns>View s formulářem pro vytvoření nového auta, pokud model není platný; jinak přesměruje na indexovou stránku.</returns>
        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Label,Model,YearOfManufacture,Body,Fuel,TopSpeedKmForHour,EngineType,EngineDisplacement,PowerInKw,DriveTrain,Transmission,TitleCarImage")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);                           // vytvoří záznam pro databázi
                await _context.SaveChangesAsync();          // uloží nový záznam do databáze
                return RedirectToAction(nameof(Index));    // přesměruje uživatele na domovskou stránku
            }
            return View(car);
        }

        /// <summary>
        /// Zobrazuje formulář pro úpravu existujícího auta.
        /// </summary>
        /// <param name="id">ID auta.</param>
        /// <returns>View s formulářem pro úpravu auta.</returns>
        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        /// <summary>
        /// Zpracovává POST požadavek pro úpravu existujícího auta.
        /// </summary>
        /// <param name="id">ID auta.</param>
        /// <param name="car">Model upraveného auta.</param>
        /// <returns>View s formulářem pro úpravu auta, pokud model není platný; jinak přesměruje na indexovou stránku.</returns>
        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Label,Model,YearOfManufacture,Body,Fuel,TopSpeedKmForHour,EngineType,EngineDisplacement,PowerInKw,Transmission,DriveTrain,TitleCarImage")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        /// <summary>
        /// Zobrazuje stránku pro smazání konkrétního auta.
        /// </summary>
        /// <param name="id">ID auta.</param>
        /// <returns>View s potvrzením smazání auta.</returns>
        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (car == null)
            {
                return NotFound();
            }

            var briefCarInfo = new BriefCarViewModel (car.Id, car.Label, car.Model, car.EngineType,
                        car.EngineDisplacement, car.PowerInKw, car.Transmission, car.DriveTrain, car.TitleCarImage);

            return View(briefCarInfo);
        }

        /// <summary>
        /// Zpracovává POST požadavek pro potvrzení smazání auta.
        /// </summary>
        /// <param name="id">ID auta.</param>
        /// <returns>Přesměruje na indexovou stránku po smazání auta.</returns>
        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// Kontroluje, zda auto s daným ID existuje v databázi.
        /// </summary>
        /// <param name="id">ID auta.</param>
        /// <returns>True, pokud auto existuje; jinak False.</returns>
        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
