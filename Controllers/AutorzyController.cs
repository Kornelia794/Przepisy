using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zpnet.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zpnet.Controllers
{
    public class AutorzyController : Controller
    {
        private readonly Models.zpnetContext _context;

        public AutorzyController(zpnetContext context)
        {
            _context = context;
        }

        // GET: PrzepisyController
        public async Task<IActionResult> IndexAsync()
        {

            return View(await _context.Autorzy.Include(k=>k.ksiazki).Include(k => k.przepisy).ToListAsync());
            //return View ();
        }
        // GET: Students/Create
        public IActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("imie,nazwisko,Data_u")] autor autor)
        {
      
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }


        // GET: PrzepisyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var autor = await _context.Autorzy.SingleAsync(k => k.id == id);
            return View(autor);

        }

        // POST: PrzepisyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("id,imie,nazwisko,Data_u")] autor autor)
        {
            try
            {
                _context.Update(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }









        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var autor = await _context.Autorzy.Include(k=>k.przepisy).Include(k=>k.ksiazki).SingleAsync(k=>k.id == id);
            if (autor != null)
            {
                _context.Autorzy.Remove(autor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
