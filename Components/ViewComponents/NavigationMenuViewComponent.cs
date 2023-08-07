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
                new MenuItem    {Controller = "Home", Action = "Index", Label = "Home", Authorized = false, AllowedRoles = new List<string> {"Administrator", "Customer"}},
                new MenuItem    {Controller = "Home", Action = "About", Label = "About", Authorized = false, AllowedRoles = new List<string> {"Administrator", "Customer"}},
                new MenuItem    {Controller = "Pizzas", Action = "Index", Label = "View Pizzas", DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "Pizzas", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "Pizzas", Action = "Create", Label = "Create"}
                }, Authorized = false },
                new MenuItem    {Controller = "Toppings", Action = "Index", Label = "View Toppings", DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "Toppings", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "Toppings", Action = "Create", Label = "Create"}
                }, Authorized = true , AllowedRoles = new List<string> {"Administrator" } },
                new MenuItem    {Controller = "PizzaAssociations", Action = "Index", Label = "Pizza Toppings", DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "PizzaAssociations", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "PizzaAssociations", Action = "Create", Label = "Create"}
                }, Authorized = true , AllowedRoles = new List<string> {"Administrator" } },
                new MenuItem    {Controller = "Home", Action = "Privacy", Label = "Privacy", Authorized = false , AllowedRoles = new List<string> {"Administrator", "Customer"} }
            };
            return View(menuItems); //Becomes model in the view
        }
    }
}
