
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EinnahmeAusgabe.Models;
using EinnahmeAusgabe.Data;

public class TransaktionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransaktionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TRANSAKTIONS
    //Index 
    public async Task<IActionResult> Index(
    string suche,
    DateTime? datumVon,
    DateTime? datumBis,
    string sortierung,
     int pageSize = 10)
    {
        var transaktionen = _context.Transaktionen
            .Include(t => t.Kategorie)
            .AsQueryable();

        // Suche nach Bezeichnung
        if (!string.IsNullOrWhiteSpace(suche))
        {
            transaktionen = transaktionen.Where(t =>
                t.Bezeichnung.Contains(suche));
        }

        // Datum von
        if (datumVon.HasValue)
        {
            transaktionen = transaktionen.Where(t =>
                t.Erstellungsdatum >= datumVon.Value);
        }

        // Datum bis
        if (datumBis.HasValue)
        {
            var datumBisEnde = datumBis.Value.Date.AddDays(1);

            transaktionen = transaktionen.Where(t =>
                t.Erstellungsdatum < datumBisEnde);
        }

        // Sortierung
        switch (sortierung)
        {
            case "bezeichnung_auf":
                transaktionen = transaktionen
                    .OrderBy(t => t.Bezeichnung);
                break;

            case "bezeichnung_ab":
                transaktionen = transaktionen
                    .OrderByDescending(t => t.Bezeichnung);
                break;

            case "datum_alt":
                transaktionen = transaktionen
                    .OrderBy(t => t.Erstellungsdatum);
                break;

            case "datum_neu":
                transaktionen = transaktionen
                    .OrderByDescending(t => t.Erstellungsdatum);
                break;

            case "betrag_klein":
                transaktionen = transaktionen
                    .OrderBy(t => t.Betrag);
                break;

            case "betrag_gross":
                transaktionen = transaktionen
                    .OrderByDescending(t => t.Betrag);
                break;

            default:
                transaktionen = transaktionen
                    .OrderByDescending(t => t.Erstellungsdatum);
                break;
        }

            if (pageSize != 10 && pageSize != 25 && pageSize != 50 && pageSize != 0)
            {
            pageSize = 10;
            }

            if (pageSize > 0)
            {
            transaktionen = transaktionen.Take(pageSize);
            }
        return View(await transaktionen.ToListAsync());
    }
    // GET: TRANSAKTIONS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaktion = await _context.Transaktionen
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transaktion == null)
        {
            return NotFound();
        }

        return View(transaktion);
    }

    // GET: TRANSAKTIONS/Create
    public IActionResult Create()
    {
        ViewData["KategorieId"] = new SelectList(
       _context.Kategorien,
       "Id",
       "Name");
        return View();
    }

    // POST: TRANSAKTIONS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    // POST: TRANSAKTIONS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Bezeichnung,Betrag,Erstellungsdatum,Typ,KategorieId")] Transaktion transaktion)
    {
        if (transaktion.Erstellungsdatum > DateTime.Now)
        {
            ModelState.AddModelError(
                "Erstellungsdatum",
                "Das Erstellungsdatum darf nicht in der Zukunft liegen.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(transaktion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["KategorieId"] = new SelectList(
            _context.Kategorien,
            "Id",
            "Name",
            transaktion.KategorieId);

        return View(transaktion);
    }

    // GET: TRANSAKTIONS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaktion = await _context.Transaktionen.FindAsync(id);

        if (transaktion == null)
        {
            return NotFound();
        }

        ViewData["KategorieId"] = new SelectList(
            _context.Kategorien,
            "Id",
            "Name",
            transaktion.KategorieId);

        return View(transaktion);
    }

    // POST: TRANSAKTIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Bezeichnung,Betrag,Erstellungsdatum,Typ,KategorieId")] Transaktion transaktion)
    {
        if (transaktion.Erstellungsdatum > DateTime.Now)
        {
            ModelState.AddModelError(
                "Erstellungsdatum",
                "Das Erstellungsdatum darf nicht in der Zukunft liegen.");
        }
        if (id != transaktion.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(transaktion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaktionExists(transaktion.Id))
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
        return View(transaktion);
    }

    // GET: TRANSAKTIONS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaktion = await _context.Transaktionen
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transaktion == null)
        {
            return NotFound();
        }

        return View(transaktion);
    }

    // POST: TRANSAKTIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var transaktion = await _context.Transaktionen.FindAsync(id);
        if (transaktion != null)
        {
            _context.Transaktionen.Remove(transaktion);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransaktionExists(int? id)
    {
        return _context.Transaktionen.Any(e => e.Id == id);
    }
}
