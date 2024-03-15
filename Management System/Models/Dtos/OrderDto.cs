namespace Management_System.Models.Dtos
{
    public class OrderDto : BaseDto
    {
        public OrderDto()
        {
            Items = new List<OrderItemDto>();
        }

        [Required]
        public virtual Guid CustomerId { get; set; }


        public virtual List<OrderItemDto> Items { get; set; }

        //Navigation Properties
        public virtual CustomerDto Customer { get; set; }
    }

    public class OrderDetailDto : BaseDto
    {
        public virtual Guid ItemId { get; set; }

        [Display(Name = "شماره مشتری")]
        public virtual Guid CustomerId { get; set; }

        [Display(Name = "نام کالا")]
        public virtual string ProductName { get; set; }

        [Display(Name = "توضیحات آیتم")]
        public virtual string? ItemDescription { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public virtual DateTime CreateAt { get; set; }

        [Display(Name = "تاریخ انقضاء")]
        public virtual DateTime ExpireAt { get; set; }
    }

    public class AddOrderDto
    {
        [Required]
        [Display(Name = "شناسه مشتری")]
        public virtual Guid CustomerId { get; set; }
    }

    public class SearchFilterOrderDto
    {
        [Display(Name = "شناسه سفارش")]
        public virtual List<Guid>? Id { get; set; }

        [Display(Name = "شناسه مشتری")]
        public virtual List<Guid>? CustomerId { get; set; }

        [Display(Name = "شناسه محصول")]
        public virtual List<Guid>? ProductId { get; set; }
    }
}