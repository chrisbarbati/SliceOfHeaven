using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Pizza> Pizzas {get; set;}
        public DbSet<Topping> Toppings { get; set;}
        public DbSet<PizzaTopping> PizzaToppings { get; set; }

        /*
         * https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.onmodelcreating?view=efcore-7.0
         *  Function called to make more specific model associations than ASP.NET infers from Models.
         *  
         *  https://www.learnentityframeworkcore.com/configuration/fluent-api/haswith-pattern
         *  https://www.learnentityframeworkcore.com/configuration/fluent-api/haskey-method
         *  https://www.learnentityframeworkcore.com/configuration/fluent-api/hasforeignkey-method
         *  https://www.learnentityframeworkcore.com/configuration/fluent-api/hasmany-method
         *  https://www.learnentityframeworkcore.com/configuration/fluent-api/hasone-method
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /**
             *  
             */
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();

            modelBuilder.Entity<PizzaTopping>()
                //Define a composite primary key for each PizzaTopping
                .HasKey(pizzaTopping => new { pizzaTopping.PizzaId, pizzaTopping.ToppingId });

            modelBuilder.Entity<PizzaTopping>()
                //Each PizzaTopping has ONE pizza. One to One.
                .HasOne(pizzaTopping=> pizzaTopping.Pizza)
                //One Pizza may have MANY pizzaToppings associations. One to Many.
                .WithMany(pizza => pizza.PizzaToppings)
                //Has foreign key to correspond to a particular PizzaID.
                .HasForeignKey(pizzaTopping => pizzaTopping.PizzaId);

            modelBuilder.Entity<PizzaTopping>()
                //Each PizzaTopping has ONE topping. One to One.
                .HasOne(pizzaTopping => pizzaTopping.Topping)
                //Each Topping has many PizzaToppings associations, but no Topping has a
                //collection of PizzaTopping related to it. So we call this method 
                //with no arguments (
                .WithMany()
                //Has foreign key to correspong to a particular ToppingID.
                .HasForeignKey(pizzaTopping => pizzaTopping.ToppingId);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaStore.Models.Cart>? Cart { get; set; }

        public DbSet<PizzaStore.Models.Order>? Order { get; set; }
    }
}