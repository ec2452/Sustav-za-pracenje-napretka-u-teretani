using Teretana.Models;

// ==== GLAVNI SETUP APLIKACIJE ====
// Ovdje kreiramo test podatke i LINQ upite za sustav praćenja napretka u teretani

var builder = WebApplication.CreateBuilder(args);

// ==== 1. KATALOG VJEŽBI - 7 vježbi ====
var benchPress = new Vjezba
{
    Id = 1,
    Naziv = "Bench Press",
    Opis = "Klasicna potisak s klupe za razvoj prsa i tricepsa.",
    MisicnaSkupina = MisicnaSkupina.Prsa,
    VideoUrl = "https://example.com/bench",
    RazinaTezine = 3,
    PotrebnaOprema = true
};

var deadlift = new Vjezba
{
    Id = 2,
    Naziv = "Mrtvo dizanje",
    Opis = "Slozena vjezba koja aktivira cijelo tijelo s naglaskom na donja ledja.",
    MisicnaSkupina = MisicnaSkupina.CijeloTijelo,
    VideoUrl = "https://example.com/deadlift",
    RazinaTezine = 4,
    PotrebnaOprema = true
};

var barbellRow = new Vjezba
{
    Id = 3,
    Naziv = "Veslanje u pretklonu",
    Opis = "Vjezba za ledja s naglaskom na srednji dio trapeza.",
    MisicnaSkupina = MisicnaSkupina.Leda,
    VideoUrl = "https://example.com/row",
    RazinaTezine = 3,
    PotrebnaOprema = true
};

var treadmillRun = new Vjezba
{
    Id = 4,
    Naziv = "Trcanje na traci",
    Opis = "Kardio aktivnost za poboljsanje izdrzljivosti i potrosnju kalorija.",
    MisicnaSkupina = MisicnaSkupina.Kardio,
    VideoUrl = "https://example.com/treadmill",
    RazinaTezine = 2,
    PotrebnaOprema = true
};

var plank = new Vjezba
{
    Id = 5,
    Naziv = "Plank",
    Opis = "Staticka vjezba za aktivaciju core muskulature.",
    MisicnaSkupina = MisicnaSkupina.Trbuh,
    VideoUrl = "https://example.com/plank",
    RazinaTezine = 2,
    PotrebnaOprema = false
};

var kettlebellSwing = new Vjezba
{
    Id = 6,
    Naziv = "Kettlebell swing",
    Opis = "Dinamicno zamahivanje za eksplozivnost kukova i kardio efekt.",
    MisicnaSkupina = MisicnaSkupina.CijeloTijelo,
    VideoUrl = "https://example.com/swing",
    RazinaTezine = 3,
    PotrebnaOprema = true
};

var mobilityFlow = new Vjezba
{
    Id = 7,
    Naziv = "Mobility flow",
    Opis = "Sekvenca mobilizacijskih pokreta za ramena i kukove.",
    MisicnaSkupina = MisicnaSkupina.CijeloTijelo,
    VideoUrl = "https://example.com/mobility",
    RazinaTezine = 2,
    PotrebnaOprema = false
};

var katalogVjezbi = new List<Vjezba>
{
    benchPress,
    deadlift,
    barbellRow,
    treadmillRun,
    plank,
    kettlebellSwing,
    mobilityFlow
};

// ==== 2. PLANOVI TRENINGA - 3 plana za različite razine ====
var planSnaga = new Plan
{
    Id = 1,
    Naziv = "Snaga i masa",
    Opis = "Program od 12 tjedana za razvoj baze snage i misicne mase.",
    TrajanjeTjedana = 12,
    Razina = RazinaIskustva.Srednji,
    Vjezbe = new List<Vjezba> { benchPress, deadlift, barbellRow }
};

var planKardio = new Plan
{
    Id = 2,
    Naziv = "Kardio reset",
    Opis = "Plan s naglaskom na poticanje metabolizma i povecanje VO2 max.",
    TrajanjeTjedana = 8,
    Razina = RazinaIskustva.Pocetnik,
    Vjezbe = new List<Vjezba> { treadmillRun, kettlebellSwing, plank }
};

var planCore = new Plan
{
    Id = 3,
    Naziv = "Core & mobility",
    Opis = "Sestotjedni plan za stabilizaciju trupa i veci opseg pokreta.",
    TrajanjeTjedana = 6,
    Razina = RazinaIskustva.Napredni,
    Vjezbe = new List<Vjezba> { plank, mobilityFlow, kettlebellSwing }
};

var planovi = new List<Plan> { planSnaga, planKardio, planCore };

