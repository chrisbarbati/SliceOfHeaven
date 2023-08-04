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
            ViewBag.AvailableToppings = _context.Toppings.ToList();

            ViewData["PizzaId"] = new SelectList(_context.Pizzas, "Id", "Name");
            ViewData["ToppingId"] = new SelectList(_context.Toppings, "Id", "Name");
            return View();
        }

        // POST: PizzaAssociations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToppingId")] PizzaAssociation pizzaAssociation, int[] selectedToppingIDs)
        {
            //PizzaId,
            pizzaAssociation.PizzaId = _context.Pizzas.OrderBy(p => p.Id).Last().Id;

            foreach (int selectedToppingID in selectedToppingIDs)
            {
                System.Diagnostics.Debug.WriteLine(selectedToppingID);
            }
            

            if (ModelState.IsValid)
            {
                foreach(int selectedToppingID in selectedToppingIDs)
                {
                    PizzaAssociation PA = new PizzaAssociation { 
                        PizzaId = pizzaAssociation.PizzaId, 
                        ToppingId = selectedToppingID, 
                        //We can use .First() here, as pizza and topping Ids are unique
                        //Not actually sure if this is necessary, but implemented it when trying to fix
                        //a different issue. Leaving it for now
                        Pizza = _context.Pizzas.First(p => p.Id == pizzaAssociation.PizzaId), 
                        Topping = _context.Toppings.First(t => t.Id == selectedToppingID)
                    };
                    
                    _context.Add(PA);
                    await _context.SaveChangesAsync();
                }
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
