using HelloPizza.Models;

namespace HelloPizza.Adapter
{
    public class PanPizzaAdapter : Pizza
    {
        private readonly PanPizza _panPizza;

        public PanPizzaAdapter(PanPizza panPizza) : base(panPizza.Name)
        {
            _panPizza = panPizza;
        }

        public override string Description => $"Pfannen-Pizza: {_panPizza.Name}";
        
        public override void Bake()
        {
            _panPizza.PutOilInPan();
            _panPizza.FryInPan();
        }
    }
}
