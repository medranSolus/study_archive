namespace MarekMachlinskiLab6Zad1.Database
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Obiekt frakcji w bazie danych
    /// </summary>
    public partial class Faction
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faction()
        {
            Units = new HashSet<Unit>();
        }

        /// <summary>
        /// Nazwa frakcji
        /// </summary>
        [Key]
        [StringLength(7)]
        public string Name { get; set; }

        /// <summary>
        /// Lista jednostek we frakcji (???)
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unit> Units { get; set; }
    }
}
