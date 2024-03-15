using Management_System.Infrustructure.Contexts;
using Management_System.Models.Dtos;
using Management_System.Models.Entities;

namespace Management_System.Services
{
    public interface ICustomerService
    {
        #region Get
        Task<IEnumerable<CustomerDto>> GetAllAsync(SearchFilterCustomerDto searchFilterCustomerDto = null);
        Task<CustomerDetailDto> GetByIdAsync(Guid id);
        Task<IEnumerable<OrderDto>> GetAllOrdersCustomer(Guid Id);
        #endregion

        #region Add , Delete , Update
        Task<CustomerDto> AddCustomerAsync(MainContext context, AddCustomerDto addCustomerDto);
        Task<CustomerDto> AddCustomerAsync(AddCustomerDto addCustomerDto);
        Task<ContactCustomerDto> AddCustomerContactAsync(AddContactDto addContactDto);
        Task AddCustomerContactAsync(Guid ContactId, Guid CustomerId);
        Task<CustomerDto> EditCustomerAsync(MainContext context, Guid Id, EditCustomerDto editCustomerDto);
        Task<CustomerDto> EditCustomerAsync(Guid Id, EditCustomerDto editCustomerDto);
        Task DeleteCustomerAsync(MainContext context, List<Guid> Ids);
        Task DeleteCustomerAsync(List<Guid> Ids);
        Task DeleteCustomerAsync(Guid Id);
        Task DeleteContactCustomerAsync(Guid CustomerId, Guid ContactId);

        #endregion
    }

    [EasyDi(ServiceLifetime.Scoped)]
    [Display(Name = "مشتریان", Description = "این قسمت مربوط به مشتریان می باشد")]
    public class CustomerService : ICustomerService
    {
        #region Constructor
        private readonly MainContext context;
        private readonly IMapper mapper;
        private readonly IPuterContext putterContext;
        private IContactService contactService;
        private readonly IOrderService orderService;

        public CustomerService(MainContext Context, IMapper Mapper, IPuterContext PutterContext, IContactService ContactService, IOrderService OrderService)
        {
            context = Context;
            mapper = Mapper;
            putterContext = PutterContext;
            contactService = ContactService;
            orderService = OrderService;
        }
        #endregion

        #region Add Customer
        public async Task<CustomerDto> AddCustomerAsync(MainContext context, AddCustomerDto addCustomerDto)
        {
            var Customer = mapper.Map<Customer>(addCustomerDto);
            await context.AddAsync(Customer);

            return mapper.Map<CustomerDto>(Customer);
        }
        public async Task<CustomerDto> AddCustomerAsync(AddCustomerDto addCustomerDto)
        {
            var result = await AddCustomerAsync(context, addCustomerDto);
            await context.SaveChangesAsync();
            return result;
        }
        #endregion

        #region  Add Customer Contacts
        public async Task AddCustomerContactAsync(Guid ContactId, Guid CustomerId)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(p => p.Id == ContactId);
            var customer = await context.Customers.FirstOrDefaultAsync(p => p.Id == CustomerId);

            await context.SaveChangesAsync();
        }
        #endregion

        #region Delete Customer
        public async Task DeleteCustomerAsync(List<Guid> Ids)
        {
            await DeleteCustomerAsync(context, Ids);
            await context.SaveChangesAsync();
        }
        public async Task DeleteCustomerAsync(Guid Id)
        {
            await DeleteCustomerAsync(context, new List<Guid> { Id });
            await context.SaveChangesAsync();
        }
        public async Task DeleteCustomerAsync(MainContext context, List<Guid> Ids)
        {
            var Customers = await context.Customers.Where(p => !p.IsDeleted && Ids.Contains(p.Id)).ToListAsync();

            foreach (var Customer in Customers)
            {
                Customer.IsDeleted = true;
                Customer.DeletedAt = DateTime.Now;
            }
        }
        public async Task<ContactCustomerDto> AddCustomerContactAsync(AddContactDto addContactDto)
        {
            var resultContact = await contactService.AddContactAsync(addContactDto);

            var contact = await context.Contacts.FirstOrDefaultAsync(p => p.Id == resultContact.Id);
            var Customer = await context.Customers.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == addContactDto.CustomerId);

            await context.ContactCustomers.AddAsync(new ContactCustomer()
            {
                ContactId = contact.Id,
                CustomerId = Customer.Id
            });
            await context.SaveChangesAsync();

            return mapper.Map<ContactCustomerDto>(contact);
        }
        public async Task DeleteContactCustomerAsync(Guid CustomerId, Guid ContactId)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(p => p.Id == CustomerId);
            var contact = await context.Contacts.FirstOrDefaultAsync(p => p.Id == ContactId);

            await context.ContactCustomers.AddAsync(new ContactCustomer()
            {
                ContactId = contact.Id,
                CustomerId = customer.Id
            });

            await context.SaveChangesAsync();
        }
        #endregion

        #region Edit Customer
        public async Task<CustomerDto> EditCustomerAsync(MainContext context, Guid Id, EditCustomerDto editCustomerDto)
        {
            var Customer = await context.Customers.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == Id);
            if (Customer == null) return new CustomerDto();

            putterContext.Put(Customer, editCustomerDto);
            return mapper.Map<CustomerDto>(Customer);
        }
        public async Task<CustomerDto> EditCustomerAsync(Guid Id, EditCustomerDto editCustomerDto)
        {
            var result = await EditCustomerAsync(context, Id, editCustomerDto);
            await context.SaveChangesAsync();
            return result;
        }
        #endregion

        #region Get All Customer
        public async Task<IEnumerable<CustomerDto>> GetAllAsync(SearchFilterCustomerDto? searchFilterCustomerDto = null)
        {
            var query = context.Customers.Where(p => !p.IsDeleted).AsNoTracking().ProjectTo<CustomerDto>(mapper.ConfigurationProvider).AsQueryable();
            if (searchFilterCustomerDto != null)
            {
                if (searchFilterCustomerDto.Id != null) query = query.Where(c => searchFilterCustomerDto.Id.Contains(c.Id));
                if (searchFilterCustomerDto.Name != null) query = query.Where(c => searchFilterCustomerDto.Name.Contains(c.Name));
                if (searchFilterCustomerDto.Province != null) query = query.Where(c => searchFilterCustomerDto.Province.Contains(c.Province));
                if (searchFilterCustomerDto.City != null) query = query.Where(c => searchFilterCustomerDto.City.Contains(c.City));
            }

            var Items = await query.ToListAsync();
            if (Items == null || Items.Count == 0) return new List<CustomerDto>();

            return mapper.Map<IEnumerable<CustomerDto>>(Items);
        }
        #endregion

        #region Get Customer By Id
        public async Task<CustomerDetailDto> GetByIdAsync(Guid id)
        {
            var Customer = await context.Customers.Include(c => c.Orders).ProjectTo<CustomerDetailDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            if (Customer == null) return new CustomerDetailDto();
            return mapper.Map<CustomerDetailDto>(Customer);
        }
        #endregion

        #region Get All Orders Customer
        public async Task<IEnumerable<OrderDto>> GetAllOrdersCustomer(Guid Id)
        {
            var orders = await context.Orders.Where(p => !p.IsDeleted && p.Customer.Id == Id).ToListAsync();
            if (orders == null) return new List<OrderDto>();
            return mapper.Map<List<OrderDto>>(orders);
        }
        #endregion
    }
}