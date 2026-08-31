using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EinnahmeAusgabe.Models;
using EinnahmeAusgabe.Data;

public class KategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public KategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Kategories
    public async Task<IActionResult> Index()
    {
        return View(await _context.Kategorien.ToListAsync());
    }

    // GET: Kategories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategorie = await _context.Kategorien
            .FirstOrDefaultAsync(m => m.Id == id);

        if (kategorie == null)
        {
            return NotFound();
        }

        return View(kategorie);
    }

    // GET: Kategories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Kategories/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,Farbe")] Kategorie kategorie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(kategorie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(kategorie);
    }

    // GET: Kategories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategorie = await _context.Kategorien.FindAsync(id);

        if (kategorie == null)
        {
            return NotFound();
        }

        return View(kategorie);
    }

    // POST: Kategories/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("Id,Name,Farbe")] Kategorie kategorie)
    {
        if (id != kategorie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(kategorie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorieExists(kategorie.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(kategorie);
    }

    // GET: Kategories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var kategorie = await _context.Kategorien
            .FirstOrDefaultAsync(m => m.Id == id);

        if (kategorie == null)
        {
            return NotFound();
        }

        return View(kategorie);
    }

    // POST: Kategories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Alle Transaktionen dieser Kategorie suchen
        var transaktionen = await _context.Transaktionen
            .Where(t => t.KategorieId == id)
            .ToListAsync();

        // Kategorie bei diesen Transaktionen entfernen
        foreach (var transaktion in transaktionen)
        {
            transaktion.KategorieId = null;
        }

        // Kategorie suchen
        var kategorie = await _context.Kategorien.FindAsync(id);

        if (kategorie != null)
        {
            _context.Kategorien.Remove(kategorie);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool KategorieExists(int? id)
    {
        return _context.Kategorien.Any(e => e.Id == id);
    }
}