// ==== 3. TRENINZI - 3 treninga (po jedan za svakog korisnika) ====
var anaTrening = new Trening
{
    Id = 1,
    DatumVrijeme = DateTime.Today.AddDays(-2).AddHours(18),
    TrajanjeMinuta = 75,
    Vrsta = VrstaTreninga.Snaga,
    Biljeske = "Fokus na prsa i ledja.",
    PotroseneKalorije = 520,
    Ocjena = 8,
    KorisnikId = 1,
    TreningVjezbe = new List<TreningVjezba>
    {
        new TreningVjezba
        {
            Id = 1,
            TreningId = 1,
            VjezbaId = benchPress.Id,
            Vjezba = benchPress,
            BrojSerija = 4,
            BrojPonavljanja = 8,
            TezinaKg = 45,
            OdmorSekundi = 120,
            Redoslijed = 1
        },
        new TreningVjezba
        {
            Id = 2,
            TreningId = 1,
            VjezbaId = barbellRow.Id,
            Vjezba = barbellRow,
            BrojSerija = 4,
            BrojPonavljanja = 10,
            TezinaKg = 35,
            OdmorSekundi = 90,
            Redoslijed = 2
        }
    }
};

var markoTrening = new Trening
{
    Id = 2,
    DatumVrijeme = DateTime.Today.AddDays(-1).AddHours(7),
    TrajanjeMinuta = 50,
    Vrsta = VrstaTreninga.Kardio,
    Biljeske = "Intervalli od 400 m.",
    PotroseneKalorije = 430,
    Ocjena = 7,
    KorisnikId = 2,
    TreningVjezbe = new List<TreningVjezba>
    {
        new TreningVjezba
        {
            Id = 3,
            TreningId = 2,
            VjezbaId = treadmillRun.Id,
            Vjezba = treadmillRun,
            BrojSerija = 6,
            BrojPonavljanja = 1,
            TezinaKg = null,
            OdmorSekundi = 60,
            Redoslijed = 1
        },
        new TreningVjezba
        {
            Id = 4,
            TreningId = 2,
            VjezbaId = kettlebellSwing.Id,
            Vjezba = kettlebellSwing,
            BrojSerija = 5,
            BrojPonavljanja = 15,
            TezinaKg = 20,
            OdmorSekundi = 45,
            Redoslijed = 2
        }
    }
};

var lanaTrening = new Trening
{
    Id = 3,
    DatumVrijeme = DateTime.Today.AddDays(-3).AddHours(19),
    TrajanjeMinuta = 60,
    Vrsta = VrstaTreninga.Mobility,
    Biljeske = "Rad na stabilnosti lopatica.",
    PotroseneKalorije = 320,
    Ocjena = 9,
    KorisnikId = 3,
    TreningVjezbe = new List<TreningVjezba>
    {
        new TreningVjezba
        {
            Id = 5,
            TreningId = 3,
            VjezbaId = mobilityFlow.Id,
            Vjezba = mobilityFlow,
            BrojSerija = 3,
            BrojPonavljanja = 1,
            TezinaKg = null,
            OdmorSekundi = 30,
            Redoslijed = 1
        },
        new TreningVjezba
        {
            Id = 6,
            TreningId = 3,
            VjezbaId = plank.Id,
            Vjezba = plank,
            BrojSerija = 4,
            BrojPonavljanja = 1,
            TezinaKg = null,
            OdmorSekundi = 45,
            Redoslijed = 2
        }
    }
};

// ==== 4. KORISNICI - 3 glavna korisnika s bogatim podacima ====
// Svaki korisnik ima: ciljeve, mjerenja, treninge
var ana = new Korisnik
{
    Id = 1,
    Ime = "Ana",
    Prezime = "Maric",
    Email = "ana.maric@example.com",
    DatumRodjenja = new DateTime(1995, 4, 12),
    DatumRegistracije = DateTime.Today.AddMonths(-4),
    Visina = 168,
    Tezina = 67,
    PlanId = planSnaga.Id,
    TrenutniPlan = planSnaga,
    Ciljevi = new List<Cilj>
    {
        new Cilj
        {
            Id = 1,
            Tip = TipCilja.PovecanjeSnage,
            CiljanaVrijednost = 120,
            Rok = DateTime.Today.AddMonths(3),
            Postignut = false,
            KorisnikId = 1
        }
    },
    Mjerenja = new List<Mjerenje>
    {
        new Mjerenje
        {
            Id = 1,
            DatumMjerenja = DateTime.Today.AddDays(-14),
            Tezina = 68,
            PostotakMasti = 24,
            OpsegStruka = 74,
            KorisnikId = 1
        },
        new Mjerenje
        {
            Id = 2,
            DatumMjerenja = DateTime.Today.AddDays(-1),
            Tezina = 67,
            PostotakMasti = 23,
            OpsegStruka = 72,
            KorisnikId = 1
        }
    },
    Treninzi = new List<Trening> { anaTrening }
};

