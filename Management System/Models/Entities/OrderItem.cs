namespace Management_System.Models.Entities
{
    public class OrderItem : BaseEntity
    {

        [Required]
        public virtual Guid ProductId { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public virtual int Quantity { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public virtual string? Description { get; set; }

        public virtual Guid OrderId { get; set; }

        //Navigation Property
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}