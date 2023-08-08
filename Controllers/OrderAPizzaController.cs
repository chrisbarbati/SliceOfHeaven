using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Controllers
{
    public class OrderAPizzaController : Controller
    {
        //Context
        private ApplicationDbContext _context;

        public OrderAPizzaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Get all of the information we need to pass to the view
            //May need to expand this later
            List<Topping> toppings = await _context.Toppings.OrderBy(topping => topping.Id).ToListAsync();

            return View(toppings);
        }
    }
}
