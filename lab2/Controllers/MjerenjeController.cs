using Microsoft.AspNetCore.Mvc;
using Teretana.Models;
using Sustavzapracenjenapretkauteretani.Models;

namespace Sustavzapracenjenapretkauteretani.Controllers;

public class MjerenjeController : Controller
{
    private readonly List<Korisnik> _korisnici;

    public MjerenjeController(List<Korisnik> korisnici)
    {
        _korisnici = korisnici;
    }

    public IActionResult Index(int? userId, string sortBy = "datumDesc")
    {
        // 1. Izvlači sve mjerenja sa Korisnik navigacijom
        var allMjerenja = _korisnici
            .SelectMany(k => k.Mjerenja.Select(m =>
            {
                m.Korisnik = k;
                return m;
            }))
            .ToList();

        // 2. Filtrira po korisniku ako je zadano
        if (userId.HasValue)
        {
            allMjerenja = allMjerenja.Where(m => m.KorisnikId == userId.Value).ToList();
        }

        // 3. Sortira po datumu (novije prvo) - default
        var sortedMjerenja = allMjerenja
            .OrderByDescending(m => m.DatumMjerenja)
            .ToList();

        // 4. Kalkulira trendove za svako mjerenje
        var mjerenjaWithTrends = sortedMjerenja.Select((mjerenje, index) =>
        {
            var userMjerenja = sortedMjerenja
                .Where(m => m.KorisnikId == mjerenje.KorisnikId)
                .OrderByDescending(m => m.DatumMjerenja)
                .ToList();

            var currentIndex = userMjerenja.IndexOf(mjerenje);
            
            double? previousWeight = null;
            double? weightChange = null;
            double? percentageChange = null;
            string trendDirection = "neutral";
            bool isFirst = currentIndex == 0 && userMjerenja.Count == 1;

            // Ako postoji prethodno mjerenje
            if (currentIndex < userMjerenja.Count - 1)
            {
                var previousMjerenje = userMjerenja[currentIndex + 1];
                previousWeight = previousMjerenje.Tezina;
                weightChange = mjerenje.Tezina - previousMjerenje.Tezina;
                percentageChange = (weightChange.Value / previousMjerenje.Tezina) * 100;

                trendDirection = weightChange > 0 ? "up" : (weightChange < 0 ? "down" : "neutral");
            }

            return new MjerenjeWithTrend
            {
                Mjerenje = mjerenje,
                PreviousWeight = previousWeight,
                WeightChange = weightChange,
                PercentageChange = percentageChange,
                TrendDirection = trendDirection,
                IsFirstMeasurement = isFirst
            };
        }).ToList();

        // 5. Sortira po korisniku pri prikazu (za vizualni groping ako potrebno)
        var groupedMjerenja = mjerenjaWithTrends
            .GroupBy(m => m.Mjerenje!.KorisnikId)
            .SelectMany(g => g)
            .ToList();

        // 6. Gradi AvailableUsers kao KorisnikOption s brojem mjerenja
        var availableUsers = _korisnici
            .Select(k => new KorisnikOption
            {
                Id = k.Id,
                FullName = $"{k.Ime} {k.Prezime}".Trim(),
                MjerenjaCount = k.Mjerenja.Count
            })
            .OrderBy(k => k.FullName)
            .ToList();

        // 7. Prosljeđuje ViewModel
        var filterName = userId.HasValue 
            ? availableUsers.FirstOrDefault(k => k.Id == userId.Value)?.FullName 
            : null;

        var viewModel = new MjerenjeIndexViewModel
        {
            Mjerenja = groupedMjerenja,
            AvailableUsers = availableUsers,
            SelectedUserId = userId,
            CurrentFilterName = filterName,
            SortBy = sortBy,
            TotalMjerenjaCount = allMjerenja.Count
        };

        return View(viewModel);
    }

    public IActionResult Details(int id)
    {
        var mjerenje = _korisnici
            .SelectMany(k => k.Mjerenja.Select(m =>
            {
                m.Korisnik = k;
                return m;
            }))
            .FirstOrDefault(m => m.Id == id);

        if (mjerenje is null)
        {
            return NotFound();
        }

        // Pronađi korisnika
        var korisnik = _korisnici.FirstOrDefault(k => k.Id == mjerenje.KorisnikId);
        if (korisnik is null)
        {
            return NotFound();
        }

        // Sve mjerenje ovog korisnika sortirane po datumu
        var userMjerenja = korisnik.Mjerenja
            .OrderByDescending(m => m.DatumMjerenja)
            .ToList();

        // Pronađi index trenutnog mjerenja
        var currentIndex = userMjerenja.FindIndex(m => m.Id == id);
        
        // Pronađi prethodno mjerenje
        var previousMjerenje = currentIndex < userMjerenja.Count - 1 
            ? userMjerenja[currentIndex + 1] 
            : null;

        // Kalkuliraj trend
        double? weightChange = null;
        double? percentageChange = null;
        string trendDirection = "neutral";
        bool isFirstMeasurement = currentIndex == userMjerenja.Count - 1;

        if (previousMjerenje != null)
        {
            weightChange = mjerenje.Tezina - previousMjerenje.Tezina;
            percentageChange = (weightChange.Value / previousMjerenje.Tezina) * 100;
            trendDirection = weightChange > 0 ? "up" : (weightChange < 0 ? "down" : "neutral");
        }

        // Pronađi relevantne ciljeve (Mrsavljenje ili DobivanjeMase)
        var relevantGoals = korisnik.Ciljevi
            .Where(c => c.Tip == TipCilja.Mrsavljenje || c.Tip == TipCilja.DobivanjeMase)
            .ToList();

        // Statistika - samo mjerenja iz ove godine
        var thisYearMjerenja = userMjerenja
            .Where(m => m.DatumMjerenja.Year == DateTime.Now.Year)
            .ToList();

        var lowestWeight = thisYearMjerenja.Count > 0 
            ? thisYearMjerenja.Min(m => m.Tezina) 
            : (double?)null;

        var highestWeight = thisYearMjerenja.Count > 0 
            ? thisYearMjerenja.Max(m => m.Tezina) 
            : (double?)null;

        var averageWeight = thisYearMjerenja.Count > 0 
            ? thisYearMjerenja.Average(m => m.Tezina) 
            : (double?)null;

        // Kreiraj ViewModel
        var viewModel = new MjerenjeDetailsViewModel
        {
            Mjerenje = mjerenje,
            PreviousMjerenje = previousMjerenje,
            WeightChange = weightChange,
            PercentageChange = percentageChange,
            TrendDirection = trendDirection,
            IsFirstMeasurement = isFirstMeasurement,
            RelevantGoals = relevantGoals,
            TotalMeasurementsThisYear = thisYearMjerenja.Count,
            LowestWeight = lowestWeight,
            HighestWeight = highestWeight,
            AverageWeight = averageWeight
        };

        return View(viewModel);
    }
}
