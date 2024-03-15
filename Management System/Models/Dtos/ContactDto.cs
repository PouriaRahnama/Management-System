namespace Management_System.Models.Dtos
{
    public class ContactDto : BaseDto
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "نقش کاربر")]
        public string? Role { get; set; }

        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        [Display(Name = "جنسیت")]
        public string GenderString
        {
            get
            {
                return Gender ? "آقا" : "خانم";
            }
        }

        [StringLength(12)]
        [Display(Name = "تلفن")]
        public string? Phone { get; set; }

        [Display(Name = "داخلی کاربر")]
        [StringLength(5)]
        public string? LocalNumber { get; set; }

        [Display(Name = "موبایل")]
        [StringLength(12)]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [StringLength(100)]
        public string? Email { get; set; }

    }

    public class ShortDetailContactDto
    {
        [Display(Name = "شناسه")]
        public Guid? Id { get; set; }

        [StringLength(100)]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        public string Name
        {
            get => FirstName + " " + LastName;
        }
    }
    public class AddContactDto
    {
        [Display(Name = "شناسه مشتری")]
        public Guid? CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "نقش کاربر")]
        public string? Role { get; set; }

        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        [StringLength(12)]
        [Display(Name = "تلفن")]
        public string? Phone { get; set; }

        [Display(Name = "داخلی کاربر")]
        [StringLength(5)]
        public string? LocalNumber { get; set; }

        [Display(Name = "موبایل")]
        [StringLength(12)]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [StringLength(100)]
        public string? Email { get; set; }
    }
    public class AddContactCustomerDto
    {
        [Required]
        [Display(Name = "شناسه مشتری")]
        public Guid? CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "نقش کاربر")]
        public string? Role { get; set; }

        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        [StringLength(12)]
        [Display(Name = "تلفن")]
        public string? Phone { get; set; }

        [Display(Name = "داخلی کاربر")]
        [StringLength(5)]
        public string? LocalNumber { get; set; }

        [Display(Name = "موبایل")]
        [StringLength(12)]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [StringLength(100)]
        public string? Email { get; set; }
    }
    public class EditContactDto
    {
        [Required]
        [Display(Name = "شناسه")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "نقش کاربر")]
        public string? Role { get; set; }

        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

        [StringLength(12)]
        [Display(Name = "تلفن")]
        public string? Phone { get; set; }

        [Display(Name = "داخلی کاربر")]
        [StringLength(5)]
        public string? LocalNumber { get; set; }

        [Display(Name = "موبایل")]
        [StringLength(12)]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class PatchContactDto
    {
        [Required]
        [Display(Name = "شناسه")]
        public Guid Id { get; set; }

        [StringLength(100)]
        [Display(Name = "نام")]
        public string? FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "نقش کاربر")]
        public string? Role { get; set; }

        [Display(Name = "جنسیت")]
        public bool? Gender { get; set; }

        [StringLength(12)]
        [Display(Name = "تلفن")]
        public string? Phone { get; set; }

        [Display(Name = "داخلی کاربر")]
        [StringLength(5)]
        public string? LocalNumber { get; set; }

        [Display(Name = "موبایل")]
        [StringLength(12)]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "تاریخ آپدیت")]
        public DateTime? UpdatedAt { get; set; }
    }
    public class SearchFilterContactDto
    {
        [Display(Name = "نام مشتری")]
        public Guid CustomerId { get; set; }

        [Display(Name = "نام مخاطب")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی مخاطب")]
        public string? LastName { get; set; }

        [Display(Name = "شماره تلفن")]
        public string? Phone { get; set; }

        [Display(Name = "شماره موبایل")]
        public string? Mobile { get; set; }
    }
    public class AddListofCustomer
    {
        [Display(Name = "شناسه مخاطب")]
        public Guid Id { get; set; }

        [Display(Name = "شناسه مشتری ها")]
        public List<Guid>? Customers { get; set; }
    }
    public class ContactCustomerDto
    {
        [Display(Name = "شناسه")] public Guid Id { get; set; }
        [Display(Name = "نام")] public string FirstName { get; set; }
        [Display(Name = "نام خانوادگي")] public string LastName { get; set; }
    }

}