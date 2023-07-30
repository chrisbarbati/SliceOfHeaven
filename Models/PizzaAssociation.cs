using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    public class PizzaAssociation
    {
        /*
         *      Attempt #6 to get the ERD association between Pizza and Topping to work 
         *      correctly. Needs a Junction-Table, which will be represented by this class.
         */

        public Pizza? Pizza {  get; set; }
        public int PizzaId { get; set; }

        public Topping? Topping { get; set; }
        public int ToppingId { get; set;}
    }
}
