using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MarekMachlinskiLab4.Models
{
    /// <summary>
    /// Klasa przedstawiająca grę
    /// </summary>
    public class Game : Entity
    {
        /// <summary>
        /// Nazwa gry (pole wymagane)
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Producent gry (pole wymagane)
        /// </summary>
        [Required]
        public string Producent { get; set; }
        /// <summary>
        /// Lista recenzji
        /// </summary>
        public virtual List<Review> Reviews { get; set; }
    }
}
