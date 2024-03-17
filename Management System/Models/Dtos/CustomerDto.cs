using System.ComponentModel.DataAnnotations;

namespace Management_System.Models.Dtos
{
    public class CustomerDto : BaseDto
    {
        [Display(Name = "نام مشتری")]
        public virtual string Name { get; set; }

        [Display(Name = "استان")]
        public virtual string Province { get; set; }

        [Display(Name = "شهرستان")]
        public virtual string City { get; set; }

        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }
        public virtual List<OrderDto> Orders { get; set; }
        public virtual List<ContactDto> Contacts { get; set; }
    }

    public class AddCustomerDto
    {
        [Required(ErrorMessage = "{0} الزامی است")]
        [StringLength(200)]
        [Display(Name = "نام مشتری")]
        public required virtual string Name { get; set; }

        [Required(ErrorMessage = "{0} الزامی است")]
        [StringLength(200)]
        [Display(Name = "استان")]
        public required virtual string Province { get; set; }

        [Required(ErrorMessage = "{0} الزامی است")]
        [StringLength(200)]
        [Display(Name = "شهرستان")]
        public required virtual string City { get; set; }

        [StringLength(2000)]
        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [StringLength(2000)]
        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }
    }

    public class CustomerDetailDto : BaseDto
    {
        [Display(Name = "نام مشتری")]
        public virtual string Name { get; set; }

        [Display(Name = "استان")]
        public virtual string Province { get; set; }

        [Display(Name = "شهرستان")]
        public virtual string City { get; set; }

        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }
        public virtual List<ContactDto>? Contacts { get; set; }
        public virtual List<OrderDetailDto>? Orders { get; set; }
    }

    public class EditCustomerDto
    {
        [Required(ErrorMessage = "{0} الزامی است")]
        [Display(Name = "شناسه")]
        public virtual Guid Id { get; set; }

        [Required(ErrorMessage = "{0} الزامی است")]
        [StringLength(200)]
        [Display(Name = "نام مشتری")]
        public required virtual string Name { get; set; }

        [Required(ErrorMessage = "{0} الزامی است")]
        [StringLength(200)]
        [Display(Name = "استان")]
        public required virtual string Province { get; set; }

        [Required(ErrorMessage = "{0} الزامی است")]
        [StringLength(200)]
        [Display(Name = "شهرستان")]
        public required virtual string City { get; set; }

        [StringLength(2000)]
        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [StringLength(2000)]
        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }
    }

    public class SearchFilterCustomerDto
    {
        [Display(Name = "شناسه")]
        public virtual List<Guid>? Id { get; set; }

        [Display(Name = "نام مشتری")]
        public virtual string? Name { get; set; }

        [Display(Name = "استان")]
        public virtual string? Province { get; set; }

        [Display(Name = "شهرستان")]
        public virtual string? City { get; set; }
    }

}