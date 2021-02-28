namespace CarsApp.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Obiekt auta
    /// </summary>
    [Table("Car")]
    public partial class Car
    {
        /// <summary>
        /// Identyfikator auta
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Model auta
        /// </summary>
        [Required]
        [StringLength(128)]
        public string Model { get; set; }

        /// <summary>
        /// Producent
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Cena auta
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Link do zdjêcia
        /// </summary>
        [StringLength(128)]
        public string Photo { get; set; }
    }
}
