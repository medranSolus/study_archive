using System.ComponentModel.DataAnnotations;

namespace MarekMachlinskiLab4Zad1.Tables
{
    /// <summary>
    /// Stopnie określające poziom członka społeczności krasnoludów
    /// </summary>
    public class Rank : Entity
    {
        /// <summary>
        /// Nazwa stopnia
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
