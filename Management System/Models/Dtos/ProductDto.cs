namespace Management_System.Models.Dtos
{
    public class ProductDto : BaseDto
    {
        [Display(Name = "نام محصول")]
        public virtual string Name { get; set; }

        [Display(Name = "توضیحات محصول")]
        public virtual string? Description { get; set; }

        [Display(Name = "آدرس تصویر محصول")]
        public virtual string? ImageAddress { get; set; }

        public virtual List<OrderItemDto>? OrderItemDtos { get; set; }
    }

    public class AddProductDto
    {
        [Required(ErrorMessage = "{0} الزامی است")]
        [Display(Name = "نام محصول")]
        public required virtual string Name { get; set; }

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

        [Required(ErrorMessage = "{0} الزامی است")]
        [Display(Name = "نام محصول")]
        public required virtual string Name { get; set; }

        [Display(Name = "توضیحات محصول")]
        public virtual string? Description { get; set; }

        [Display(Name = "آدرس تصویر محصول")]
        public virtual IFormFile? ImageFile { get; set; }
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