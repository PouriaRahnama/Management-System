namespace Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        #region Constructor

        private readonly IOrderService orderService;
        private ILogger<OrderController> logger;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public OrderController(IOrderService OrderService, IProductService ProductService, ICustomerService CustomerService, ILogger<OrderController> Logger)
        {
            orderService = OrderService;
            logger = Logger;
            productService = ProductService;
            customerService = CustomerService;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var result = await orderService.GetAllOrdersAsync();
            return View(result);
        }

        #endregion

        #region Create


        public async Task<IActionResult> Create()
        {
            #region Get All Customer
            var resultCustomers = await customerService.GetAllAsync();

            List<CustomerDto> customers = resultCustomers.ToList();
            ViewBag.Customers = new SelectList(customers, "Id", "Name");

            #endregion

            #region Get All Product
            var resultProd = await productService.GetAllProductsAsync();

            var products = resultProd.ToList();
            ViewBag.Products = new SelectList(products, "Id", "Name");


            #endregion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddOrderDto addOrderDto, AddItemDto itemDto, Guid Customers, Guid Products)
        {
            #region Get All Customer
            var resultCustomers = await customerService.GetAllAsync();

            List<CustomerDto> customers = resultCustomers.ToList();
            ViewBag.Customers = new SelectList(customers, "Id", "Name");

            #endregion

            #region Get All Product
            var resultProd = await productService.GetAllProductsAsync();

            var products = resultProd.ToList();
            ViewBag.Products = new SelectList(products, "Id", "Name");


            #endregion
            if (!ModelState.IsValid)
                return View(itemDto);

            if (Customers == Guid.Empty || Products == Guid.Empty)
                return View();
            try
            {
                addOrderDto.CustomerId = Customers;
                var result = await orderService.AddorderAsync(addOrderDto);
                OrderDto order = result;
                itemDto.ProductId = Products;
                itemDto.OrderId = order.Id;
                await orderService.AddItemDtoAsync(order, itemDto, Products);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Index");
        }


        #endregion

        #region Detail

        public async Task<IActionResult> Detail(Guid Id)
        {
            var resultProd = await productService.GetAllProductsAsync();
            var products = resultProd.ToList();
            ViewBag.Products = new SelectList(products, "Id", "Name");

            OrderDto order = new OrderDto();
            try
            {
                var result = await orderService.GetOrderByIdAsync(Id);
                if (result == null)
                    return NotFound();

                order = result;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return View(order);
        }

        #endregion

        #region Delete Order

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                await orderService.DeleteOrderById(Id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete Item

        [HttpPost]
        public async Task<IActionResult> DeleteItem(Guid OrderId, Guid ItemId)
        {
            try
            {
                await orderService.DeleteOrderItemAsync(OrderId, ItemId);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DiminishItem(Guid OrderId, Guid ItemId)
        {
            try
            {
                await orderService.DiminisItem(ItemId);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return RedirectToAction("Detail", new { Id = OrderId });
        }

        #endregion

        public async Task<IActionResult> Update(Guid OrderId, Guid Products)
        {
            AddItemDto item = new AddItemDto()
            {
                OrderId = OrderId,
                ProductId = Products,
                Quantity = 1
            };
            try
            {
                var result = await orderService.GetOrderByIdAsync(OrderId);
                var order = result;
                await orderService.AddItemDtoAsync(order, item, Products);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Detail", new { Id = OrderId });
        }

        public async Task<IActionResult> UpdateCountItem(Guid OrderId, Guid ProductId)
        {
            AddItemDto item = new AddItemDto()
            {
                OrderId = OrderId,
                ProductId = ProductId,
                Quantity = 1
            };
            try
            {
                var result = await orderService.GetOrderByIdAsync(OrderId);
                var order = result;
                await orderService.AddItemDtoAsync(order, item, ProductId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Detail", new { Id = OrderId });
        }
    }
}
