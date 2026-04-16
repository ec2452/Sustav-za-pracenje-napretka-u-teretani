using Microsoft.AspNetCore.Mvc;
using Teretana.Models;
using Sustavzapracenjenapretkauteretani.Models;

namespace Sustavzapracenjenapretkauteretani.Controllers;

public class KorisnikController : Controller
{
    private readonly List<Korisnik> _korisnici;
    private readonly List<Plan> _planovi;

    public KorisnikController(List<Korisnik> korisnici, List<Plan> planovi)
    {
        _korisnici = korisnici;
        _planovi = planovi;
    }

    public IActionResult Index()
    {
        return View(_korisnici);
    }

    public IActionResult Details(int id)
    {
        var korisnik = _korisnici.FirstOrDefault(k => k.Id == id);
        if (korisnik is null)
        {
            return NotFound();
        }

        return View(korisnik);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new KorisnikCreateViewModel
        {
            DatumRodjenja = DateTime.Today.AddYears(-25),
            DatumMjerenja = DateTime.Today,
            RokCilja = DateTime.Today.AddMonths(3),
            TipCilja = TipCilja.Mrsavljenje
        };

        PopulatePlanOptions(model);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(KorisnikCreateViewModel model)
    {
        if (model.RokCilja <= DateTime.Today)
        {
            ModelState.AddModelError(nameof(model.RokCilja), "Rok cilja mora biti u budućnosti.");
        }

        if (model.DatumMjerenja < model.DatumRodjenja)
        {
            ModelState.AddModelError(nameof(model.DatumMjerenja), "Datum mjerenja ne može biti prije datuma rođenja.");
        }

        Plan? selectedPlan = null;
        if (model.PlanId.HasValue)
        {
            selectedPlan = _planovi.FirstOrDefault(p => p.Id == model.PlanId.Value);
            if (selectedPlan is null)
            {
                ModelState.AddModelError(nameof(model.PlanId), "Odabrani plan ne postoji.");
            }
        }

        if (!ModelState.IsValid)
        {
            PopulatePlanOptions(model);
            return View(model);
        }

        var korisnikId = _korisnici.Count == 0 ? 1 : _korisnici.Max(k => k.Id) + 1;
        var ciljId = _korisnici.SelectMany(k => k.Ciljevi).DefaultIfEmpty().Max(c => c?.Id ?? 0) + 1;
        var mjerenjeId = _korisnici.SelectMany(k => k.Mjerenja).DefaultIfEmpty().Max(m => m?.Id ?? 0) + 1;

        var korisnik = new Korisnik
        {
            Id = korisnikId,
            Ime = model.Ime.Trim(),
            Prezime = model.Prezime.Trim(),
            Email = model.Email.Trim(),
            DatumRodjenja = model.DatumRodjenja,
            DatumRegistracije = DateTime.Now,
            Visina = model.Visina,
            Tezina = model.Tezina,
            PlanId = model.PlanId,
            TrenutniPlan = selectedPlan
        };

        var cilj = new Cilj
        {
            Id = ciljId,
            Tip = model.TipCilja,
            CiljanaVrijednost = model.CiljanaVrijednost,
            Rok = model.RokCilja,
            Postignut = false,
            KorisnikId = korisnikId,
            Korisnik = korisnik
        };

        var mjerenje = new Mjerenje
        {
            Id = mjerenjeId,
            DatumMjerenja = model.DatumMjerenja,
            Tezina = model.MjerenjeTezina,
            PostotakMasti = model.PostotakMasti,
            OpsegStruka = model.OpsegStruka,
            KorisnikId = korisnikId,
            Korisnik = korisnik
        };

        korisnik.Ciljevi.Add(cilj);
        korisnik.Mjerenja.Add(mjerenje);
        selectedPlan?.Korisnici.Add(korisnik);

        _korisnici.Add(korisnik);

        return RedirectToAction(nameof(Details), new { id = korisnik.Id });
    }

    private void PopulatePlanOptions(KorisnikCreateViewModel model)
    {
        model.AvailablePlanovi = _planovi
            .OrderBy(p => p.Naziv)
            .Select(p => new PlanOption
            {
                Id = p.Id,
                Naziv = p.Naziv
            })
            .ToList();
    }
}
