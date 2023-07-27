using Microsoft.AspNetCore.Identity;

namespace PizzaStore.Models
{
    public class User : IdentityUser
    {
        public List<Order>? Orders { get; set; }

        public List<Cart>? Carts { get; set; }
    }
}