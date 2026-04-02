using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sustavzapracenjenapretkauteretani.Models;

namespace Teretana.Models
{
    /// <summary>
    /// Predstavlja pojedinačni trening korisnika
    /// </summary>
    
     public enum VrstaTreninga
    {
        Snaga = 1,
        Kardio = 2,
        HIIT = 3,
        Funkcionalni = 4,
        Mobility = 5
    }

    public class Trening
    {
        [Key]
        public int Id { get; set; }

        // ============ DATETIME SVOJSTVA ============
        /// <summary>
        /// Datum i vrijeme kad je trening započeo
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Početak treninga")]
        public DateTime DatumVrijeme { get; set; }

        /// <summary>
        /// Trajanje treninga u minutama
        /// </summary>
        [Range(1, 480, ErrorMessage = "Trajanje mora biti između 1 i 480 minuta")]
        [Display(Name = "Trajanje (min)")]
        public int TrajanjeMinuta { get; set; }

        // ============ ENUM SVOJSTVO ============
        /// <summary>
        /// Vrsta treninga - koristi naš enum
        /// </summary>
        [Range(1, 5, ErrorMessage = "Odaberi ispravnu vrstu treninga")]
        [Display(Name = "Vrsta treninga")]
        public VrstaTreninga Vrsta { get; set; }

        /// <summary>
        /// Korisnikove bilješke o treningu
        /// </summary>
        [StringLength(500)]
        public string? Biljeske { get; set; }

        /// <summary>
        /// Procijenjene potrošene kalorije
        /// </summary>
        [Range(0, 5000)]
        [Display(Name = "Potrošene kalorije")]
        public int? PotroseneKalorije { get; set; }

        /// <summary>
        /// Subjektivna ocjena treninga (1-10)
        /// </summary>
        [Range(1, 10)]
        [Display(Name = "Ocjena (1-10)")]
        public int? Ocjena { get; set; }

        // ============ STRANI KLJUČ - VEZA N:1 ============
        /// <summary>
        /// ID korisnika kojem pripada ovaj trening
        /// [ForeignKey] eksplicitno označava strani ključ
        /// </summary>
        [Required]
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }

        /// <summary>
        /// Navigacijsko svojstvo prema korisniku
        /// </summary>
        public virtual Korisnik? Korisnik { get; set; }

        // ============ VEZA 1:N - Trening sadrži više vježbi ============
        /// <summary>
        /// Lista vježbi u ovom treningu
        /// Ovo je "slaba" strana veze 1:N
        /// </summary>
        public virtual ICollection<TreningVjezba> TreningVjezbe { get; set; }

        public Trening()
        {
            TreningVjezbe = new HashSet<TreningVjezba>();
            DatumVrijeme = DateTime.Now;
        }
    }
}
