namespace MarekMachlinskiLab5Zad1.Database
{
    using System.Data.Entity;

    /// <summary>
    /// Po��czenie z baz� danych
    /// </summary>
    public partial class MagicPortalContext : DbContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public MagicPortalContext()
            : base("name=MagicPortalContext")
        {
        }

        /// <summary>
        /// Tabela graczy
        /// </summary>
        public virtual DbSet<Player> Players { get; set; }

        /// <summary>
        /// Tworzenie modelu z bazy danych
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .Property(e => e.Title)
                .IsFixedLength();
        }
    }
}
