namespace HelloSolid.ISP;

public class Creature : IEat, ISleep
{
    public string Name { get; set; }

    public string FavoriteFood { get; set; }

    public Creature()
    {
    }

    public Creature(string name)
    {
        Name = name;
    }

    public void Eat()
    {
        Console.WriteLine($"{Name} is eating {FavoriteFood} and is happy!");
    }

    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping and is happy!");
    }
}
