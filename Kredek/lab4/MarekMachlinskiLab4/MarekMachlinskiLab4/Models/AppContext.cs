using System.Data.Entity;

namespace MarekMachlinskiLab4.Models
{
    /// <summary>
    /// Klasa odpowiedzialna za tabele w bazie danych
    /// </summary>
    public class AppContext : DbContext
    {
        /// <summary>
        /// Konstruktor inicjalizujący klasę bazową
        /// </summary>
        public AppContext() :base("AppContext") { }

        /// <summary>
        /// Tabela gier
        /// </summary>
        public virtual DbSet<Game> Games { get; set; }

        /// <summary>
        /// Tabela recenzji gier
        /// </summary>
        public virtual DbSet<Review> Reviews { get; set; }
    }
}
