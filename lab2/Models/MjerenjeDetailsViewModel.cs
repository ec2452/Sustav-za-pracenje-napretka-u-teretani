using System;
using Teretana.Models;

namespace Sustavzapracenjenapretkauteretani.Models
{
    /// <summary>
    /// ViewModel za Details stranicu mjerenja s trend analizom
    /// </summary>
    public class MjerenjeDetailsViewModel
    {
        public Mjerenje Mjerenje { get; set; }
        public Mjerenje? PreviousMjerenje { get; set; }
        
        // Trend calculation
        public double? WeightChange { get; set; }
        public double? PercentageChange { get; set; }
        public string TrendDirection { get; set; } = "neutral"; // "up", "down", "neutral"
        public bool IsFirstMeasurement { get; set; }
        
        // Related goals
        public List<Cilj> RelevantGoals { get; set; } = new();
        
        // Statistics
        public int TotalMeasurementsThisYear { get; set; }
        public double? LowestWeight { get; set; }
        public double? HighestWeight { get; set; }
        public double? AverageWeight { get; set; }
    }
}
