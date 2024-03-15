using Management_System.ConfigApp;
using System.ComponentModel.DataAnnotations;

namespace Management_System.Models.Dtos
{
    /// <summary>
    /// Base Dto
    /// </summary>
    public abstract class BaseDto
    {
        [Display(Name = "شناسه")]
        public Guid Id { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; }

        public string CreateDate
        {
            get
            {
                return CreatedAt.ToPersian();
            }
        }

        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "حذف پیشفرض")]
        public bool IsDeleted { get; set; }

        [Display(Name = "تاریخ حذف")]
        public DateTime? DeletedAt { get; set; }
    }
}