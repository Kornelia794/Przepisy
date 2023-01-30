using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zpnet.Models;

namespace zpnet.Controllers
{
    public class PrzepisyController : Controller
    {
        private readonly Models.zpnetContext _context;

        public PrzepisyController(zpnetContext context)
        {
            _context = context;
        }

        // GET: PrzepisyController
        public async Task<IActionResult> IndexAsync()
        {


    
            return View(await _context.Przepis.Include(K=>K.ksiazki).Include(K => K.autor).ToListAsync());
            //return View ();
        }
        public async Task<IActionResult> szczegoly(int id = 0)
        {
            if(id == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            var przepis = await _context.Przepis.Include(k => k.ksiazki).Include(k => k.autor).Where(k => k.Id == id).FirstAsync();

            ViewData["Count"] = przepis.ksiazki.Count;
            ViewData["przepis"] = przepis;
            return View();
        }

        // GET: PrzepisyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrzepisyController/Create
        public IActionResult Create()
        {

            pobierzListeKsiazek();
            ViewData["autorzy"] = new SelectList(_context.Autorzy, "id", "imieNazwisko");
            ViewData["ksiazkaID"] = new SelectList(_context.Ksiazka, "id", "nazwa", "przepisText"); ;
            //getKsiazki();
            return View();
        }
        private void pobierzListeKsiazek(int? id = 0)
        {
            var CoursesAll = _context.Ksiazka;
            ViewData["ksiazkiLista"] = CoursesAll;

        }
        // POST: PrzepisyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("nazwa,typ,ksiazkaID,autorid,przepisText")] przepis przepis)
        {
            try
            {
      
            
                _context.Add(przepis);
                /* await _context.SaveChangesAsync();
                 var kContent = await _context.Ksiazka.Include(k => k.przepisy).SingleAsync(k => k.id == przepis.ksiazkaID);
                 var kbubu = await _context.Przepis.ToListAsync();
                 kContent.przepisy.Add(kbubu[0]);

                 _context.Update(kContent);
                 await _context.SaveChangesAsync();*/

                await _context.SaveChangesAsync();
                var WybraneKsiazki=  HttpContext.Request.Form["selectedCourses"];
                
                foreach (var k in WybraneKsiazki)
                {
                    var kContent = await _context.Ksiazka.Include(k => k.przepisy).SingleAsync(k2 => k2.id == int.Parse(k));
                    kContent.przepisy.Add(przepis);
                    _context.Update(kContent);
                    await _context.SaveChangesAsync();
                }
              
               
               await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PrzepisyController/Edit/5
        public async Task<ActionResult> edycja(int id)
        {
            var przepis = await _context.Przepis.Include(k => k.ksiazki).SingleAsync(k => k.Id == id);
            var listaKsiazek = _context.Ksiazka;
            var wybraneKsiazki = new List<ksiazka>();
            foreach (var ksiazki in listaKsiazek)
            { 
                foreach (var k in przepis.ksiazki)
                {
                    if (ksiazki.id == k.id)
                    { 
                        ksiazki.zaznaczone = true;
                        }

                    }
                }

            ViewData["autorzy"] = new SelectList(_context.Autorzy, "id", "imieNazwisko");
            ViewData["ksiazkiLista"] = listaKsiazek;
            return View(przepis);

        }

        // POST: PrzepisyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> edycja(int id, [Bind("Id,nazwa,typ,przepisText,autorid")] przepis przepis)
        {


             try
                    {
                _context.Update(przepis);
                await _context.SaveChangesAsync();
                var przepisNew = await _context.Przepis.Include(k => k.ksiazki).SingleAsync(k => k.Id == przepis.Id);
                var WybraneKsiazki = HttpContext.Request.Form["zaznaczonePrzepisy"];
                if (przepisNew.ksiazki != null) {
                    przepisNew.ksiazki.Clear();
                } else {
                    przepisNew.ksiazki = new List<ksiazka>(); 
                };
                _context.Update(przepisNew);
                await _context.SaveChangesAsync();
                
                foreach (var k in WybraneKsiazki)
                {   
                    var kContent = await _context.Ksiazka.Include(k => k.przepisy).SingleAsync(k2 => k2.id == int.Parse(k));
                    kContent.przepisy.Add(przepis);
                    _context.Update(kContent);
                    await _context.SaveChangesAsync();
                }
                

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
              }
             catch
               {
                    return View();
                }
        }

   


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        { 

            if (_context.Ksiazka == null)
            {
                return Problem("Entity set 'zpnetContext.Student'  is null.");
            }
            var przepis = await _context.Przepis.FindAsync(id);
            if (przepis != null)
            {
                _context.Przepis.Remove(przepis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

        private void getKsiazki()
        {
            /*
            var CoursesAll = _context.Ksiazka;
            var ksiazkaLista = new List<ksiazka>();
            var k = new ksiazka();
            k.nazwa = "Korneliia";
            ksiazkaLista.Add(k);
           */
            ViewData["ksiazkaID"] = new SelectList(_context.Ksiazka, "Id", "Nazwa"); ;

        }

    }
}
