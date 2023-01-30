using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zpnet.Models;
namespace zpnet.Controllers
{
    public class KsiazkaController : Controller
    {
        private readonly zpnetContext _context;

        public KsiazkaController(zpnetContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var data = await _context.Ksiazka.Include(k => k.przepisy).Include(k=>k.autor).ToListAsync();
            var dataLength = data.Count;
            ViewData["dataLength"] = dataLength;
            return View(data);
           //return View ();
        }



        public async Task<IActionResult> szczegoly(int id = 0)
        {
            var ksiazka = await _context.Ksiazka.Include(k=>k.przepisy).Include(k=>k.autor).Where(k=>k.id == id).FirstAsync();
            ViewData["ksiazka"] = ksiazka;
            return View();
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["autorzy"] = new SelectList(_context.Autorzy, "id", "imieNazwisko");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("autorid,nazwa")] ksiazka ksiazka)
        {
            _context.Add(ksiazka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 

            if (_context.Ksiazka == null)
            {
                return Problem("Entity set 'zpnetContext.Student'  is null.");
            }
            var ksiazka = await _context.Ksiazka.FindAsync(id);
            if (ksiazka != null)
            {
                _context.Ksiazka.Remove(ksiazka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        // GET: PrzepisyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var przepis = await _context.Ksiazka.SingleAsync(k => k.id == id);
            ViewData["autorzy"] = new SelectList(_context.Autorzy, "id", "imieNazwisko");
            return View(przepis);

        }

        // POST: PrzepisyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("id,nazwa,autorid")] ksiazka ksiazka)
        {
            try
            {
                _context.Update(ksiazka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }



}
