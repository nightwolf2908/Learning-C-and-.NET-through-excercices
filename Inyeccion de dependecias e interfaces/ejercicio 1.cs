using System;

public interface IMetodoPago{
    public void Procesar(decimal monto);
}

public class PagoTarjeta : IMetodoPago
{
    public void Procesar(decimal monto)
    {
        Console.WriteLine($"Pagando {monto} con tarjeta de credito");
    }
}

public class Paypal : IMetodoPago
{
    public void Procesar(decimal monto)
    {
        Console.WriteLine($"Pagando {monto} con Paypal");
    }
}

public class ProcesadorOrdenes
{
    private readonly IMetodoPago _metodoPago;
    public ProcesadorOrdenes(IMetodoPago metodoPago)
    {
        _metodoPago = metodoPago;
    }
    public void CompletarCompra(decimal total)
    {
        _metodoPago.Procesar(total);
    }
}

class Program
{
    static void Main(string[] args)
    {
        IMetodoPago metodoPago = new PagoTarjeta();
        ProcesadorOrdenes procesador = new ProcesadorOrdenes(metodoPago);
        procesador.CompletarCompra(100.00m);

        IMetodoPago metodoPagoPaypal = new Paypal();
        ProcesadorOrdenes procesadorPaypal = new ProcesadorOrdenes(metodoPagoPaypal);
        procesadorPaypal.CompletarCompra(200.00m);
    }
}