using Microsoft.AspNetCore.Mvc;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Controllers;

public class PlanController : Controller
{
    private readonly List<Plan> _planovi;
    private readonly List<Korisnik> _korisnici;

    public PlanController(List<Plan> planovi, List<Korisnik> korisnici)
    {
        _planovi = planovi;
        _korisnici = korisnici;
    }

    public IActionResult Index()
    {
        return View(_planovi);
    }

    public IActionResult Details(int id)
    {
        var plan = _planovi.FirstOrDefault(p => p.Id == id);
        if (plan is null)
        {
            return NotFound();
        }

        var aktivniKorisnici = _korisnici
            .Where(k => k.PlanId == plan.Id || (k.TrenutniPlan != null && k.TrenutniPlan.Id == plan.Id))
            .OrderBy(k => k.Ime)
            .ThenBy(k => k.Prezime)
            .ToList();

        plan.Korisnici.Clear();
        foreach (var korisnik in aktivniKorisnici)
        {
            plan.Korisnici.Add(korisnik);
        }

        return View(plan);
    }
}
