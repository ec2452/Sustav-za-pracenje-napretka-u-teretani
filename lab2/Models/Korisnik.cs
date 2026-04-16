using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sustavzapracenjenapretkauteretani.Models;

namespace Teretana.Models
{
    /// <summary>
    /// Predstavlja registriranog člana teretane
    /// </summary>
    public class Korisnik
    {
        // ============ PRIMARNI KLJUČ ============
        /// <summary>
        /// Jedinstveni identifikator korisnika u bazi
        /// [Key] označava da je ovo primarni ključ
        /// </summary>
        [Key]
        public int Id { get; set; }

        // ============ OSNOVNI PODACI ============
        /// <summary>
        /// Ime korisnika
        /// [Required] znači da polje ne smije biti prazno
        /// [StringLength] ograničava maksimalnu duljinu
        /// </summary>
        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(50, ErrorMessage = "Ime ne smije biti duže od 50 znakova")]
        public required string Ime { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(50)]
        public required string Prezime { get; set; }

        /// <summary>
        /// Email adresa - koristi se za prijavu
        /// [EmailAddress] automatski validira format emaila
        /// </summary>
        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Neispravan format emaila")]
        public required string Email { get; set; }

        // ============ DATETIME SVOJSTVO ============
        /// <summary>
        /// Datum rođenja korisnika
        /// DateTime tip služi za čuvanje datuma i vremena
        /// [DataType] određuje kako će se prikazati u View-u
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Datum rođenja")]
        public DateTime DatumRodjenja { get; set; }

        /// <summary>
        /// Datum kad se korisnik registrirao
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Datum registracije")]
        public DateTime DatumRegistracije { get; set; }

        // ============ FIZIČKI PODACI ============
        /// <summary>
        /// Visina u centimetrima
        /// [Range] ograničava dozvoljene vrijednosti
        /// </summary>
        [Range(100, 250, ErrorMessage = "Visina mora biti između 100 i 250 cm")]
        [Display(Name = "Visina (cm)")]
        public double Visina { get; set; }

        /// <summary>
        /// Trenutna težina u kilogramima
        /// </summary>
        [Range(30, 300, ErrorMessage = "Težina mora biti između 30 i 300 kg")]
        [Display(Name = "Težina (kg)")]
        public double Tezina { get; set; }

        // ============ NAVIGACIJSKA SVOJSTVA (VEZE) ============

        /// <summary>
        /// VEZA 1:N - Jedan korisnik ima VIŠE treninga
        /// ICollection predstavlja kolekciju (listu) povezanih entiteta
        /// virtual omogućuje "lazy loading" - podaci se učitavaju kad zatrebaju
        /// </summary>
        public virtual ICollection<Trening> Treninzi { get; set; }

        /// <summary>
        /// VEZA 1:N - Jedan korisnik ima VIŠE ciljeva
        /// </summary>
        public virtual ICollection<Cilj> Ciljevi { get; set; }

        /// <summary>
        /// VEZA 1:N - Jedan korisnik ima VIŠE mjerenja
        /// </summary>
        public virtual ICollection<Mjerenje> Mjerenja { get; set; }

        // ============ STRANI KLJUČ ZA VEZU N:1 ============
        /// <summary>
        /// ID plana koji korisnik trenutno prati
        /// ? (nullable) znači da korisnik NE MORA imati plan
        /// </summary>
        [Display(Name = "Trenutni plan")]
        public int? PlanId { get; set; }

        /// <summary>
        /// VEZA N:1 - Više korisnika može pratiti ISTI plan
        /// </summary>
        public virtual Plan? TrenutniPlan { get; set; }

        // ============ KONSTRUKTOR ============
        /// <summary>
        /// Konstruktor inicijalizira kolekcije
        /// Bez ovoga bi Treninzi, Ciljevi i Mjerenja bili null
        /// </summary>
        public Korisnik()
        {
            Treninzi = new HashSet<Trening>();
            Ciljevi = new HashSet<Cilj>();
            Mjerenja = new HashSet<Mjerenje>();
            DatumRegistracije = DateTime.Now; // Automatski postavi datum
        }
    }
}
