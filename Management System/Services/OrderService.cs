namespace Management_System.Services
{
    public interface IOrderService
    {
        #region Get
        Task<List<OrderDto>> GetAllOrdersAsync(SearchFilterOrderDto? searchFilterOrderDto = null);
        Task<OrderDto> GetOrderByCustomerIdAsync(Guid CustomerId);
        Task<OrderDto> GetOrderByIdAsync(Guid Id);
        Task<List<OrderDetailDto>> GetOrderDetailByIdAsync(Guid CustomerId);
        Task<OrderItemDto> GetOrderItemByIdAsync(Guid id);
        #endregion

        #region Add , Update , Delete

        #region  Add Order & OrderItem
        Task<OrderDto> AddorderAsync(AddOrderDto addOrderDto);
        Task<IEnumerable<OrderItemDto>> AddItemDtoAsync(OrderDto orderDto, AddItemDto addItemDto, Guid productId);
        #endregion

        Task<OrderDto> DeleteOrderItemAsync(Guid orderId, Guid OrderItemId);
        Task DeleteOrderById(Guid id);
        Task DiminisItem(Guid OrderItemId);
        #endregion
    }

    [EasyDi(ServiceLifetime.Scoped)]
    [Display(Name = "سفارشات", Description = "این قسمت مربوط به ثبت سفارشات می باشد")]
    public class OrderService : IOrderService
    {
        #region Constructors
        private readonly MainContext context;
        private readonly IProductService productService;
        private readonly IPuterContext puterContext;
        private readonly IMapper mapper;
        public OrderService(MainContext Context, IProductService ProductService, IPuterContext PuterContext, IMapper Mapper)
        {
            context = Context;
            productService = ProductService;
            puterContext = PuterContext;
            mapper = Mapper;
        }
        #endregion

        #region Get
        public async Task<List<OrderDto>> GetAllOrdersAsync(SearchFilterOrderDto? searchFilterOrderDto = null)
        {
            var query = context.Orders.Include(c => c.Customer).Where(p => !p.IsDeleted).AsQueryable();

            if (searchFilterOrderDto != null)
            {
                if (searchFilterOrderDto.Id != null) query = query.Where(o => searchFilterOrderDto.Id.Contains(o.Id));
                if (searchFilterOrderDto.CustomerId != null) query = query.Where(o => searchFilterOrderDto.CustomerId.Contains(o.CustomerId));
                if (searchFilterOrderDto.ProductId != null) query = query.Where(o => o.Items.Any(p => searchFilterOrderDto.ProductId.Contains(p.Product.Id)));
            }

            var orders = await query.ToListAsync();
            foreach (var order in orders)
                order.Items = order.Items.Where(c => c.IsDeleted == false).ToList();

            if (orders == null || orders.Count == 0) return new List<OrderDto>();
            return mapper.Map<List<OrderDto>>(orders);
        }
        public async Task<OrderDto> GetOrderByCustomerIdAsync(Guid CustomerId)
        {
            var order = await context.Orders.AsNoTracking().FirstOrDefaultAsync(p => !p.IsDeleted && p.CustomerId == CustomerId);
            if (order == null) return new OrderDto();

            return mapper.Map<OrderDto>(order);
        }
        public async Task<OrderDto> GetOrderByIdAsync(Guid Id)
        {
            var order = await context.Orders.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == Id);
            if (order == null) return new OrderDto();

            order.Items = order.Items.Where(d => !d.IsDeleted).ToList();
            return mapper.Map<OrderDto>(order);
        }
        public async Task<List<OrderDetailDto>> GetOrderDetailByIdAsync(Guid CustomerId)
        {
            var query = from order in context.Orders
                        where !order.IsDeleted && order.CustomerId == CustomerId
                        join item in context.OrderItems on order.Id equals item.OrderId into OrderListItem
                        from item in OrderListItem.DefaultIfEmpty()
                        where !item.IsDeleted

                        select new OrderDetailDto()
                        {
                            Id = order.Id,
                            IsDeleted = order.IsDeleted,
                            CustomerId = order.CustomerId,
                            ItemDescription = item.Description,
                            CreateAt = order.CreatedAt,
                            ExpireAt = DateTime.Now,
                            ProductName = item.Product.Name,
                            CreatedAt = order.CreatedAt,
                            ItemId = item.Id
                        };

            var OrderItems = await query.ToListAsync();
            var result = OrderItems.DistinctBy(x => x.ItemId).ToList();
            return result;

        }
        public async Task<OrderItemDto> GetOrderItemByIdAsync(Guid id)
        {
            var orderItem = await context.OrderItems.AsNoTracking().FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            if (orderItem == null) return new OrderItemDto();

            return mapper.Map<OrderItemDto>(orderItem);
        }
        #endregion

        #region Add , Update , Delete

        #region Add
        public async Task<IEnumerable<OrderItemDto>> AddItemDtoAsync(OrderDto orderDto, AddItemDto addItemDto, Guid productId)
        {
            if (orderDto.Items.Exists(p => p.Product.Id == productId && p.OrderId == addItemDto.OrderId && p.IsDeleted == false))
                await UpdateItem(addItemDto, productId);
            else
                await AddItemDtoAsync(addItemDto);

            return orderDto.Items;
        }
        private async Task UpdateItem(AddItemDto addItemDto, Guid productId)
        {
            var item = await context.OrderItems.FirstOrDefaultAsync(i => i.OrderId == addItemDto.OrderId && i.ProductId == productId && i.IsDeleted == false);
            if (item == null) return;
            item.Quantity += 1;

            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }
        private async Task<OrderItemDto> AddItemDtoAsync(AddItemDto addItemDto)
        {
            var orderItem = mapper.Map<OrderItem>(addItemDto);
            await context.OrderItems.AddAsync(orderItem);
            await context.SaveChangesAsync();

            return mapper.Map<OrderItemDto>(orderItem);
        }
        public async Task<OrderDto> AddorderAsync(AddOrderDto addOrderDto)
        {
            var order = mapper.Map<Order>(addOrderDto);
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return mapper.Map<OrderDto>(order);
        }
        #endregion

        public async Task<OrderDto> DeleteOrderItemAsync(Guid orderId, Guid OrderItemId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => !o.IsDeleted && o.Id == orderId);
            if (order == null) return new OrderDto();

            var orderItem = order.Items.FirstOrDefault(p => p.Id == OrderItemId);
            if (orderItem == null) return new OrderDto();

            order.Items.FirstOrDefault(p => p.Id == orderItem.Id)!.IsDeleted = true;
            await context.SaveChangesAsync();

            return mapper.Map<OrderDto>(order);
        }
        public async Task DeleteOrderById(Guid id)
        {
            Order? order = await context.Orders.FirstOrDefaultAsync(o => !o.IsDeleted && o.Id == id);

            if (order?.Items.Count != 0)
                foreach (var item in order!.Items)
                    item.IsDeleted = true;

            order.IsDeleted = true;
            await context.SaveChangesAsync();
        }
        public async Task DiminisItem(Guid OrderItemId)
        {
            var orderItem = await context.OrderItems.FirstOrDefaultAsync(o => !o.IsDeleted && o.Id == OrderItemId);
            if (orderItem == null) return;

            orderItem.Quantity -= 1;
            await context.SaveChangesAsync();
        }
        #endregion
    }
}