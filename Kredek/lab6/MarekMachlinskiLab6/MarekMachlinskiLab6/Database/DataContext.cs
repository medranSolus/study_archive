namespace MarekMachlinskiLab6.Database
{
    using System.Data.Entity;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Bike> Bike { get; set; }
        public virtual DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Bike>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Bike)
                .WillCascadeOnDelete(false);
        }
    }
}
