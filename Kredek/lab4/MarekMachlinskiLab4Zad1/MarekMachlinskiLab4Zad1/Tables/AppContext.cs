using System.Data.Entity;

namespace MarekMachlinskiLab4Zad1.Tables
{
    /// <summary>
    /// Klasa odpowiedzialna za tabele w bazie danych
    /// </summary>
    public class AppContext : DbContext
    {
        /// <summary>
        /// Konstruktor inicjalizujący klasę bazową
        /// </summary>
        public AppContext() : base("AppContext") { }

        /// <summary>
        /// Tabela użytkowników
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Tabela pobranych dokumentów przez użytkowników
        /// </summary>
        public virtual DbSet<DocumentCreation> DocumentsLog { get; set; }

        /// <summary>
        /// Tabela klanów
        /// </summary>
        public virtual DbSet<Clan> Clans { get; set; }

        /// <summary>
        /// Tabela rang użytkowników
        /// </summary>
        public virtual DbSet<Rank> Ranks { get; set; }

        /// <summary>
        /// Tabela wojen klanów
        /// </summary>
        public virtual DbSet<War> Wars { get; set; }

        /// <summary>
        /// Tabela przymierzy klanów
        /// </summary>
        public virtual DbSet<Alliance> Alliances { get; set; }
    }
}
