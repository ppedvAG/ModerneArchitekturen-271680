namespace HelloPizza.Models;

public class Funghi : Pizza
{
    public Funghi() : base("Funghi")
    {
        Toppings.Add(PizzaToppings.Mushrooms);
        Toppings.Add(PizzaToppings.Cheese);
        Price = 6;
    }
}
