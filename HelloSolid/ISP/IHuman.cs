namespace HelloSolid.ISP
{
    /// <summary>
    /// Super-Interface was mehrere Interfaces zusammenfasst. Das ist nicht gut, da nicht alle Klassen alle Methoden implementieren müssen.
    /// Verstößt gegen das Interface Segregation Principle (ISP)
    /// Unendlich viele Kombinationen von Interfaces sind möglich, aber nicht alle Klassen müssen alle Methoden implementieren.
    /// </summary>
    public interface IHuman : IChef, IEat, ISleep
    {
    }
}