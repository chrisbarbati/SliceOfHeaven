namespace PizzaStore.Models
{
    
    public enum Cheese
    {
        Mozzarella,
        Goat, //Find a way to make the output nicer so it doesn't appear as "goat" in the dropdown
        Vegan
    }

    public enum Dough
    {
        Regular,
        ThinCrust,
        NoGluten,
        Vegan
    }

    /**
     * Model class for a Pizza object.
     * 
     * Each Pizza has a name, number of calories, List of toppings, etc.
     */

    //TODO Add Decorators

    public class Pizza
    {
        int Id { get; set; }
        String Name { get; set; }
        int Calories { get; set; } // Later, calculate this based on dough, cheese, and toppings
        Dough DoughType { get; set; }
        Cheese CheeseType { get; set; }
        Boolean IsVegan { get; set; } //Later, determine this based on dough, cheese, and toppings
        List<Topping>? Toppings { get; set; } //Optional, toppings not required
    }
}
