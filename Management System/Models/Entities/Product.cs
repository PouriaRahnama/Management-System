namespace Management_System.Models.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public virtual string Name { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public virtual string? Description { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public virtual string? ImageAddress { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

    }
}