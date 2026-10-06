namespace HelloPizza.Models;

public class Salami : Pizza
{
    public Salami() : base("Salami Pizza")
    {
        Toppings.Add(PizzaToppings.TomatoSauce);
        Toppings.Add(PizzaToppings.Cheese);
        Toppings.Add(PizzaToppings.Salami);
    }
}
