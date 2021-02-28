using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarekMachlinskiLab4.Models
{
    /// <summary>
    /// Klasa przedstawiająca recenzję gry
    /// </summary>
    public class Review : Entity
    {
        /// <summary>
        /// Tekst recenzji (pole jest wymagane)
        /// </summary>
        [Required]
        public string Text { get; set; }
        /// <summary>
        /// Czas wystawienia recenzji (pole wymagane)
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        /// <summary>
        /// Zrecenzowana gra
        /// </summary>
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        /// <summary>
        /// Identyfikator zrecenzowanej gry (pole wymagane)
        /// </summary>
        [Required]
        public int GameId { get; set; }
        /// <summary>
        /// Pole pokazujące ilość punktów w grze
        /// </summary>
        [Required]
        public int Score { get; set; }
    }
}
