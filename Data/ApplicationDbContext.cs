using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Pizza> Pizzas {get; set;}
        public DbSet<Topping> Toppings { get; set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaStore.Models.Cart>? Cart { get; set; }

        public DbSet<PizzaStore.Models.Order>? Order { get; set; }
    }
}