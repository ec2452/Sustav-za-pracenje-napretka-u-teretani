using Microsoft.AspNetCore.Mvc;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Controllers;

public class CiljController : Controller
{
    private readonly List<Korisnik> _korisnici;

    public CiljController(List<Korisnik> korisnici)
    {
        _korisnici = korisnici;
    }

    public IActionResult Index()
    {
        var ciljevi = _korisnici
            .SelectMany(k => k.Ciljevi.Select(c =>
            {
                c.Korisnik = k;
                return c;
            }))
            .ToList();

        return View(ciljevi);
    }

    public IActionResult Details(int id)
    {
        var cilj = _korisnici
            .SelectMany(k => k.Ciljevi.Select(c =>
            {
                c.Korisnik = k;
                return c;
            }))
            .FirstOrDefault(c => c.Id == id);

        if (cilj is null) return NotFound();

        return View(cilj);
    }
}


