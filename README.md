# SliceOfHeaven

Slice Of Heaven is an ASP.NET project designed to simulate an online pizza store ordering system. This is simlar to what one would see from Dominos, except 
mine is more functional.

Users can create a pizza, add toppings, edit toppings, and then checkout with the Stripe API. 

Admins can do the above, but can also add new topping selections for the users.

There are three main model classes Pizza, Topping, and PizzaAssociation. Pizza and Topping represent those items respectively, and PizzaAssociation represents a junction
table in the ERD that associates multiple Toppings with one Pizza.
