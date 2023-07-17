using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    /**
     * Class for a Pizza Topping object
     */

    //TODO Add decorators
    public class Topping
    {
        int Id { get; set; }

        [Required()]
        [MaxLength(100)]
        String Name { get; set; }

        [Required()]
        [Range(0, 200)]
        Double Price { get; set; }

        [Required()]
        [Range(0, 3000)]
        int Calories { get; set; }

        [Required()]
        [Display(Name = "Vegan")]
        Boolean IsVegan { get;  set; }
    }
}
