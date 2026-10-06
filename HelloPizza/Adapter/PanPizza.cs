namespace HelloPizza.Adapter
{
    public class PanPizza
    {
        public string Name => "NY Style Pfannen-Pizza";

        public void PutOilInPan() 
        {
            Console.WriteLine("Öl in die Pfanne geben.");
        }

        public void FryInPan() 
        { 
            Console.WriteLine("In der Pfanne braten."); 
        }
    }
}
