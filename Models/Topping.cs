namespace PizzaStore.Models
{
    /**
     * Class for a Pizza Topping object
     */

    //TODO Add decorators
    public class Topping
    {
        int Id { get; set; }
        String Name { get; set; }
        Double Price { get; set; }
        int Calories { get; set; }
        Boolean IsVegan { get;  set; }
    }
}
