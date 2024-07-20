using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Autopujcovna_DreamRide.Data;
using Autopujcovna_DreamRide.Models;
using Autopujcovna_DreamRide.Models.ViewModels;

namespace Autopujcovna_DreamRide.Controllers
{
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.ToListAsync(); // získání všech záznamů (aut) z tabulky Cars
            var briefCarViewModels = new List<BriefCarViewModel>();

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
                        car.DriveTrain
                    );

                briefCarViewModels.Add(briefCarViewModel);
            }
            return View(briefCarViewModels);
        }

        // GET: Cars/Details/5
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

            var CarDetail = new CarDetailViewModel(car.Id, car.Label, car.Model, car.TopSpeedKmForHour, car.PowerInKw,
                        car.Transmission, car.YearOfManufacture, car.EngineDisplacement, car.EngineType, car.DriveTrain, car.Fuel, car.Body);

            return View(CarDetail);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Label,Model,YearOfManufacture,Body,Fuel,TopSpeedKmForHour,EngineType,EngineDisplacement,PowerInKw,Transmission,DriveTrain")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);          // vytvoří záznam pro databázi
                await _context.SaveChangesAsync();  // uloží nový záznam do databáze
                return RedirectToAction(nameof(Index));     // přesměruje uživatele na domovskou stránku
            }
            return View(car);
        }

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

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Label,Model,YearOfManufacture,Body,Fuel,TopSpeedKmForHour,EngineType,EngineDisplacement,PowerInKw,Transmission,DriveTrain")] Car car)
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
                        car.EngineDisplacement, car.PowerInKw, car.Transmission, car.DriveTrain);

            return View(briefCarInfo);
        }

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

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
