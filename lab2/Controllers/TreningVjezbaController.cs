using Microsoft.AspNetCore.Mvc;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Controllers;

public class TreningVjezbaController : Controller
{
    private readonly List<Korisnik> _korisnici;

    public TreningVjezbaController(List<Korisnik> korisnici)
    {
        _korisnici = korisnici;
    }

    public IActionResult Index()
    {
        var treningVjezbe = _korisnici
            .SelectMany(k => k.Treninzi)
            .SelectMany(t => t.TreningVjezbe.Select(tv =>
            {
                tv.Trening = t;
                return tv;
            }))
            .ToList();

        return View(treningVjezbe);
    }

    public IActionResult Details(int id)
    {
        var treningVjezba = _korisnici
            .SelectMany(k => k.Treninzi)
            .SelectMany(t => t.TreningVjezbe.Select(tv =>
            {
                tv.Trening = t;
                return tv;
            }))
            .FirstOrDefault(tv => tv.Id == id);

        if (treningVjezba is null)
        {
            return NotFound();
        }

        return View(treningVjezba);
    }
}
