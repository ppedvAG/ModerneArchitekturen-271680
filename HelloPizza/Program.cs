using HelloPizza.Adapter;
using HelloPizza.BuilderPattern;
using HelloPizza.Decorator;
using HelloPizza.FactoryMethod;
using HelloPizza.Models;
using HelloPizza.Strategy;

namespace HelloPizza
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("🍕🍕🍕 Hello, Pizza! 🍕🍕🍕");

            Console.WriteLine("\nFactory Pattern:\nStandard-🍕");
            var pizzaShop = new PizzaShop();
            Pizza margherita = pizzaShop.CreateMargheritaPizza();
            Console.WriteLine(margherita);

            Console.WriteLine("\nBuilder Pattern:\tEigene 🍕 zusammengestellt");
            var builder = new PizzaConfigurator();
            Pizza customPizza = builder.AddCheese()
                                     .AddSalami()
                                     .AddMushrooms()
                                     .Build();
            Console.WriteLine(customPizza);

            Console.WriteLine("\nDecorator Pattern:\tVerpackte 🍕");
            Pizza boxedPizza = new BoxDecorator(new ExtraCheeseDecorator(margherita));
            Console.WriteLine(boxedPizza);

            Console.WriteLine("\nAdapter Pattern:\tPfannen-Pizza");
            Pizza panPizza = new PanPizzaAdapter(new PanPizza());
            Console.WriteLine(panPizza);

            Console.WriteLine("\nStrategy Pattern:\tVerschiedene Liefermethoden");
            var pizzaExpress = new PizzaExpress();
            pizzaExpress.Order("Margherita", 9500);
        }
    }
}
