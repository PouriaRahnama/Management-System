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
        public virtual OrderDto OrderDto { get; set; }
        public virtual ProductDto Product { get; set; }
    }

    public class AddItemDto
    {
        [Required]
        [Display(Name = "شناسه محصول")]
        public virtual Guid ProductId { get; set; }

        [Required]
        [Display(Name = "تعداد محصول")]
        public virtual int Quantity { get; set; }

        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }

        [Required]
        public virtual Guid OrderId { get; set; }
        public virtual ProductDto Product { get; set; }
    }

    public class EditItemDto
    {
        [Required]
        [Display(Name = "شناسه آیتم")]
        public virtual Guid Id { get; set; }

        [Required]
        [Display(Name = "شناسه محصول")]
        public virtual ProductDto Product { get; set; }

        [Required]
        [Display(Name = "تعداد محصول")]
        public virtual int Quantity { get; set; }

        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }

        [Required]
        [Display(Name = "تاریخ بروزرسانی")]
        public virtual DateTime UpdatedAt { get; set; }

    }


}