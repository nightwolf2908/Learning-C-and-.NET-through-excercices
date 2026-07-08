using System;
using System.Threading.Tasks;

class Ejercicio2
{
    static async Task Main(string[] args)
    {
        
    
    Task<string> hacerCafe = HacerCafeAsync();
    Task<string> hacerPan = HacerPanAsync();

    Console.WriteLine("Preparando desayuno...");
    
    await Task.WhenAll(hacerCafe, hacerPan);
    Console.WriteLine(hacerCafe.Result);
    Console.WriteLine(hacerPan.Result);
    }
    static async Task<string> HacerCafeAsync()
    {
        await Task.Delay(2000);
        return "Cafe listo";
    }
    static async Task<string> HacerPanAsync()
    {
        await Task.Delay(3000);
        return "Pan listo";
    }
}