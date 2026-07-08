using System;
using System.Threading.Tasks;

class Ejercicio1
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Preparando ingredientes...");
        var resultado = await HornearPizzaAsync();
        Console.WriteLine(resultado);
    }
    static async Task<string> HornearPizzaAsync()
    {
        await Task.Delay(4000);
        return "Pizza lista!";
    }
}