namespace Management_System.Models.Dtos
{
    public class ProductDto : BaseDto
    {
        [Required]
        [Display(Name = "نام محصول")]
        public virtual string Name { get; set; }

        [Display(Name = "توضیحات محصول")]
        public virtual string? Description { get; set; }

        [Display(Name = "آدرس تصویر محصول")]
        public virtual string? ImageAddress { get; set; }

        public virtual List<OrderItemDto> OrderItemDtos { get; set; }
    }
    public class AddProductDto
    {
        [Required(ErrorMessage = "نام محصول الزامی است")]
        [Display(Name = "نام محصول")]
        public virtual string Name { get; set; }

        [Display(Name = "توضیحات محصول")]
        public virtual string? Description { get; set; }

        [Display(Name = "آدرس تصویر محصول")]
        public virtual IFormFile? ImageFile { get; set; }
    }
    public class EditProductDto
    {
        [Required]
        [Display(Name = "شناسه محصول")]
        public virtual Guid Id { get; set; }

        [Required]
        [Display(Name = "نام محصول")]
        public virtual string Name { get; set; }

        [Display(Name = "توضیحات محصول")]
        public virtual string? Description { get; set; }

        [Display(Name = "آدرس تصویر محصول")]
        public virtual IFormFile? ImageFile { get; set; }

        [Required]
        [Display(Name = "تاریخ بروز رسانی")]
        public DateTime Updated { get; set; }
    }
    public class PathProductDto
    {
        [Required]
        [Display(Name = "شناسه محصول")]
        public virtual Guid Id { get; set; }

        [Display(Name = "نام محصول")]
        public virtual string? Name { get; set; }

        [Display(Name = "توضیحات محصول")]
        public virtual string? Description { get; set; }

        [Display(Name = "آدرس تصویر محصول")]
        public virtual string? ImageAddress { get; set; }

        [Required]
        [Display(Name = "تاریخ بروز رسانی")]
        public virtual DateTime Updated { get; set; }
    }
    public class SearchFilterProductDto
    {
        [Display(Name = "شناسه محصول")]
        public virtual List<Guid>? Id { get; set; }

        [Display(Name = "نام محصول")]
        public virtual string? Name { get; set; }

        [Display(Name = "توضیحات محصول")]
        public virtual string? Description { get; set; }
    }
}