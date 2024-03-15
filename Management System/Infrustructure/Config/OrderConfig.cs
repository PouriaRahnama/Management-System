using Management_System.Models.Entities;

namespace Management_System.Infrustructure.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

        }

    }
}