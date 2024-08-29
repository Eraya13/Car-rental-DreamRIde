using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Autopujcovna_DreamRide.Data;
using Autopujcovna_DreamRide.Models;
using Autopujcovna_DreamRide.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

// NEZAPOMONOUT, ŽE TATO VERZE NEOBSAHUJE VŠECHNY POTŘEBNÉ COMMITY Z TÉTO VERZE NAHRAJU DO NOVÉHO REPOZITORY ZMĚNY!!!
// *** TOTO JE NEJVÍC AKTUÁLNÍ VERZE *** --> VĚCI ALE NEJSOU COMMITNUTÉ!!!


namespace Autopujcovna_DreamRide.Controllers
{
    /// <summary>
    /// Controller pro správu žádostí (requests).
    /// Tento kontroler obsahuje akce pro rezervaci auta a vytváření žádostí.
    /// Většina funkcionality je zatím ve vývoji.
    /// </summary>
    public class RequestsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Inicializuje novou instanci <see cref="RequestsController"/> s poskytnutým kontextem databáze.
        /// </summary>
        /// <param name="context">Instance kontextu databáze.</param>
        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Zobrazuje stránku pro rezervaci auta.
        /// </summary>
        /// <returns>View s formulářem pro rezervaci auta.</returns>
        public IActionResult CarReservation()
        {
            return View();
        }

        /// <summary>
        /// Zpracovává POST požadavek pro rezervaci auta.
        /// Tato metoda zatím pouze ověřuje validitu modelu.
        /// </summary>
        /// <param name="requestViewModel">Model žádosti o rezervaci auta.</param>
        /// <returns>View s formulářem pro rezervaci auta, pokud model není platný; jinak by měla přesměrovat uživatele na domovskou stránku HomeControlleru.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarReservation([Bind("Id,ClientId,CarId,Note,Name,Surname,PhonePreset,PhoneNumber,Email,PrefferedContactWay,StartDay,EndDay,AdditionalInfo")] RequestViewModel requestViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(requestViewModel);
        }


        /// <summary>
        /// Zobrazí formulář pro vytvoření nové žádosti o půjčení auta.
        /// Pro administrátory se vrátí prostý pohled "Create" = pouze holý formulář na vyplnění
        /// Pro klienty se vrátí pohled "CarReservation" 
        ///                     = pohled, kde klient má možnost si zažádat o auto skrze formulář nebo využít uvedených kontaktů (mobil. číslo a email)
        /// </summary>
        /// <returns> Pohled s formulářem pro vytvoření žádosti.</returns>
        // GET: Requests/Create
        public IActionResult Create()
        {
            var cars = _context.Cars.ToList();

            // Inicializace seznamu položek pro Dropdown výběr aut
            var carSelectionItems = new List<SelectListItem>();

            // Naplnění seznamu položek na základě aut z databáze
            foreach (var car in cars)
            {
                carSelectionItems.Add (new SelectListItem
                {
                    Value = car.Id.ToString(),              // Id = hodnota, která se odešle z formuláře na server
                    // Text, který se má zobrazit v seznamu položek Dropdownu výběru aut
                    Text = $"{car.ToString} {car.EngineType} {car.EngineDisplacement.ToString("0.0")}" 
                });
            }
            // Vytvoření ViewModelu s naplněným seznamem aut pomocí objektového inicializátoru
            var viewModel = new RequestViewModel
            {
                Cars = carSelectionItems
            };

            if (User.IsInRole("admin"))
                return View("Create");          // Pohled pro admina       

            return View("CarReservation");     // Pohled pro klienta
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,CarId,Note,PrefferedContactWay,StartDay,RentDays,Status")] Request request)
        {

            // zde přijdou konstruktoru - Klient konstruktor + Konstruktor žádosti s vybraným autem - carId = atribut
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        //// GET: Requests/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var request = await _context.Requests.FindAsync(id);
        //    if (request == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(request);
        //}

        //// POST: Requests/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,CarId,Note,PrefferedContactWay,StartDay,RentDays,Status")] Request request)
        //{
        //    if (id != request.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(request);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RequestExists(request.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(request);
        //}

        //// GET: Requests/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var request = await _context.Requests
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (request == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(request);
        //}

        //// POST: Requests/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var request = await _context.Requests.FindAsync(id);
        //    if (request != null)
        //    {
        //        _context.Requests.Remove(request);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RequestExists(int id)
        //{
        //    return _context.Requests.Any(e => e.Id == id);
        //}
    }
}