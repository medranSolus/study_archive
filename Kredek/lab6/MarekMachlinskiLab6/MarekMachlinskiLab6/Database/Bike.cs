namespace MarekMachlinskiLab6.Database
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Bike")]
    public partial class Bike
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bike()
        {
            Order = new HashSet<Order>();
        }

        public int BikeId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int MaxSpeed { get; set; }

        public int WheelSize { get; set; }

        public decimal Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
