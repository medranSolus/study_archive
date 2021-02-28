namespace MarekMachlinskiLab6Zad1.Database
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Obiekt jednostki w bazie danych
    /// </summary>
    public partial class Unit
    {
        /// <summary>
        /// Identyfikator
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa jednostki
        /// </summary>
        [Required]
        [StringLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// Frakcja jednostki
        /// </summary>
        [Required]
        [StringLength(7)]
        public string FactionName { get; set; }

        /// <summary>
        /// Opcjonalny link do grafiki jednostki
        /// </summary>
        [StringLength(511)]
        public string PhotoLink { get; set; }

        /// <summary>
        /// Obiekt frakcji, do której nale¿y jednostka
        /// </summary>
        public virtual Faction Factions { get; set; }
    }
}
