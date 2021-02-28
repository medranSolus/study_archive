using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarekMachlinskiLab4Zad1.Tables
{
    /// <summary>
    /// Klasa przedstawiająca wojnę klanów
    /// </summary>
    public class War : Entity
    {
        /// <summary>
        /// Pierwszy klan w wojnie
        /// </summary>
        [ForeignKey("Clan1Name")]
        public virtual Clan Clan1 { get; set; }

        /// <summary>
        /// Identyfikator obiektu Clan1
        /// </summary>
        [Required]
        public string Clan1Name { get; set; }

        /// <summary>
        /// Drugi klan w wojnie
        /// </summary>
        [ForeignKey("Clan2Name")]
        public virtual Clan Clan2 { get; set; }

        /// <summary>
        /// Identyfikator obiektu Clan1
        /// </summary>
        [Required]
        public string Clan2Name { get; set; }
    }
}
