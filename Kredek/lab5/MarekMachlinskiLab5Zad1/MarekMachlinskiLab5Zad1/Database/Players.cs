namespace MarekMachlinskiLab5Zad1.Database
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Obiekt gracza
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// Klucz g³ówny, imiê gracza
        /// </summary>
        [Key]
        [StringLength(31)]
        public string Name { get; set; }

        /// <summary>
        /// Data do³¹czenia
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime EnlistDate { get; set; }

        /// <summary>
        /// Tytu³ gracza
        /// </summary>
        [StringLength(31)]
        public string Title { get; set; }
    }
}
