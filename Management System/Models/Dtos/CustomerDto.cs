using System.ComponentModel.DataAnnotations;

namespace Management_System.Models.Dtos
{
    public class CustomerDto : BaseDto
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "نام مشتری")]
        public virtual string Name { get; set; }

        [StringLength(200)]
        [Display(Name = "استان")]
        public virtual string Province { get; set; }

        [StringLength(200)]
        [Display(Name = "شهرستان")]
        public virtual string City { get; set; }

        [StringLength(2000)]
        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [StringLength(2000)]
        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }
        public virtual List<OrderDto> Orders { get; set; }
        public virtual List<ContactDto> Contacts { get; set; }

    }
    public class AddCustomerDto
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "نام مشتری")]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "استان")]
        public virtual string Province { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "شهرستان")]
        public virtual string City { get; set; }

        [StringLength(2000)]
        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [StringLength(2000)]
        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }
    }

    public class CustomerDetailDto : BaseDto
    {
        [StringLength(200)]
        [Display(Name = "نام مشتری")]
        public virtual string Name { get; set; }

        [StringLength(200)]
        [Display(Name = "استان")]
        public virtual string Province { get; set; }

        [StringLength(200)]
        [Display(Name = "شهرستان")]
        public virtual string City { get; set; }

        [StringLength(2000)]
        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [StringLength(2000)]
        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }
        public virtual List<ContactDto>? Contacts { get; set; }
        public virtual List<OrderDetailDto>? Orders { get; set; }
    }
    public class EditCustomerDto
    {
        [Required]
        [Display(Name = "شناسه")]

        public virtual Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "نام مشتری")]
        public virtual string Name { get; set; }

        [StringLength(200)]
        [Display(Name = "استان")]
        public virtual string Province { get; set; }

        [StringLength(200)]
        [Display(Name = "شهرستان")]
        public virtual string City { get; set; }

        [StringLength(2000)]
        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [StringLength(2000)]
        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }

        [Required]
        [Display(Name = "تاریخ بروزرسانی")]

        public virtual DateTime UpdatedAt { get; set; }
    }
    public class PatchCustomerDto
    {
        [Required]
        [Display(Name = "شناسه")]
        public virtual Guid Id { get; set; }

        [StringLength(200)]
        [Display(Name = "نام مشتری")]
        public virtual string? Name { get; set; }

        [StringLength(200)]
        [Display(Name = "استان")]
        public virtual string? Province { get; set; }

        [StringLength(200)]
        [Display(Name = "شهرستان")]
        public virtual string? City { get; set; }

        [StringLength(2000)]
        [Display(Name = "آدرس")]
        public virtual string? Address { get; set; }

        [StringLength(2000)]
        [Display(Name = "توضیحات")]
        public virtual string? Description { get; set; }

        [Required]
        [Display(Name = "تاریخ بروزرسانی")]
        public virtual DateTime UpdatedAt { get; set; }
    }
    public class SearchFilterCustomerDto
    {
        [Display(Name = "شناسه")]
        public virtual List<Guid>? Id { get; set; }

        [StringLength(200)]
        [Display(Name = "نام مشتری")]
        public virtual string? Name { get; set; }

        [StringLength(200)]
        [Display(Name = "استان")]
        public virtual string? Province { get; set; }

        [StringLength(200)]
        [Display(Name = "شهرستان")]
        public virtual string? City { get; set; }
    }
    public class ContactsofCustomer
    {
        [Required]
        [Display(Name = "شناسه مشتری")]
        public virtual Guid Id { get; set; }

        [Display(Name = "شناسه مخاطبین")]
        public virtual List<ContactDto?> Contacts { get; set; }
    }

}