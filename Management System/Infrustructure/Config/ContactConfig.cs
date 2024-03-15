using Management_System.Models.Entities;

namespace Management_System.Infrustructure.Config
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
        }

    }
}