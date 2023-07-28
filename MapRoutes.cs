namespace PizzaStore
{
    public class MapRoutes
    {
        public static void MapRoute(IEndpointRouteBuilder app)
        {
            //Remove the /home/ from the paths for about, privacy, etc
            app.MapControllerRoute(
                name: "about",
                pattern: "about",
                defaults: new { controller = "Home", action = "About" });

            app.MapControllerRoute(
                name: "privacy",
                pattern: "privacy",
                defaults: new { controller = "Home", action = "Privacy" });

            //Add the "Create a Pizza" path
            app.MapControllerRoute(
                name: "createapizza",
                pattern: "createapizza",
                defaults: new {controller = "Pizzas", action = "Create"});

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
