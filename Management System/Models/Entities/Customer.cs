namespace Management_System.Models.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public virtual string Province { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public virtual string City { get; set; }

        [StringLength(2000)]
        [Column(TypeName = "nvarchar(2000)")]
        public virtual string? Address { get; set; }

        [StringLength(2000)]
        [Column(TypeName = "nvarchar(2000)")]
        public virtual string? Description { get; set; }

        //Navigation Properties
        public virtual List<Order> Orders { get; set; }
        public virtual List<ContactCustomer> ContactCustomers { get; set; }

    }
}