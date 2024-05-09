namespace Management_System.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            string? myData = TempData["SuccessMessage"] as string;
            TempData["Success"] = myData;
            return View();
        }
    }
}
