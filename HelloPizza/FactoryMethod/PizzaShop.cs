using HelloPizza.Models;

namespace HelloPizza.FactoryMethod
{
    /// <summary>
    /// Wir koennten auch die Klasse PizzaFactory nennen, aber von der Fachdomäne her ist es ein PizzaShop, der Pizzen verkauft. 
    /// Daher ist der Name PizzaShop passender.
    /// </summary>
    internal class PizzaShop
    {
        public Pizza CreateMargheritaPizza() => new Margherita();

        public Pizza CreateSalamiPizza() => new Salami();

        public Pizza CreateFunghiPizza() => new Funghi();

        public Pizza CreateByName(string name) => name.ToLower() switch
        {
            "margherita" => CreateMargheritaPizza(),
            "salami" => CreateSalamiPizza(),
            "funghi" => CreateFunghiPizza(),
            _ => throw new ArgumentException($"Pizza '{name}' ist nicht verfügbar.")
        };
    }
}
