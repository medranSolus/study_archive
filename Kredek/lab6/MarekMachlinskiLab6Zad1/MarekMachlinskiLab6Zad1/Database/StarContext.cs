namespace MarekMachlinskiLab6Zad1.Database
{
    using System.Data.Entity;

    /// <summary>
    /// Obiekt reprezentuj¹cy bazê danych
    /// </summary>
    public partial class StarContext : DbContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public StarContext()
            : base("name=StarContext")
        {
        }

        /// <summary>
        /// Lista frakcji
        /// </summary>
        public virtual DbSet<Faction> Factions { get; set; }
        /// <summary>
        /// Lista jednostek
        /// </summary>
        public virtual DbSet<Unit> Units { get; set; }

        /// <summary>
        /// Tworzenie modelu: code first from database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faction>()
                .HasMany(e => e.Units)
                .WithRequired(e => e.Factions)
                .HasForeignKey(e => e.FactionName)
                .WillCascadeOnDelete(false);
        }
    }
}
