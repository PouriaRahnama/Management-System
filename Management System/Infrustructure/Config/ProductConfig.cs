using Management_System.Models.Entities;

namespace Management_System.Infrustructure.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

        }

    }
}