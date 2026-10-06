namespace HelloPizza.Strategy;

public class Bike : IVehicle
{
    public string Name => "Drahtesel 0815";
}


public class PizzaExpress
{
    // Statt einer konrketen Implementierung, wird hier ein Interface verwendet.
    public IVehicle DeliveryStrategy { get; private set; }

    public void Order(string name, int distanceInMeters)
    {
        var pizzaShop = new FactoryMethod.PizzaShop();
        var pizza = pizzaShop.CreateByName(name);
        pizza.Prepare();
        pizza.Bake();

        SelectDeliveryStrategy(distanceInMeters);

        DeliverPizza(pizza);
    }

    private void SelectDeliveryStrategy(int distanceInMeters)
    {
        if (distanceInMeters < 1000)
        {
            DeliveryStrategy = new Bike();
        }
        else if (distanceInMeters < 5000)
        {
            DeliveryStrategy = new Car();
        }
        else
        {
            DeliveryStrategy = new Drone();
        }
    }

    public void DeliverPizza(Models.Pizza pizza)
    {
        Console.WriteLine($"Pizza {pizza.Name} wird mit {DeliveryStrategy.Name} ausgeliefert.");
    }
}