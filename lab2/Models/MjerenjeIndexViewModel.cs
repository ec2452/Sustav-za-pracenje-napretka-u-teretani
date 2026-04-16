using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Models
{
    /// <summary>
    /// ViewModel za Index stranicu Mjerenja s trend informacijama
    /// </summary>
    public class MjerenjeIndexViewModel
    {
        public List<MjerenjeWithTrend> Mjerenja { get; set; } = new();
        public List<KorisnikOption> AvailableUsers { get; set; } = new();
        public int? SelectedUserId { get; set; }
        public string? CurrentFilterName { get; set; }
        public string SortBy { get; set; } = "datumDesc"; // datumDesc, greatest-change, oldest
        public int TotalMjerenjaCount { get; set; }
    }

    /// <summary>
    /// Mjerenje s dodatnim trend informacijama
    /// </summary>
    public class MjerenjeWithTrend
    {
        public Mjerenje? Mjerenje { get; set; }
        public double? PreviousWeight { get; set; }
        public double? WeightChange { get; set; }
        public double? PercentageChange { get; set; }
        public string? TrendDirection { get; set; } // "up", "down", "neutral"
        public bool IsFirstMeasurement { get; set; }
        public string UserFullName => $"{Mjerenje?.Korisnik?.Ime ?? ""} {Mjerenje?.Korisnik?.Prezime ?? ""}".Trim();
    }

    /// <summary>
    /// Za filter dropdown
    /// </summary>
    public class KorisnikOption
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int MjerenjaCount { get; set; }
    }
}
