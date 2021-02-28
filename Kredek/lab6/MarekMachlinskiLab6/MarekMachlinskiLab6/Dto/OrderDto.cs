namespace MarekMachlinskiLab6.Dto
{
    /// <summary>
    /// Obiekt informacji o zleceniodawcy
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// Id zlecenia
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Imię zleceniodawcy
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko zleceniodawcy
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email zleceniodawcy
        /// </summary>
        public string Email { get; set; }
    }
}