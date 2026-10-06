using HelloPizza.Models;

namespace HelloPizza.BuilderPattern;

public class CustomPizza : Pizza
{
    public CustomPizza() : base("Individual")
    {
    }

    public void AddTopping(PizzaToppings topping)
    {
        Toppings.Add(topping);

        // Unerwuenschter Seiteneffekt (DX), Principle of Least Astonishment verletzt
        //Price += .5m; // Preis für jeden zusätzlichen Belag
    }

    // Besser:
    public void AddToppingAndUpdatePrice(PizzaToppings topping)
    {
        AddTopping(topping);
        IncrementPrice();
    }

    // Logik ausfuehren
    private void IncrementPrice()
    {
        Price += .5m; // Preis für jeden zusätzlichen Belag
    }
}
