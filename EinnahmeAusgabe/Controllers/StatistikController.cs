using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EinnahmeAusgabe.Data;
using EinnahmeAusgabe.Models;

public class StatistikController : Controller
{
    private readonly ApplicationDbContext _context;

    public StatistikController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        DateTime? datumVon,
        DateTime? datumBis)
    {
        var transaktionen = _context.Transaktionen
            .AsQueryable();

        // Filter von
        if (datumVon.HasValue)
        {
            transaktionen = transaktionen.Where(t =>
                t.Erstellungsdatum >= datumVon.Value);
        }

        // Filter bis
        if (datumBis.HasValue)
        {
            var datumBisEnde = datumBis.Value.Date.AddDays(1);

            transaktionen = transaktionen.Where(t =>
                t.Erstellungsdatum < datumBisEnde);
        }

        var liste = await transaktionen.ToListAsync();

        var einnahmen = liste
            .Where(t => t.Typ == TransaktionTyp.Einnahme)
            .ToList();

        var ausgaben = liste
            .Where(t => t.Typ == TransaktionTyp.Ausgabe)
            .ToList();

        var model = new StatistikViewModel
        {
            DatumVon = datumVon,
            DatumBis = datumBis,

            SummeEinnahmen = einnahmen.Sum(t => t.Betrag),
            SummeAusgaben = ausgaben.Sum(t => t.Betrag),

            DurchschnittEinnahmen = einnahmen.Any()
                ? einnahmen.Average(t => t.Betrag)
                : 0,

            DurchschnittAusgaben = ausgaben.Any()
                ? ausgaben.Average(t => t.Betrag)
                : 0,

            TopEinnahmen = einnahmen
                .OrderByDescending(t => t.Betrag)
                .Take(5)
                .Select(t => new StatistikEintrag
                {
                    Bezeichnung = t.Bezeichnung,
                    Betrag = t.Betrag
                })
                .ToList(),

            TopAusgaben = ausgaben
                .OrderByDescending(t => t.Betrag)
                .Take(5)
                .Select(t => new StatistikEintrag
                {
                    Bezeichnung = t.Bezeichnung,
                    Betrag = t.Betrag
                })
                .ToList()
        };

        return View(model);
    }
}