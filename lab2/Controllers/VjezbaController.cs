using Microsoft.AspNetCore.Mvc;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Controllers;

public class VjezbaController : Controller
{
    private readonly List<Vjezba> _vjezbe;

    public VjezbaController(List<Vjezba> vjezbe)
    {
        _vjezbe = vjezbe;
    }

    public IActionResult Index()
    {
        return View(_vjezbe);
    }

    public IActionResult Details(int id)
    {
        var vjezba = _vjezbe.FirstOrDefault(v => v.Id == id);
        if (vjezba is null)
        {
            return NotFound();
        }

        return View(vjezba);
    }
}
