using System.ComponentModel.DataAnnotations;

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
        public int Id { get; set; }


        [Required()]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required()]
        [Range(0, 30000)] //Unsure how many calories are in an entire pizza, especially one loaded with toppings
        public int Calories { get; set; } // Later, calculate this based on dough, cheese, and toppings

        [Required()]
        public Dough DoughType { get; set; }

        public Cheese CheeseType { get; set; } //Not required, as cheese is technically optional

        [Required()]
        public Boolean IsVegan { get; set; } //Later, determine this based on dough, cheese, and toppings

        public List<Topping>? Toppings { get; set; } //Optional, toppings not required
    }
}
