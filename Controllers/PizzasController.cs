using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Controllers
{
    [Authorize()]
    public class PizzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
              return _context.Pizzas != null ? 
                          View(await _context.Pizzas.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Pizzas'  is null.");
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.PizzaAssociations = _context.pizzaAssociations.ToList().Where(p => p.PizzaId == id).ToList();
            ViewBag.Toppings = _context.Toppings.ToList();

            if (id == null || _context.Pizzas == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,DoughType,CheeseType")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {

                if(pizza.Name == null) //Set a default name if the user chooses not to add one
                {
                    pizza.Name = "Your pizza";
                }

                pizza.Price = 20;//Base price of a pizza is $20.


                if(pizza.DoughType == Dough.Vegan && (pizza.CheeseType == Cheese.Vegan || pizza.CheeseType == Cheese.None))
                {
                    pizza.IsVegan = true; //Set whether the base pizza is vegan or not.
                }
                else
                {
                    pizza.IsVegan = false; //Toppings will also affect this
                }

                if (pizza.CheeseType == Cheese.None)
                { //Pizza has 600 calories from dough alone
                    pizza.Calories = 600;
                }
                else
                { //Or 1000 when cheese is added
                    pizza.Calories = 1000;
                }

                if (pizza.DoughType == Dough.NoGluten)
                {//Gluten-free pizza has a different crust
                    pizza.IsGlutenFree = true;
                    pizza.ImagePath = "crust2.png";
                }
                else
                {//All other pizzas have crust1
                    pizza.IsGlutenFree = false;
                    pizza.ImagePath = "crust1.png";
                }

                _context.Add(pizza);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "PizzaAssociations", new {pizzaId = pizza.Id});
            }
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Calories,DoughType,CheeseType,IsVegan,ImagePath")] Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaExists(pizza.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pizzas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pizzas'  is null.");
            }
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza != null)
            {
                _context.Pizzas.Remove(pizza);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaExists(int id)
        {
          return (_context.Pizzas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
