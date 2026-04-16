using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teretana.Models
{
    /// <summary>
    /// Periodično mjerenje tjelesnih mjera
    /// </summary>
    public class Mjerenje
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Datum mjerenja")]
        public DateTime DatumMjerenja { get; set; }

        [Range(30, 300)]
        [Display(Name = "Težina (kg)")]
        public double Tezina { get; set; }

        /// <summary>
        /// Postotak tjelesne masti
        /// </summary>
        [Range(3, 60)]
        [Display(Name = "Postotak masti (%)")]
        public double? PostotakMasti { get; set; }

        /// <summary>
        /// Opseg struka u cm
        /// </summary>
        [Range(40, 200)]
        [Display(Name = "Opseg struka (cm)")]
        public double? OpsegStruka { get; set; }

        // ============ STRANI KLJUČ ============
        [Required]
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }

        public virtual Korisnik? Korisnik { get; set; }
    }
}
