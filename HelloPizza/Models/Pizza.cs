using System;
using System.Collections.Generic;
using System.Text;

namespace HelloPizza.Models;

public class Pizza
{
    public List<PizzaToppings> Toppings { get; } = [];

    public string Name { get; }

    public virtual string Description => $"Pizza {Name} with {string.Join(", ", Toppings)}";

    public decimal Price { get; protected set; } = 5;

    public Pizza(string name)
    {
        Name = name;
    }

    public void Prepare()
    {
        Console.WriteLine($"{Name} vorbereiten...");
    }

    public virtual void Bake()
    {
        Console.WriteLine($"{Name} in Steinofen backen...");
    }

    public override string ToString() => Description;
}
