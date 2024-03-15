using Management_System.Models.Entities;

namespace Management_System.Infrustructure.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

        }

    }
}