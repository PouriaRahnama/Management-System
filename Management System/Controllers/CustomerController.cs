namespace Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        #region Constructor

        private readonly ILogger<CustomerController> logger;
        private ICustomerService customerService;
        private IOrderService orderService;
        private IContactService contactService;


        public CustomerController(ILogger<CustomerController> Logger, ICustomerService CustomerService, IOrderService OrderService, IContactService ContactService)
        {
            logger = Logger;
            customerService = CustomerService;
            orderService = OrderService;
            contactService = ContactService;
        }

        #endregion

        #region Get
        public async Task<IActionResult> Index()
        {
            var result = await customerService.GetAllAsync();
            List<CustomerDto> Customers = new();
            if (result != null)
                Customers = result.ToList();

            return View(Customers.ToList());
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            var result = await customerService.GetByIdAsync(Id);
            CustomerDetailDto Customer = result;

            var resultOrder = await orderService.GetOrderDetailByIdAsync(Id);
            Customer.Orders = resultOrder;

            var items1 = await contactService.GetAllAsync();
            var items = await contactService.GetAllContactsByCustomerIdAsync(result.Id);

            List<ShortDetailContactDto>? contacts = items1.Select(p => new ShortDetailContactDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,

            }).ToList();

            foreach (var item in items)
            {
                var con = contacts.FirstOrDefault(c => c.Id == item.Id);
                contacts.Remove(con);
            }

            contacts.Add(new ShortDetailContactDto()
            {
                Id = Guid.Empty,
                FirstName = "مخاطب",
                LastName = "جدید"
            });


            ViewBag.Contacts = new SelectList(contacts, "Id", "Name");
            Customer.Contacts = (List<ContactDto>?)items;

            return View(Customer);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return View(customerDto);

            CustomerDto customer = new CustomerDto();
            try
            {
                var result = await customerService.AddCustomerAsync(customerDto);
                customer = result;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Detail", new { customer.Id });
        }

        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                await customerService.DeleteCustomerAsync(Id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteContactCustomer(Guid CustomerId, Guid ContactId)
        {
            await customerService.DeleteContactCustomerAsync(CustomerId, ContactId);
            return RedirectToAction("Detail", new { Id = CustomerId });
        }

        #endregion

        #region Edit

        [HttpPost]
        public async Task<IActionResult> EditCustomer(Guid Id, EditCustomerDto editCustomerDto)
        {
            try
            {
                await customerService.EditCustomerAsync(Id, editCustomerDto);
                logger.LogInformation($"Customer with Id {Id} is Updated At {DateTime.Now}");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
            return RedirectToAction("Detail", new { Id });
        }

        #endregion

        #region Add Contact

        public async Task<IActionResult> AddContact(string Contacts, Guid CustomerId)
        {
            Guid contact = Guid.Parse(Contacts);
            if (contact == Guid.Empty)
                return RedirectToAction("AddContact", "Contact", new { CustomerId });
            else
            {
                await customerService.AddCustomerContactAsync(contact, CustomerId);
                return RedirectToAction("Detail", new { Id = CustomerId });
            }
        }

        #endregion

    }
}
