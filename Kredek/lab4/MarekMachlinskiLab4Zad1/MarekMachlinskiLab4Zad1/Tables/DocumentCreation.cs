using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarekMachlinskiLab4Zad1.Tables
{
    /// <summary>
    /// Klasa przedstawiająca pobranie dokumentu przez użytkownika
    /// </summary>
    public class DocumentCreation : Entity
    {
        /// <summary>
        /// Użytkownik pobierający dokument
        /// </summary>
        [ForeignKey("UserName")]
        public virtual User User { get; set; }

        /// <summary>
        /// Identyfikator obiektu User
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Nazwa dokumentu
        /// </summary>
        [Required]
        public string CreatedDocument { get; set; }

        /// <summary>
        /// Data wystawienia dokumentu
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }
    }
}
