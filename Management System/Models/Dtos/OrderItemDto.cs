using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace Management_System.Models.Dtos
{
    public class OrderItemDto : BaseDto
    {
        [Required]
        [Display(Name = "شناسه محصول")]
        public virtual Guid ProductId { get; set; }
        [Required]
        [Display(Name = "تعداد محصول")]
        public virtual int Quantity { get; set; }

        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }
        public virtual Guid OrderId { get; set; }

        [Display(Name = "لایسنس ها")]
        public virtual OrderDto? OrderDto { get; set; }
        public virtual ProductDto? Product { get; set; }
    }

    public class AddItemDto
    {
        [Display(Name = "شناسه محصول")]
        public virtual Guid ProductId { get; set; }

        [Display(Name = "شناسه سفارش")]
        public virtual Guid OrderId { get; set; }

        [Required]
        [Display(Name = "تعداد محصول")]
        public virtual int Quantity { get; set; }

        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }



    }
}