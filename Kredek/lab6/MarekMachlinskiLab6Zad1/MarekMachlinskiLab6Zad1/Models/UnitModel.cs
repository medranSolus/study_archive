namespace MarekMachlinskiLab6Zad1.Models
{
    /// <summary>
    /// Model do przesyłania klasy Unit
    /// </summary>
    public class UnitModel
    {
        /// <summary>
        /// Identyfikator
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nazwa jednostki
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Frakcja jednostki
        /// </summary>
        public string FactionName { get; set; }

        /// <summary>
        /// Opcjonalny link do grafiki jednostki
        /// </summary>
        public string PhotoLink { get; set; }
    }
}