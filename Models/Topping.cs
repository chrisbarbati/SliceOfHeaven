using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    /**
     * Class for a Pizza Topping object
     * 
     * DB password (not a permanent place to store this)
     * 
     */

    //TODO Add decorators
    public class Topping
    {
        public int Id { get; set; }

        [Required()]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required()]
        [Range(0, 200)]
        public Double Price { get; set; }

        [Required()]
        [Range(0, 3000)]
        public int Calories { get; set; }

        [Required()]
        [Display(Name = "Vegan")]
        public Boolean IsVegan { get;  set; }
    }
}
