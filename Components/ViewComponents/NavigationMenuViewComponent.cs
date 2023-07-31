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
                new MenuItem    {Controller = "Home", Action = "Index", Label = "Home"},
                new MenuItem    {Controller = "Home", Action = "About", Label = "About"},
                new MenuItem    {Controller = "PizzasController", Action = "Index", Label = "View Pizzas", DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "PizzasController", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "PizzasController", Action = "Create", Label = "Create"}
                } },
                new MenuItem    {Controller = "ToppingsController", Action = "Index", Label = "View Toppings", DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "ToppingsController", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "ToppingsController", Action = "Create", Label = "Create"}

                } },
                new MenuItem    {Controller = "PizzaAssociationsController", Action = "Index", Label = "Pizza Toppings", DropdownItems = new List<MenuItem>
                {
                    new MenuItem {Controller = "PizzaAssociationsController", Action = "Index", Label = "List"},
                    new MenuItem {Controller = "PizzaAssociationsController", Action = "Create", Label = "Create"}
                } },
                new MenuItem    {Controller = "Home", Action = "Privacy", Label = "Privacy"}
            };
            return View(menuItems); //Becomes model in the view
        }
    }
}
