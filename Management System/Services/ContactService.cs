namespace Management_System.Services
{
    public interface IContactService
    {
        #region Get Data
        Task<IEnumerable<ContactDto>> GetAllAsync(SearchFilterContactDto? searchFilterContactDto = null);
        Task<ContactDto> GetByIdAsync(Guid Id);
        Task<IEnumerable<ContactCustomerDto>> GetByCustomerAsync(Guid CustomerId);
        Task<List<ContactDto>> GetAllContactsByCustomerIdAsync(Guid CustomerId);
        #endregion

        #region  Add,Update,Delete
        Task<ContactDto> AddContactAsync(MainContext context, AddContactDto addContactDto);
        Task<ContactDto> AddContactAsync(AddContactDto addContactDto);
        Task<ContactDto> EditContactAsync(MainContext context, Guid Id, EditContactDto editContactDto);
        Task<ContactDto> EditContactAsync(Guid Id, EditContactDto editContactDto);
        Task DeleteContactAsync(MainContext context, List<Guid> Ids);
        Task DeleteContactAsync(List<Guid> Ids);
        Task DeleteContactAsync(Guid Id);
        #endregion 
    }

    [EasyDi(ServiceLifetime.Scoped)]
    [Display(Name = "مخاطبین", Description = "این قسمت مربوط به مخاطبین مشتریان می باشد")]
    public class ContactService : IContactService
    {
        #region Constractor

        private readonly IMapper mapper;
        private readonly MainContext context;
        private readonly IPuterContext putter;
        public ContactService(IPuterContext putter, MainContext context, IMapper mapper)
        {
            this.putter = putter;
            this.context = context;
            this.mapper = mapper;
        }
        #endregion

        #region Add Contact
        public async Task<ContactDto> AddContactAsync(MainContext context, AddContactDto addContactDto)
        {
            var Contact = mapper.Map<Contact>(addContactDto);
            await context.AddAsync(Contact);

            return mapper.Map<ContactDto>(Contact);
        }
        public async Task<ContactDto> AddContactAsync(AddContactDto addContactDto)
        {
            var result = await AddContactAsync(context, addContactDto);
            await context.SaveChangesAsync();

            return result;
        }
        #endregion

        #region Delete Contact
        public async Task DeleteContactAsync(MainContext context, List<Guid> Ids)
        {
            var Contacts = await context.Contacts.Where(c => !c.IsDeleted && Ids.Contains(c.Id)).ToListAsync();
            foreach (var Contact in Contacts)
            {
                Contact.IsDeleted = true;
            }
        }
        public async Task DeleteContactAsync(List<Guid> Ids)
        {
            await DeleteContactAsync(context, Ids);
            await context.SaveChangesAsync();
        }
        public async Task DeleteContactAsync(Guid Id)
        {
            await DeleteContactAsync(context, new List<Guid> { Id });
            await context.SaveChangesAsync();
        }
        #endregion

        #region Edit Contact
        public async Task<ContactDto> EditContactAsync(MainContext context, Guid Id, EditContactDto editContactDto)
        {
            var Contact = await context.Contacts.FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == Id);
            if (Contact == null) return new ContactDto();
            putter.Put(Contact, editContactDto);

            return mapper.Map<ContactDto>(Contact);
        }
        public async Task<ContactDto> EditContactAsync(Guid Id, EditContactDto editContactDto)
        {
            var result = await EditContactAsync(context, Id, editContactDto);
            await context.SaveChangesAsync();

            return result;
        }
        #endregion

        #region Get
        public async Task<IEnumerable<ContactDto>> GetAllAsync(SearchFilterContactDto? searchFilterContactDto = null)
        {
            var query = context.Contacts.Where(p => !p.IsDeleted).AsNoTracking().AsQueryable();
            if (searchFilterContactDto != null)
            {
                if (searchFilterContactDto.CustomerId != null) query = query.Where(w => w.ContactCustomers.Any(w => w.CustomerId == searchFilterContactDto.CustomerId));
                if (string.IsNullOrEmpty(searchFilterContactDto.FirstName)) query = query.Where(w => searchFilterContactDto.FirstName!.Contains(w.FirstName));
                if (string.IsNullOrEmpty(searchFilterContactDto.LastName)) query = query.Where(w => searchFilterContactDto.LastName!.Contains(w.LastName));
                if (string.IsNullOrEmpty(searchFilterContactDto.Phone)) query = query.Where(w => searchFilterContactDto.Phone!.Contains(w.Phone!));
                if (string.IsNullOrEmpty(searchFilterContactDto.Mobile)) query = query.Where(w => searchFilterContactDto.Mobile!.Contains(w.Mobile!));
            }
            var Items = await query.ToListAsync();
            if (Items == null || Items.Count == 0) return new List<ContactDto>();
            return mapper.Map<IEnumerable<ContactDto>>(Items);
        }
        public async Task<IEnumerable<ContactCustomerDto>> GetByCustomerAsync(Guid CustomerId)
        {
            var query = context.Contacts.Where(w => w.ContactCustomers.Any(p => p.CustomerId == CustomerId)).AsNoTracking().ProjectTo<ContactCustomerDto>(mapper.ConfigurationProvider).AsQueryable();

            var items = await query.ToListAsync();
            if (items == null || items.Count == 0) return new List<ContactCustomerDto>();

            return mapper.Map<IEnumerable<ContactCustomerDto>>(items);
        }
        public async Task<ContactDto> GetByIdAsync(Guid Id)
        {
            var Contact = await context.Contacts.AsNoTracking().ProjectTo<ContactDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == Id);
            if (Contact == null) return new ContactDto();

            return mapper.Map<ContactDto>(Contact);
        }
        #endregion

        public async Task<List<ContactDto>> GetAllContactsByCustomerIdAsync(Guid CustomerId)
        {
            var result = await context.ContactCustomers.Where(a => a.CustomerId == CustomerId).Select(c => c.Contact).ToListAsync();
            return mapper.Map<List<ContactDto>>(result);
        }
    }
}