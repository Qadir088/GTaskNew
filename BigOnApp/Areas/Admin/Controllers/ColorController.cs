using BigOnApp.DAL.context;
using BigOnApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigOnApp.Areas.Admin.Controllers;
[Area("Admin")]
public class ColorController : Controller
{
    private readonly AppDbContext _context;

    public ColorController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var colors = _context.Colors
            .Where(c => c.DeletedBy == null)
            .ToList();
        return View(colors);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Color color)
    {
        _context.Colors.Add(color);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult Detail(int id)
    {
        var color = _context.Colors.SingleOrDefault(x => x.Id == id);
        if (color == null) return NotFound();

        return View(color);
    }

    public IActionResult Edit(int id)
    {
        var color = _context.Colors.SingleOrDefault(x => x.Id == id);
        if (color == null) return NotFound();
        
        return View(color);
    }
    [HttpPost]
    public IActionResult Edit(Color color)
    {
        var dbColor = _context.Colors.Find(color.Id);
        if(dbColor == null) return NotFound();
        dbColor.Name = color.Name;
        dbColor.HasCode = color.HasCode;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var color = _context.Colors.Single(c => c.Id == id);
        if (color == null) 
            return Json(new
            {
                error = true,
                message = "Data tapilmadi"
            });
        _context.Entry(color).State = EntityState.Deleted;
        _context.SaveChanges();
        var colors = _context.Colors
            .Where(c => c.DeletedBy == null)
            .ToList();
        return PartialView("_Body",colors);

    }
}
