namespace HelloSolid.ISP;

public class Human : Creature, IChef, IHuman
{
    public Human(string name) : base(name)
    {
    }

    public void CookFood()
    {
        Console.WriteLine($"{Name} is preparing {FavoriteFood}!");
    }
}
