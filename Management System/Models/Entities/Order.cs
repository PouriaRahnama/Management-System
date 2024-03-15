namespace Management_System.Models.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        [Required]
        [Column(TypeName = "uniqueidentifier")]
        public virtual Guid CustomerId { get; set; }


        public virtual List<OrderItem> Items { get; set; }

        //Navigation Properties
        public virtual Customer Customer { get; set; }
    }
}