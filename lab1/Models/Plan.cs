using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Teretana.Models
{
    /// <summary>
    /// Razina iskustva za plan
    /// </summary>
    public enum RazinaIskustva
    {
        Pocetnik = 1,
        Srednji = 2,
        Napredni = 3
    }

    /// <summary>
    /// Preddefinirani plan treninga
    /// </summary>
    public class Plan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Naziv plana")]
        public string Naziv { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Opis { get; set; }

        /// <summary>
        /// Trajanje plana u tjednima
        /// </summary>
        [Range(1, 52)]
        [Display(Name = "Trajanje (tjedni)")]
        public int TrajanjeTjedana { get; set; }

        [Range(1, 3, ErrorMessage = "Odaberi ispravnu razinu iskustva")]
        [Display(Name = "Razina iskustva")]
        public RazinaIskustva Razina { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum kreiranja")]
        public DateTime DatumKreiranja { get; set; }

        // ============ NAVIGACIJSKA SVOJSTVA ============

        /// <summary>
        /// VEZA N:N - Plan sadrži više vježbi
        /// </summary>
        public virtual ICollection<Vjezba> Vjezbe { get; set; }

        /// <summary>
        /// VEZA 1:N - Plan prati više korisnika
        /// </summary>
        public virtual ICollection<Korisnik> Korisnici { get; set; }

        public Plan()
        {
            Vjezbe = new HashSet<Vjezba>();
            Korisnici = new HashSet<Korisnik>();
            DatumKreiranja = DateTime.Now;
        }
    }
}
