using Microsoft.AspNetCore.Mvc;

using PizzaStore.Models;

namespace PizzaStore.Components.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<MenuItem>
            {
                new MenuItem    { //Home button
                    Controller = "Home",
                    Action = "Index",
                    Label = "Home",
                    Authorized = false, //Accessible to users who aren't logged in 
                    AllowedRoles = new List<string> {"Administrator", "Customer"} //Accessible to all roles
                },
                new MenuItem    { //About page
                    Controller = "Home",
                    Action = "About",
                    Label = "About",
                    Authorized = false,
                    AllowedRoles = new List<string> {"Administrator", "Customer"} //Accessible to all roles
                },new MenuItem    { //Admin
                    Controller = "Orders",
                    Action = "Index",
                    Label = "Admin",
                    Authorized = true,
                    AllowedRoles = new List<string> {"Administrator"}, //Accessible to all roles
                    DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "Pizzas", Action = "Index", Label = "Pizzas", AllowedRoles = new List<string> {"Administrator"}},
                    new MenuItem {Controller = "Toppings", Action = "Index", Label = "Toppings", AllowedRoles = new List<string> {"Administrator"}},
                    new MenuItem {Controller = "PizzaAssociations", Action = "Index", Label = "Pizza to Topping Association", AllowedRoles = new List<string> {"Administrator"}},
                    new MenuItem {Controller = "Carts", Action = "Index", Label = "Carts", AllowedRoles = new List<string> {"Administrator"}},
                    new MenuItem {Controller = "Orders", Action = "Index", Label = "Orders", AllowedRoles = new List<string> {"Administrator"}}
                }
                },new MenuItem    { //View Cart
                    Controller = "OrderAPizza",
                    Action = "ViewMyCart",
                    Label = "Cart",
                    Authorized = true,
                    AllowedRoles = new List<string> {"Administrator", "Customer"} //Accessible to all roles
                },
                new MenuItem    {
                    Controller = "Pizzas",
                    Action = "Create",
                    Label = "Order A Pizza",
                    Authorized = false //Link is accessible to users not logged in, but controller will redirect to login
                },
                new MenuItem    {
                    Controller = "Home", 
                    Action = "Privacy", 
                    Label = "Privacy", 
                    Authorized = false , //Accessible to users who aren't logged in
                    AllowedRoles = new List<string> {"Administrator", "Customer"}  //Accessible to all roles
                }
            };
            return View(menuItems); //Becomes model in the view
        }
    }
}
