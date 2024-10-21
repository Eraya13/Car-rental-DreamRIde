using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Autopujcovna_DreamRide.Data;
using Autopujcovna_DreamRide.Models;

namespace Autopujcovna_DreamRide.Controllers
{
    /// <summary>
    /// Controller pro správu klientů (= žadatelé o půjčení auta)
    /// Tento kontroler je momentálně stále ve vývoji a webová stránka ho nevyužívá, většina funkcí je zakomentována a nemusí fungovat
    /// Stejně tak pohledy Controlleru nejsou hotové a funkční
    /// </summary>
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        // předání reference na data z existující databáze - na tabulky
        // načtení dat z databáze pro práci s daty v databázi
        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        //// GET: Clients
        //public async Task<IActionResult> Index()    //C
        //{
        //        // předání do Viewu celé tabulky z databáze do podoby listu (nyní každý záznam můžeme přístupovat jako prvek v listu)
        //    return View(await _context.Clients.ToListAsync());  // M - Clients , List - VVV
        //}

        //// GET: Clients/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Clients
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(client);
        //}

        //// GET: Clients/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Clients/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,MobilePreset,MobileNumber")] Client client)
        //{           // Bind = proces, kterým ASP.NET MVC mapuje data z HTTTP POST požadavko na atributy objektů
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(client);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(client);
        //}

        //// GET: Clients/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Clients.FindAsync(id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(client);
        //}

        //// POST: Clients/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Email,MobilePreset,MobileNumber")] Client client)
        //{
        //    if (id != client.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(client);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ClientExists(client.Id))
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
        //    return View(client);
        //}

        //// GET: Clients/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Clients
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(client);
        //}

        //// POST: Clients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var client = await _context.Clients.FindAsync(id);
        //    if (client != null)
        //    {
        //        _context.Clients.Remove(client);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ClientExists(int id)
        //{
        //    return _context.Clients.Any(e => e.Id == id);
        //}
    }
}
