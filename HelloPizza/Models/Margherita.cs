namespace HelloPizza.Models;

public class Margherita : Pizza
{
    public Margherita() : base("Margherita")
    {
        Toppings.Add(PizzaToppings.Cheese);
        Toppings.Add(PizzaToppings.TomatoSauce);
    }
}
