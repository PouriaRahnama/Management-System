using Management_System.Models.Dtos;
using Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management_System.Controllers
{
    public class AccountController : Controller
    {
        #region Constructor

        private readonly ILogger<AccountController> logger;
        private IAccountService accountService;

        public AccountController(ILogger<AccountController> Logger, IAccountService AccountService)
        {
            logger = Logger;
            accountService = AccountService;
        }

        #endregion

        #region Add
        [Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(AddAccountDto addAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await accountService.Add(addAccountDto);
            if (result == StatusResultDto.Failure)
            {
                ViewData["ErrorMessage"] = "اطلاعات وارد شده معتبر نمی باشد";
                return View();
            }

            TempData["SuccessMessage"] = "ساخت اکانت با موفقیت انجام پذیرفت.";
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            var result = accountService.CheckSignIn(User);
            if (result == StatusResultDto.Entred)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginAccountDto loginAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = accountService.CheckSignIn(User);
            if (result == StatusResultDto.NotEntred)
            {
                var account = await accountService.SignIn(loginAccountDto);
                if (account == StatusResultDto.Lock)
                {
                    ViewData["ErrorMessage"] = "اکانت شما به دلیل 5 بار ورود نا موفق قفل شده است";
                    return View();
                }

                if (account == StatusResultDto.Failure)
                {
                    ViewData["ErrorMessage"] = "اطلاعات وارد شده اشتباه می باشد.";
                    return View();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<RedirectToActionResult> Logout()
        {
            await accountService.SignOut();
            return RedirectToAction("Login");
        }
        #endregion

        [Authorize]
        public async Task<IActionResult> ShowAllUsers()
        {
            var users = await accountService.GetAllUsers();
            return View(users);
        }

    }
}
