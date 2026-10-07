using HelloSolid.DIP.Shopping.Core;
using HelloSolid.ISP;

namespace HelloSolid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SampleISP();

            SampleDIP();
        }

        #region ISP Interface Segregation Principle
        private static void SampleISP()
        {
            var bunny = new Creature("Bunny") { FavoriteFood = "Carrots 🥕🥕" };
            EatSomething(bunny);

            var jamieOliver = new Human("Jamie Oliver") { FavoriteFood = "Pasta 🍝" };
            PrepareFoodAndEat(jamieOliver);

            var duffy = CreateCreature<Creature>("Duffy", "Donuts 🍩");
            duffy.Sleep();
        }

        private static void EatSomething(IEat eater)
        {
            eater.Eat();
        }

        private static void PrepareFoodAndEat_BadExample(IHuman human)
        {
            human.CookFood();
            human.Eat();
            human.Sleep();
        }

        // Hier sparen wir uns das Super-Interface IHuman und nutzen die einzelnen Interfaces, die wir wirklich brauchen.
        private static void PrepareFoodAndEat<T>(T human) 
            where T : IChef, IEat, ISleep
        {
            human.CookFood();
            human.Eat();
            human.Sleep();
        }

        private static T CreateCreature<T>(string name, string food) 
            where T : class, IEat, new()
        {
            //var creature = new T
            //{
            //    FavoriteFood = food
            //};

            // Wenn wir Parameter via dem Konstruktur übergeben wollen, müssen wir die Activator-Klasse nutzen.
            T creature = (T)Activator.CreateInstance(typeof(T), name);
            creature.FavoriteFood = food;

            return creature;
        }
        #endregion

        private static void SampleDIP()
        {
            var paymentService = new DIP.Payment.PaymentService();
            var cart = new ShoppingCart(paymentService);
            cart.AddProduct(new Product("Laptop", 999.99m));
            cart.PayOrder(999.99m);
        }
    }
}
