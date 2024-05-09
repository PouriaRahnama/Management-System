namespace Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        #region Constructor
        private readonly ILogger<ContactController> logger;
        private IContactService contactService;
        private ICustomerService customerService;

        public ContactController(ILogger<ContactController> Logger, IContactService ContactService, ICustomerService CustomerService)
        {
            logger = Logger;
            contactService = ContactService;
            customerService = CustomerService;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await contactService.GetAllAsync();
            List<ContactDto> contacts = new();
            if (result != null)
                contacts = result.ToList();

            return View(contacts);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> AddContact(Guid? CustomerId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContact(Guid Id, AddContactDto addContactDto, string GenderName)
        {

            if (!ModelState.IsValid)
                return View();
            try
            {
                addContactDto.Gender = GenderName == "1" ? true : false;
                if (Id != Guid.Empty)
                    addContactDto.CustomerId = Id;
                if (addContactDto.CustomerId == null)
                {
                    await contactService.AddContactAsync(addContactDto);
                }
                else
                {
                    await customerService.AddCustomerContactAsync(addContactDto);
                    return RedirectToAction("Detail", "Customer", new { Id = addContactDto.CustomerId });
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Detail & Edit
        [HttpGet]
        public async Task<IActionResult> Detail(Guid Id)
        {
            var result = await contactService.GetByIdAsync(Id);
            if (result == null)
                return NotFound();
            ContactDto contact = result;
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, EditContactDto editContactDto, string GenderName)
        {
            try
            {
                editContactDto.Gender = GenderName == "1" ? true : false;
                await contactService.EditContactAsync(Id, editContactDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                await contactService.DeleteContactAsync(Id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
