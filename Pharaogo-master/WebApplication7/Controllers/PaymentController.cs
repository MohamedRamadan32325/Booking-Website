using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index(decimal price)
        {
            return View(price);
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
