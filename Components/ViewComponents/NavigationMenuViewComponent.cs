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
                },new MenuItem    {
                    Controller = "Pizzas",
                    Action = "Index",
                    Label = "Pizzas",
                    DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "Pizzas", Action = "Index", Label = "List", AllowedRoles = new List<string> {"Administrator"}},
                    new MenuItem {Controller = "Pizzas", Action = "Create", Label = "Create", AllowedRoles = new List<string> {"Administrator"}}
                },
                    Authorized = true, //Link is accessible to users not logged in, but controller will redirect to login
                    AllowedRoles = new List<string> {"Administrator"}
                },
                new MenuItem    {
                    Controller = "Pizzas",
                    Action = "Create",
                    Label = "Order A Pizza",
                    Authorized = false //Link is accessible to users not logged in, but controller will redirect to login
                },
                new MenuItem    {
                    Controller = "Toppings", 
                    Action = "Index", 
                    Label = "View Toppings", 
                    DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "Toppings", Action = "Index", Label = "List", AllowedRoles = new List<string> {"Administrator"}},
                    new MenuItem {Controller = "Toppings", Action = "Create", Label = "Create", AllowedRoles = new List<string> {"Administrator"}}
                }, 
                    Authorized = true , //Not accessible to users who aren't logged in
                    AllowedRoles = new List<string> {"Administrator" }  //Accessible to administrators only
                },
                new MenuItem    {
                    Controller = "PizzaAssociations",
                    Action = "Index",
                    Label = "Pizza Associations",
                    DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "PizzaAssociations", Action = "Index", Label = "List", AllowedRoles = new List<string> {"Administrator"}},
                    new MenuItem {Controller = "PizzaAssociations", Action = "Create", Label = "Create", AllowedRoles = new List<string> {"Administrator"}}
                }, 
                    Authorized = true , //Not accessible to users who aren't logged in
                    AllowedRoles = new List<string> {"Administrator" }  //Accessible to administrators only
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
