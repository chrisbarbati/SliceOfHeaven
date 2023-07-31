using Microsoft.AspNetCore.Identity;

namespace PizzaStore.Models
{
    public class User : IdentityUser
    {
        /*
         *  Test User Info
         *  Username: chris.barbati@gmail.com
         *  password: Password1!
         *  
         *  
         */

        public List<Order>? Orders { get; set; }

        public List<Cart>? Carts { get; set; }
    }
}