var marko = new Korisnik
{
    Id = 2,
    Ime = "Marko",
    Prezime = "Kos",
    Email = "marko.kos@example.com",
    DatumRodjenja = new DateTime(1990, 11, 3),
    DatumRegistracije = DateTime.Today.AddMonths(-8),
    Visina = 182,
    Tezina = 92,
    PlanId = planKardio.Id,
    TrenutniPlan = planKardio,
    Ciljevi = new List<Cilj>
    {
        new Cilj
        {
            Id = 3,
            Tip = TipCilja.Mrsavljenje,
            CiljanaVrijednost = 85,
            Rok = DateTime.Today.AddMonths(2),
            Postignut = false,
            KorisnikId = 2
        }
    },
    Mjerenja = new List<Mjerenje>
    {
        new Mjerenje
        {
            Id = 3,
            DatumMjerenja = DateTime.Today.AddDays(-21),
            Tezina = 94,
            PostotakMasti = 28,
            OpsegStruka = 98,
            KorisnikId = 2
        },
        new Mjerenje
        {
            Id = 4,
            DatumMjerenja = DateTime.Today.AddDays(-2),
            Tezina = 92,
            PostotakMasti = 26,
            OpsegStruka = 95,
            KorisnikId = 2
        }
    },
    Treninzi = new List<Trening> { markoTrening }
};

var lana = new Korisnik
{
    Id = 3,
    Ime = "Lana",
    Prezime = "Jurisic",
    Email = "lana.jurisic@example.com",
    DatumRodjenja = new DateTime(1988, 7, 22),
    DatumRegistracije = DateTime.Today.AddMonths(-1),
    Visina = 175,
    Tezina = 61,
    PlanId = planCore.Id,
    TrenutniPlan = planCore,
    Ciljevi = new List<Cilj>
    {
        new Cilj
        {
            Id = 5,
            Tip = TipCilja.Fleksibilnost,
            CiljanaVrijednost = 5,
            Rok = DateTime.Today.AddMonths(4),
            Postignut = false,
            KorisnikId = 3
        }
    },
    Mjerenja = new List<Mjerenje>
    {
        new Mjerenje
        {
            Id = 5,
            DatumMjerenja = DateTime.Today.AddDays(-10),
            Tezina = 62,
            PostotakMasti = 21,
            OpsegStruka = 68,
            KorisnikId = 3
        }
    },
    Treninzi = new List<Trening> { lanaTrening }
};

var korisnici = new List<Korisnik> { ana, marko, lana };

planSnaga.Korisnici.Add(ana);
planKardio.Korisnici.Add(marko);
planCore.Korisnici.Add(lana);
planKardio.Korisnici.Add(lana);

// ==== 5. LINQ UPITI - 8 primjera za analizu podataka ====

// UPIT 1: Korisnici s aktivnim (neostvarenim) ciljevima
var aktivniCiljeviPoKorisniku = korisnici
    .Select(k => new
    {
        Korisnik = k,
        AktivniCiljevi = k.Ciljevi
            .Where(c => !c.Postignut && c.Rok >= DateTime.Today)
            .OrderBy(c => c.Rok)
            .ToList()
    })
    .Where(result => result.AktivniCiljevi.Any())
    .ToList();

// UPIT 2: Prosječne kalorije po vrsti treninga (Snaga, Kardio, HIIT...)
var prosjecneKalorijePoVrsti = korisnici
    .SelectMany(k => k.Treninzi)
    .GroupBy(t => t.Vrsta)
    .Select(group => new
    {
        Vrsta = group.Key,
        ProsjecneKalorije = group.Average(t => t.PotroseneKalorije)
    })
    .OrderByDescending(x => x.ProsjecneKalorije)
    .ToList();

// UPIT 3: Rangiranje planova po zahtjevnosti (težina vježbi + broj korisnika)
var zahtjevniPlanovi = planovi
    .Select(p => new
    {
        Plan = p,
        ProsjecnaTezinaVjezbi = p.Vjezbe.Average(v => v.RazinaTezine),
        BrojAktivnihKorisnika = p.Korisnici.Count
    })
    .OrderByDescending(x => x.ProsjecnaTezinaVjezbi)
    .ThenByDescending(x => x.BrojAktivnihKorisnika)
    .ToList();

