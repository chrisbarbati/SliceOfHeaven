using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Controllers
{
    public class PizzaAssociationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzaAssociationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PizzaAssociations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.pizzaAssociations.Include(p => p.Pizza).Include(p => p.Topping);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PizzaAssociations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.pizzaAssociations == null)
            {
                return NotFound();
            }

            var pizzaAssociation = await _context.pizzaAssociations
                .Include(p => p.Pizza)
                .Include(p => p.Topping)
                .FirstOrDefaultAsync(m => m.PizzaId == id);
            if (pizzaAssociation == null)
            {
                return NotFound();
            }

            return View(pizzaAssociation);
        }

        // GET: PizzaAssociations/Create
        public IActionResult Create()
        {
            ViewData["PizzaId"] = new SelectList(_context.Pizzas, "Id", "Name");
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Name");
            return View();
        }

        // POST: PizzaAssociations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PizzaId,ToppingId")] PizzaAssociation pizzaAssociation)
        {
            //When making a new PizzaAssociation, iterate over all pizzas in database
            foreach(Pizza pizza in _context.Pizzas.ToList<Pizza>())
            {
                //Wherever the ID of this pizzaAssociation.PizzaID matches the ID of a pizza
                if(pizza.Id == pizzaAssociation.PizzaId)
                {
                    //If current pizza.PizzaAssociations is null, initialize it
                    if(pizza.PizzaAssociations == null)
                    {
                        pizza.PizzaAssociations = new List<PizzaAssociation>();
                    }

                    //Add this PizzaAssociation to the list so we can iterate later.
                    pizza.PizzaAssociations.Add(pizzaAssociation);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(pizzaAssociation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PizzaId"] = new SelectList(_context.Pizzas, "Id", "Name", pizzaAssociation.PizzaId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Name", pizzaAssociation.ToppingId);
            return View(pizzaAssociation);
        }

        // GET: PizzaAssociations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.pizzaAssociations == null)
            {
                return NotFound();
            }

            var pizzaAssociation = await _context.pizzaAssociations.FindAsync(id);
            if (pizzaAssociation == null)
            {
                return NotFound();
            }
            ViewData["PizzaId"] = new SelectList(_context.Pizzas, "Id", "Name", pizzaAssociation.PizzaId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Name", pizzaAssociation.ToppingId);
            return View(pizzaAssociation);
        }

        // POST: PizzaAssociations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PizzaId,ToppingId")] PizzaAssociation pizzaAssociation)
        {
            if (id != pizzaAssociation.PizzaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pizzaAssociation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzaAssociationExists(pizzaAssociation.PizzaId))
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
            ViewData["PizzaId"] = new SelectList(_context.Pizzas, "Id", "Name", pizzaAssociation.PizzaId);
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Name", pizzaAssociation.ToppingId);
            return View(pizzaAssociation);
        }

        // GET: PizzaAssociations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.pizzaAssociations == null)
            {
                return NotFound();
            }

            var pizzaAssociation = await _context.pizzaAssociations
                .Include(p => p.Pizza)
                .Include(p => p.Topping)
                .FirstOrDefaultAsync(m => m.PizzaId == id);
            if (pizzaAssociation == null)
            {
                return NotFound();
            }

            return View(pizzaAssociation);
        }

        // POST: PizzaAssociations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.pizzaAssociations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.pizzaAssociations'  is null.");
            }
            var pizzaAssociation = await _context.pizzaAssociations.FindAsync(id);
            if (pizzaAssociation != null)
            {
                _context.pizzaAssociations.Remove(pizzaAssociation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PizzaAssociationExists(int id)
        {
          return (_context.pizzaAssociations?.Any(e => e.PizzaId == id)).GetValueOrDefault();
        }
    }
}
