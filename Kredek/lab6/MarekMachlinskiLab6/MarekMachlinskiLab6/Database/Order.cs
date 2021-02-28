namespace MarekMachlinskiLab6.Database
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Order")]
    public partial class Order
    {
        public int OrderId { get; set; }

        public int BikeId { get; set; }

        [Required]
        [StringLength(64)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(64)]
        public string LastName { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        public virtual Bike Bike { get; set; }
    }
}
