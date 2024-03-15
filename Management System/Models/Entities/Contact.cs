namespace Management_System.Models.Entities
{
    public class Contact : BaseEntity
    {

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public virtual string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public virtual string LastName { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public virtual string? Role { get; set; }

        [Column(TypeName = "bit")]
        public virtual bool Gender { get; set; }

        [StringLength(12)]
        [Column(TypeName = "varchar(12)")]
        public virtual string? Phone { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public virtual string? LocalNumber { get; set; }

        [StringLength(12)]
        [Column(TypeName = "varchar(12)")]
        public virtual string? Mobile { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public virtual string? Email { get; set; }

        //Navigation Properties
        public virtual List<ContactCustomer> ContactCustomers { get; set; }

    }
}