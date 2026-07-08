using System;
public class Animal
{
    protected string Nombre { get; set; }

    public Animal(string nombre)
    {
        Nombre = nombre;
    }
    public virtual void HacerSonido()
    {
        Console.WriteLine("El animal hace un sonido generico.");
    }
}

public class Perro : Animal
{
    
    public Perro(string nombre) : base(nombre)
    {
    }

    public override void HacerSonido()
    {
        Console.WriteLine($"{Nombre} dice: guau guau");
    }
}

public class Gato : Animal
{
    public Gato(string nombre) : base(nombre){}

    public override void HacerSonido()
    {
        Console.WriteLine($"{Nombre} dice: miau miau");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Animal miPerro = new Perro("Firulais");
        Animal miGato = new Gato("Michi");
        miPerro.HacerSonido();
        miGato.HacerSonido();
    }
}
