using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sustavzapracenjenapretkauteretani.Models;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<Vjezba> _vjezbe;
        private readonly List<Plan> _planovi;
        private readonly List<Korisnik> _korisnici;

        public HomeController(ILogger<HomeController> logger, List<Vjezba> vjezbe, List<Plan> planovi, List<Korisnik> korisnici)
        {
            _logger = logger;
            _vjezbe = vjezbe;
            _planovi = planovi;
            _korisnici = korisnici;
        }

        public IActionResult Index()
        {
            return View(CreateModel());
        }

        private HomeDashboardViewModel CreateModel()
        {
            var treninzi = _korisnici.SelectMany(k => k.Treninzi).ToList();
            var ciljevi = _korisnici.SelectMany(k => k.Ciljevi).ToList();
            var mjerenja = _korisnici.SelectMany(k => k.Mjerenja).ToList();

            return new HomeDashboardViewModel
            {
                VjezbeCount = _vjezbe.Count,
                PlanoviCount = _planovi.Count,
                KorisniciCount = _korisnici.Count,
                TreninziCount = treninzi.Count,
                CiljeviCount = ciljevi.Count,
                MjerenjaCount = mjerenja.Count
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
