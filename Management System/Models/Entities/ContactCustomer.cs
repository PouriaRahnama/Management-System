namespace Management_System.Models.Entities
{
    public class ContactCustomer
    {
        public virtual Guid ContactId { get; set; }
        public virtual Guid CustomerId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
