using Management_System.Infrustructure.Config;
using Management_System.Models.Entities;

namespace Management_System.Infrustructure.Contexts
{
    public class MainContext : IdentityDbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
            base.OnModelCreating(modelBuilder);

            #region Seed Data

            modelBuilder.Entity<Product>().HasData(
            new Product()
            {
                Id = Guid.Parse("c5bf13277e2e436cae9308dc21125efe"),
                Name = "name",
                ImageAddress = "IPhone.webp",
                Description = "گوشی موبایل اپل مدل iPhone 11 تک سیم‌ کارت ظرفیت 128 گیگابایت و رم 4 گیگابایت به همراه شارژر 20 وات اپل - ویتنام نات اکتیو",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            modelBuilder.Entity<Customer>().HasData(
            new Customer()
            {
                Id = Guid.Parse("57ba4c5e98c14ddde31608dc2111c64f"),
                Name = "pouria",
                Description = "good person",
                Address = "isfahan",
                City = "isfahan",
                Province = "isfahan",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });


            modelBuilder.Entity<Order>().HasData(
            new Order()
            {
                Id = Guid.Parse("a1669bece7ba465ca4e3af2efafb9968"),
                CustomerId = Guid.Parse("57ba4c5e98c14ddde31608dc2111c64f"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem()
            {
                Id = Guid.Parse("e01aeca548ed40379acd7c398b6c1820"),
                ProductId = Guid.Parse("c5bf13277e2e436cae9308dc21125efe"),
                OrderId = Guid.Parse("a1669bece7ba465ca4e3af2efafb9968"),
                Description = "good ast",
                Quantity = 2,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            modelBuilder.Entity<Contact>().HasData(
            new Contact()
            {
                Id = Guid.Parse("eb3718630915481a88739ebeddf8039a"),
                FirstName = "ali",
                LastName = "norozi",
                Email = "test@gmail.com",
                Mobile = " 09132884969",
                Phone = "03137820975",
                Role = "Admin",
                Gender = true,
                LocalNumber = "123",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            modelBuilder.Entity<ContactCustomer>().HasData(
            new ContactCustomer()
            {
                ContactId = Guid.Parse("eb3718630915481a88739ebeddf8039a"),
                CustomerId = Guid.Parse("57ba4c5e98c14ddde31608dc2111c64f"),

            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "5e158bd69a2847b5a6e42073f21c9485",
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole()
                {
                    Id = "059f213c211f4865bdf2bd4986a68df3",
                    Name = "User",
                    NormalizedName = "USER"
                });
            #endregion
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactCustomer> ContactCustomers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeletedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<IdentityUser>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.EmailConfirmed = true;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }



    }
}