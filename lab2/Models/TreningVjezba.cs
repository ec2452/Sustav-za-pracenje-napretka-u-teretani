using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teretana.Models
{
    /// <summary>
    /// Povezuje Trening i Vjezbu s dodatnim detaljima
    /// Ovo je "asocijativna klasa" - nije čista N:N veza
    /// jer ima vlastita svojstva (setovi, ponavljanja, težina)
    /// </summary>
    public class TreningVjezba
    {
        [Key]
        public int Id { get; set; }

        // ============ STRANI KLJUČEVI ============

        /// <summary>
        /// ID treninga
        /// </summary>
        [Required]
        [ForeignKey("Trening")]
        public int TreningId { get; set; }

        /// <summary>
        /// ID vježbe
        /// </summary>
        [Required]
        [ForeignKey("Vjezba")]
        public int VjezbaId { get; set; }

        // ============ DODATNI PODACI O IZVEDBI ============

        /// <summary>
        /// Broj serija (setova)
        /// </summary>
        [Required]
        [Range(1, 20)]
        [Display(Name = "Broj serija")]
        public int BrojSerija { get; set; }

        /// <summary>
        /// Broj ponavljanja po seriji
        /// </summary>
        [Required]
        [Range(1, 100)]
        [Display(Name = "Ponavljanja")]
        public int BrojPonavljanja { get; set; }

        /// <summary>
        /// Težina u kilogramima (za vježbe s utezima)
        /// Nullable jer kardio vježbe nemaju težinu
        /// </summary>
        [Range(0, 500)]
        [Display(Name = "Težina (kg)")]
        public double? TezinaKg { get; set; }

        /// <summary>
        /// Odmor između serija u sekundama
        /// </summary>
        [Range(0, 600)]
        [Display(Name = "Odmor (sek)")]
        public int OdmorSekundi { get; set; }

        /// <summary>
        /// Redoslijed vježbe u treningu
        /// </summary>
        [Range(1, 100)]
        [Display(Name = "Redni broj")]
        public int Redoslijed { get; set; }

        // ============ NAVIGACIJSKA SVOJSTVA ============

        public virtual Trening? Trening { get; set; }
        public virtual Vjezba? Vjezba { get; set; }
    }
}
