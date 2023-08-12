using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace PizzaStore.Controllers
{
    [Authorize()]
    public class OrderAPizzaController : Controller
    {
        //Context
        private ApplicationDbContext _context;

        private IConfiguration _configuration;

        public OrderAPizzaController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //[HttpPost]
        public async Task<IActionResult> AddToCart(int pizzaId)
        {
            // Get our logged in user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Get the user's cart
            var cart = await _context.Cart
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            // If user does not have a cart, create a new one
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _context.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            // Get the pizza from the database
            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(pizza => pizza.Id == pizzaId);

            // If there's no product
            if (pizza == null)
            {
                return NotFound();
            }

            // Create a cart item
            var cartItem = new CartItem
            {
                Cart = cart,
                Pizza = pizza,
                Quantity = 1, //Implement this later
                Price = (decimal)pizza.Price
            };

            // If the model state is valid, execute the changes
            if (ModelState.IsValid)
            {
                await _context.AddAsync(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewMyCart");
            }
            else
            {
                return NotFound();
            }
        }

        // View the cart
        //[HttpPost]
        public async Task<IActionResult> ViewMyCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Cart
                .Include(cart => cart.User)
                .Include(cart => cart.CartItems)
                .ThenInclude(cartItem => cartItem.Pizza)
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            return View(cart);
        }

        [Authorize()]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Cart
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            if(cart == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(cartItem => cartItem.Cart == cart && cartItem.Id == cartItemId);

            if(cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewMyCart");
            }

            return View();
        }

        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Cart
                .Include(cart => cart.User)
                .Include(cart => cart.CartItems)
                .ThenInclude(cartItem => cartItem.Pizza)
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            var Order = new PizzaStore.Models.Order {
                UserId = userId,
                Cart = cart,
                Total = cart.CartItems.Sum(cartItem => cartItem.Quantity * cartItem.Price),
                ShippingAddress = "",
                PaymentMethod = PaymentMethods.VISA
            };

            ViewData["PaymentMethods"] = new SelectList(Enum.GetValues(typeof(PaymentMethods)));

            return View(Order);
        }

        public async Task<IActionResult> Payment(string ShippingAddress, PaymentMethods paymentMethod)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.Cart
                .Include(cart => cart.CartItems)
                .FirstOrDefaultAsync(cart => cart.UserId == userId && cart.Active == true);

            if (cart == null)
            {
                return NotFound();
            }

            //Add order data to the session
            HttpContext.Session.SetString("ShippingAddress", ShippingAddress);
            HttpContext.Session.SetString("PaymentMethod", paymentMethod.ToString());

            //Set the Stripe API Key
            StripeConfiguration.ApiKey = _configuration.GetSection("Stripe")["SecretKey"];

            //Set Stripe options
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(cart.CartItems.Sum(cartItem => cartItem.Quantity * cartItem.Price) * 100),
                            Currency = "cad",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Slice Of Heaven Purchase",
                            },
                        },
                        Quantity = 1
                    },
                },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                Mode = "payment",
                SuccessUrl = "https://" + Request.Host + "/OrderAPizza/SaveOrder", //Check this out after
                CancelUrl = "https://" + Request.Host + "/OrderAPizza/ViewMyCart",
            };

            System.Diagnostics.Debug.WriteLine("https://" + Request.Host + "/Home/About");
            System.Diagnostics.Debug.WriteLine("https://" + Request.Host + "/OrderAPizza/ViewMyCart");

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
