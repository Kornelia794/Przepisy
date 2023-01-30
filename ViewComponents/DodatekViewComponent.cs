using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zpnet.Controllers;
using zpnet.Models;

namespace zpnet.ViewComponents;

public class DodatekViewComponent : ViewComponent
{
    private readonly zpnetContext _context;
    public DodatekViewComponent(zpnetContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewData["autorzy"] = (await _context.Autorzy.ToListAsync()).Count;
        ViewData["ksiazki"] = (await _context.Ksiazka.ToListAsync()).Count;
        ViewData["przepisy"] = (await _context.Przepis.ToListAsync()).Count;
        return View("Default");
    }
}