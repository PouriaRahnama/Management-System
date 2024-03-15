using Management_System.Models.Entities;

namespace Management_System.Infrustructure.Config
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
        }

    }
}