using HelloPizza.Models;

namespace HelloPizza.Decorator
{
    public class PizzaDecorator : Pizza
    {
        private readonly Pizza _pizza;

        public PizzaDecorator(Pizza pizza) : base(pizza.Name)
        {
            _pizza = pizza;
        }

        // Muss fuer jedes Member der Basisklasse ueberschrieben werden
        public override string Description => _pizza.Description;
    }

    public class BoxDecorator : PizzaDecorator
    {
        public BoxDecorator(Pizza pizza) : base(pizza)
        {

        }

        public override string Description => base.Description + ", in Schachtel verpackt";
    }

    public class  ExtraCheeseDecorator : PizzaDecorator
    {
        public ExtraCheeseDecorator(Pizza pizza) : base(pizza)
        {
            Price = pizza.Price + 2; // Preis für zusätzlichen Käse
        }

        public override string Description => base.Description + ", mit zusätzlichen Käse";
    }
}
