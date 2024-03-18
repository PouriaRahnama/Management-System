namespace Management_System.Models.Dtos
{
    public class AccountDto
    {
        [Display(Name = "نام کاربری")]
        public required string Id { get; set; }

        [Display(Name = "نام کاربری")]
        public required string UserName { get; set; }

        [Display(Name = "ایمیل")]
        public required string Email { get; set; }

        [Display(Name = "نقش")]
        public string? RoleName { get; set; }

    }
    public class AddAccountDto
    {
        [Required(ErrorMessage = "الزامی است .")]
        [Display(Name = "نام کاربری")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "الزامی است .")]
        [Display(Name = "ایمیل")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "الزامی است .")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "الزامی است .")]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "رمز های وارد شده یکسان نیست")]
        public required string ConfrimPassword { get; set; }
    }

    public class LoginAccountDto
    {
        [Required(ErrorMessage = "الزامی است .")]
        [Display(Name = "نام کاربری")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "الزامی است .")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public enum StatusResultDto
    {
        Success = 0b001,
        Failure = 0b010,
        Lock = 0b011,
        Entred = 0b100,
        NotEntred = 0b101
    }
}
