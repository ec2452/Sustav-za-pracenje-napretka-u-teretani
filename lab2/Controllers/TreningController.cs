using Microsoft.AspNetCore.Mvc;
using Sustavzapracenjenapretkauteretani.Models;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Controllers;

public class TreningController : Controller
{
    private readonly List<Korisnik> _korisnici;

    public TreningController(List<Korisnik> korisnici)
    {
        _korisnici = korisnici;
    }

    public IActionResult Index(int? userId, VrstaTreninga? vrsta, int period = 0, string sort = "date_desc")
    {
        var sviTreninzi = _korisnici
            .SelectMany(k => k.Treninzi.Select(t =>
            {
                t.Korisnik = k;
                return t;
            }))
            .ToList();

        var query = sviTreninzi.AsEnumerable();

        if (userId.HasValue)
        {
            query = query.Where(t => t.KorisnikId == userId.Value);
        }

        if (vrsta.HasValue)
        {
            query = query.Where(t => t.Vrsta == vrsta.Value);
        }

        if (period is 7 or 30 or 90)
        {
            var fromDate = DateTime.Now.Date.AddDays(-period);
            query = query.Where(t => t.DatumVrijeme.Date >= fromDate);
        }

        query = sort switch
        {
            "date_asc" => query.OrderBy(t => t.DatumVrijeme),
            "duration_desc" => query.OrderByDescending(t => t.TrajanjeMinuta),
            "duration_asc" => query.OrderBy(t => t.TrajanjeMinuta),
            "rating_desc" => query.OrderByDescending(t => t.Ocjena ?? 0).ThenByDescending(t => t.DatumVrijeme),
            "rating_asc" => query.OrderBy(t => t.Ocjena ?? int.MaxValue).ThenByDescending(t => t.DatumVrijeme),
            _ => query.OrderByDescending(t => t.DatumVrijeme)
        };

        var treninzi = query.ToList();

        var model = new TreningIndexViewModel
        {
            Treninzi = treninzi,
            TotalCount = sviTreninzi.Count,
            TotalMinutes = treninzi.Sum(t => t.TrajanjeMinuta),
            TotalCalories = treninzi.Sum(t => t.PotroseneKalorije ?? 0),
            AverageDuration = treninzi.Count == 0 ? 0 : Math.Round(treninzi.Average(t => t.TrajanjeMinuta), 1),
            AverageRating = treninzi.Any(t => t.Ocjena.HasValue)
                ? Math.Round(treninzi.Where(t => t.Ocjena.HasValue).Average(t => t.Ocjena!.Value), 1)
                : null,
            SelectedUserId = userId,
            SelectedVrsta = vrsta,
            SelectedPeriod = period,
            SelectedSort = sort,
            AvailableUsers = _korisnici
                .Select(k => new TreningUserOption
                {
                    Id = k.Id,
                    FullName = $"{k.Ime} {k.Prezime}",
                    TrainingCount = k.Treninzi.Count
                })
                .OrderBy(o => o.FullName)
                .ToList()
        };

        return View(model);
    }

    public IActionResult Details(int id)
    {
        var trening = _korisnici
            .SelectMany(k => k.Treninzi.Select(t =>
            {
                t.Korisnik = k;
                return t;
            }))
            .FirstOrDefault(t => t.Id == id);

        if (trening is null)
        {
            return NotFound();
        }

        return View(trening);
    }
}
