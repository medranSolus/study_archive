using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarekMachlinskiLab4Zad1.Tables
{
    /// <summary>
    /// Klasa przedstawiająca przymierze klanów
    /// </summary>
    public class Alliance : Entity
    {
        /// <summary>
        /// Pierwszy klan w przymierzu
        /// </summary>
        [ForeignKey("Clan1Name")]
        public virtual Clan Clan1 { get; set; }

        /// <summary>
        /// Identyfikator obiektu Clan1
        /// </summary>
        [Required]
        public string Clan1Name { get; set; }

        /// <summary>
        /// Drugi klan w przymierzu
        /// </summary>
        [ForeignKey("Clan2Name")]
        public virtual Clan Clan2 { get; set; }

        /// <summary>
        /// Identyfikator obiektu Clan2
        /// </summary>
        [Required]
        public string Clan2Name { get; set; }
    }
}
