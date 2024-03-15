using Management_System.Models.Entities;

namespace Management_System.Infrustructure.Config
{
    public class ContactCustomerConfig : IEntityTypeConfiguration<ContactCustomer>
    {
        public void Configure(EntityTypeBuilder<ContactCustomer> builder)
        {
            builder.HasKey(k => new { k.CustomerId, k.ContactId });
        }
    }
}
