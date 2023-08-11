using Humanizer;
using Microsoft.AspNetCore.Identity;

namespace PizzaStore.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public String UserId { get; set; }

        public bool Active { get; set; } = true;

        public User? User { get; set; }

        public List<CartItem>? CartItems { get; set; }

        public Order? Order { get; set; }
    }
}