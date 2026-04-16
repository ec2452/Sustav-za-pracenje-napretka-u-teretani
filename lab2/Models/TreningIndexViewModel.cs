using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Models;

public class TreningIndexViewModel
{
    public List<Trening> Treninzi { get; set; } = new();
    public List<TreningUserOption> AvailableUsers { get; set; } = new();

    public int TotalCount { get; set; }
    public int TotalMinutes { get; set; }
    public int TotalCalories { get; set; }
    public double AverageDuration { get; set; }
    public double? AverageRating { get; set; }

    public int? SelectedUserId { get; set; }
    public VrstaTreninga? SelectedVrsta { get; set; }
    public int SelectedPeriod { get; set; }
    public string SelectedSort { get; set; } = "date_desc";
}

public class TreningUserOption
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int TrainingCount { get; set; }
}
