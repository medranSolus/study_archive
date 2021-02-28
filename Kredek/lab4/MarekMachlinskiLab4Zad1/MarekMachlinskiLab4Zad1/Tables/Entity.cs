using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarekMachlinskiLab4Zad1.Tables
{
    /// <summary>
    /// Klasa udostępniająca właściwość identyfikatora w tabeli
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Identyfikato rekordu
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
