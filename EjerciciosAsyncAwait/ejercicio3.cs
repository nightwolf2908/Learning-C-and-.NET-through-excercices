using System;
using System.Threading.Tasks;

class Ejercicio3
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Cajero automático iniciado.");
        try
        {
            var resultado = await RetirarDineroAsync(90);
            Console.WriteLine(resultado);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        static async Task<string> RetirarDineroAsync(int cantidad)
        {
            decimal saldo = 100m;
            await Task.Delay(2000); // Simula el tiempo de procesamiento
            if (cantidad > saldo)
            {
                throw new InvalidOperationException("Saldo insuficiente.");
            }
            saldo -= cantidad;
            return $"Retiro exitoso. Saldo restante: {saldo}"; 
        }
        
    }
}