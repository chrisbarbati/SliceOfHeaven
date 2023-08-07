using Microsoft.AspNetCore.Mvc;

namespace PizzaStore.Controllers
{
    public class OrderAPizzaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
