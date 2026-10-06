using HelloPizza.Models;

namespace HelloPizza.Decorator
{
    public class PizzaDecoratorCastable
    {
        private readonly Pizza _pizza;

        public PizzaDecoratorCastable(Pizza pizza)
        {
            _pizza = pizza;
        }

        // Eleganter: cast-operator ueberladen
        public static implicit operator Pizza(PizzaDecoratorCastable decorator) => decorator._pizza;
    }
}
