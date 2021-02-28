using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarekMachlinskiLab4.Models
{
    /// <summary>
    /// Klasa udostępniająca klasom pochodnym właściwość klucza głównego
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Klucz główny, identyfikator gry
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
