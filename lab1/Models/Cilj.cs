using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teretana.Models
{
    /// <summary>
    /// Tipovi ciljeva
    /// </summary>
    public enum TipCilja
    {
        Mrsavljenje = 1,
        DobivanjeMase = 2,
        PovecanjeSnage = 3,
        Izdrzljivost = 4,
        Fleksibilnost = 5
    }

    /// <summary>
    /// Cilj koji korisnik želi postići
    /// </summary>
    public class Cilj
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = "Odaberi ispravan tip cilja")]
        [Display(Name = "Tip cilja")]
        public TipCilja Tip { get; set; }

        /// <summary>
        /// Ciljana vrijednost (npr. 80 kg)
        /// </summary>
        [Range(0.1, 10000)]
        [Display(Name = "Ciljna vrijednost")]
        public double CiljanaVrijednost { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Rok za ostvarenje")]
        public DateTime Rok { get; set; }

        /// <summary>
        /// Je li cilj postignut
        /// </summary>
        public bool Postignut { get; set; }

        // ============ STRANI KLJUČ ============
        [Required]
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }

        public virtual Korisnik Korisnik { get; set; } = null!;
    }
}
