using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;
using System.Reflection.Emit;

namespace PizzaStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Pizza> Pizzas {get; set;}
        public DbSet<Topping> Toppings { get; set;}
        public DbSet<PizzaAssociation> pizzaAssociations { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Fix the cart issue
            builder.Entity<Order>()
                .HasOne(o => o.Cart)
                .WithMany()
                .HasForeignKey(o => o.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CartItem>()
                .HasOne(o => o.Cart)
                .WithMany(o => o.CartItems)
                .HasForeignKey(o => o.CartId)
                .OnDelete(DeleteBehavior.Restrict);

            /*
             * Still having lots of trouble with this
             * 
             * https://stackoverflow.com/questions/73894772/how-to-save-an-object-containing-a-list-of-objects-to-database-in-asp-net-mvc-wi
             * https://stackoverflow.com/questions/73229203/how-to-add-to-junction-table-of-many-to-many-relationship-in-mvc
             * 
             * https://www.learnentityframeworkcore.com/configuration/fluent-api/haskey-method
             * https://www.learnentityframeworkcore.com/configuration/fluent-api/hasone-method
             * https://www.learnentityframeworkcore.com/configuration/fluent-api/hasmany-method
             * 
             * https://www.learnentityframeworkcore.com/configuration/fluent-api/withone-method
             * https://www.learnentityframeworkcore.com/configuration/fluent-api/withmany-method
             * 
             */


            //Handles the basic stuff
            base.OnModelCreating(builder);

            builder.Entity<PizzaAssociation>()
                //Define a composite primary key for each PizzaAssociation
                .HasKey(pizzaAssociation => new { pizzaAssociation.PizzaId, pizzaAssociation.ToppingId });

            builder.Entity<PizzaAssociation>()
                //Each PizzaAssociation has ONE pizza. One to One.
                .HasOne(pizzaTopping => pizzaTopping.Pizza)
                //One Pizza may have MANY pizzaAssociation. One to Many.
                .WithMany(pizza => pizza.PizzaAssociations)
                //Has foreign key to correspond to a particular PizzaID.
                .HasForeignKey(pizzaTopping => pizzaTopping.PizzaId);

            builder.Entity<PizzaAssociation>()
                //Each PizzaAssociation has ONE topping. One to One.
                .HasOne(pizzaTopping => pizzaTopping.Topping)
                //Each Topping has many PizzaAssociation, but no Topping has a
                //collection of PizzaAssociation related to it. So we call this method 
                //with no arguments
                .WithMany()
                //Has foreign key to correspong to a particular ToppingID.
                .HasForeignKey(pizzaTopping => pizzaTopping.ToppingId);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}