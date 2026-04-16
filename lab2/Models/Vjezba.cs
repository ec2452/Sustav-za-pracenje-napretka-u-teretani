using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sustavzapracenjenapretkauteretani.Models;

namespace Teretana.Models
{
    /// <summary>
    /// Dodatni enum za kategorije mišićnih skupina
    /// </summary>
    public enum MisicnaSkupina
    {
        Prsa = 1,
        Leda = 2,
        Ramena = 3,
        Biceps = 4,
        Triceps = 5,
        Noge = 6,
        Trbuh = 7,
        Kardio = 8,
        CijeloTijelo = 9
    }

    /// <summary>
    /// Predstavlja jednu vježbu u katalogu vježbi
    /// Ovo je "master" tablica - sadrži definicije vježbi
    /// </summary>
    public class Vjezba
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Naziv vježbe (npr. "Bench Press", "Čučnjevi")
        /// </summary>
        [Required(ErrorMessage = "Naziv vježbe je obavezan")]
        [StringLength(100)]
        [Display(Name = "Naziv vježbe")]
        public string Naziv { get; set; } = string.Empty;

        /// <summary>
        /// Detaljan opis kako se vježba izvodi
        /// </summary>
        [StringLength(1000)]
        public string? Opis { get; set; }

        /// <summary>
        /// Primarna mišićna skupina koju vježba aktivira
        /// </summary>
        [Range(1, 9, ErrorMessage = "Odaberi ispravnu mišićnu skupinu")]
          public MisicnaSkupina MisicnaSkupina { get; set; }

        /// <summary>
        /// URL do slike ili videa s demonstracijom
        /// </summary>
        [Url]
        [Display(Name = "Video demonstracija")]
        public string? VideoUrl { get; set; }

        /// <summary>
        /// Razina težine (1-5)
        /// </summary>
        [Range(1, 5)]
        [Display(Name = "Razina težine")]
        public int RazinaTezine { get; set; }

        /// <summary>
        /// Je li potrebna oprema
        /// </summary>
        [Display(Name = "Potrebna oprema")]
        public bool PotrebnaOprema { get; set; }

        // ============ NAVIGACIJSKA SVOJSTVA ============

        /// <summary>
        /// Svi treninzi u kojima se ova vježba koristi
        /// </summary>
        public virtual ICollection<TreningVjezba> TreningVjezbe { get; set; }

        /// <summary>
        /// VEZA N:N s Plan kroz navigacijsko svojstvo
        /// Jedna vježba može biti u više planova
        /// </summary>
        public virtual ICollection<Plan> Planovi { get; set; }

        public Vjezba()
        {
            TreningVjezbe = new HashSet<TreningVjezba>();
            Planovi = new HashSet<Plan>();
        }
    }
}
