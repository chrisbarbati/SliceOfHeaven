namespace PizzaStore.Models
{
    public class PizzaTopping
    {
        /*
         * Learned the hard way (after much troubleshooting) that Objects in ASP.NET
         * don't persist in memory between page-loads. Always figured this would be an 
         * issue, but my Views are downright broken since I can't reference anything
         * that is not stored in the database or I get a NullReferenceException
         * 
         * Need to make a junction table between Pizza and Topping to maintain the 
         * association of which Toppings go on which Pizza.
         * 
         * Associates a PizzaID with a ToppingID. Then we can query the table
         * for one PizzaID, get all results (toppingIDs on that pizza), and maintain
         * the association
         */
        public int ToppingId { get; set; }
        public Topping Topping { get; set; }

        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

    }
}

