using HelloPizza.Models;

namespace HelloPizza.BuilderPattern;

internal class PizzaConfigurator
{
    private List<PizzaToppings> toppings = new();

    public PizzaConfigurator AddCheese()
    {
        toppings.Add(PizzaToppings.Cheese);
        return this;
    }

    public PizzaConfigurator AddSalami()
    {
        toppings.Add(PizzaToppings.Salami);
        return this;
    }

    public PizzaConfigurator AddMushrooms()
    {
        toppings.Add(PizzaToppings.Mushrooms);
        return this;
    }

    public PizzaConfigurator AddPineapple()
    {
        toppings.Add(PizzaToppings.Pineapple);
        return this;
    }

    public PizzaConfigurator AddOlives()
    {
        toppings.Add(PizzaToppings.BlackOlives);
        return this;
    }

    public Pizza Build()
    {
        var pizza = new CustomPizza();
        pizza.Toppings.AddRange(toppings);
        return pizza;
    }
}