// UPIT 4: Napredak korisnika - promjena težine i masti kroz vrijeme
var napredakKorisnika = korisnici
    .Select(k => new
    {
        Korisnik = k.Ime + " " + k.Prezime,
        Mjerenja = k.Mjerenja.OrderBy(m => m.DatumMjerenja).ToList(),
        UkupnaPromjenaKg = k.Mjerenja.Any()
            ? k.Mjerenja.Last().Tezina - k.Mjerenja.First().Tezina
            : 0,
        UkupnaPromjenaPostotkaMasti = k.Mjerenja.Count() >= 2
            ? (k.Mjerenja.Last().PostotakMasti ?? 0) - (k.Mjerenja.First().PostotakMasti ?? 0)
            : 0
    })
    .ToList();

// UPIT 5: Top vježbe - koje se koriste najčešće u treninzima
var topVjezbe = korisnici
    .SelectMany(k => k.Treninzi)
    .SelectMany(t => t.TreningVjezbe)
    .Where(tv => tv.Vjezba != null)
    .GroupBy(tv => tv.Vjezba!.Naziv)
    .Select(group => new
    {
        VjezbaIme = group.Key,
        BrojKoristenja = group.Count(),
        ProsjecnaTezinaKg = group.Where(tv => tv.TezinaKg.HasValue)
                                 .Average(tv => tv.TezinaKg ?? 0),
        ProsjecniBrojSerija = group.Average(tv => tv.BrojSerija)
    })
    .OrderByDescending(x => x.BrojKoristenja)
    .ToList();

// UPIT 6: Vježbe organizirane po mišičnim skupinama (prsa, ledja, noge...)
var vjebePoBrojuMisicnihSkupina = katalogVjezbi
    .GroupBy(v => v.MisicnaSkupina)
    .Select(group => new
    {
        MisicnaSkupina = group.Key,
        Vjezbe = group.OrderBy(v => v.Naziv).ToList(),
        BrojVjezbi = group.Count(),
        ProsjecnaTezinaVjezbi = group.Average(v => v.RazinaTezine)
    })
    .OrderByDescending(x => x.BrojVjezbi)
    .ToList();

// UPIT 7: Planovi sortirani po razini iskustva (Početnik > Srednji > Napredni)
var planoviPoRazini = planovi
    .GroupBy(p => p.Razina)
    .Select(group => new
    {
        Razina = group.Key,
        Planovi = group.Select(p => new
        {
            NazivPlana = p.Naziv,
            TrajanjeTjedana = p.TrajanjeTjedana,
            BrojVjezbi = p.Vjezbe.Count,
            BrojKorisnika = p.Korisnici.Count
        }).ToList()
    })
    .OrderBy(x => x.Razina)
    .ToList();

// UPIT 8: Korisnici sortirani po broju treninga (aktivnost, minutu, kalorije...)
var korisnikPoAktivnosti = korisnici
    .Select(k => new
    {
        Korisnik = k.Ime + " " + k.Prezime,
        BrojTreninga = k.Treninzi.Count,
        TreninziPoVrsti = k.Treninzi
            .GroupBy(t => t.Vrsta)
            .Select(g => new { Vrsta = g.Key, Broj = g.Count() })
            .ToList(),
        UkupnoMinuta = k.Treninzi.Sum(t => t.TrajanjeMinuta),
        UkupnoKalorija = k.Treninzi.Sum(t => t.PotroseneKalorije ?? 0),
        ProsjecnaOcjena = k.Treninzi.Average(t => t.Ocjena ?? 0)
    })
    .OrderByDescending(x => x.BrojTreninga)
    .ToList();

// ==== 6. DEPENDENCY INJECTION - Registracija svih podataka ====
// Dostupni su kroz DI u controllere via konstruktora
builder.Services.AddSingleton(katalogVjezbi);
builder.Services.AddSingleton(planovi);
builder.Services.AddSingleton(korisnici);

builder.Services.AddSingleton(aktivniCiljeviPoKorisniku);
builder.Services.AddSingleton(prosjecneKalorijePoVrsti);
builder.Services.AddSingleton(zahtjevniPlanovi);
builder.Services.AddSingleton(napredakKorisnika);
builder.Services.AddSingleton(topVjezbe);
builder.Services.AddSingleton(vjebePoBrojuMisicnihSkupina);
builder.Services.AddSingleton(planoviPoRazini);
builder.Services.AddSingleton(korisnikPoAktivnosti);

builder.Services.AddControllersWithViews();

// ==== 7. BUILD I POKRETANJE APLIKACIJE ====
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
