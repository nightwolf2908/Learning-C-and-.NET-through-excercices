using System.Linq;
using System.Collections.Generic;

public class Producto
{
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}

class Program
{
    static void Main(string[] agrs)
    {
        List<Producto> productos = new List<Producto>
        {
            new Producto { Nombre = "Producto 1", Categoria = "Categoria A", Precio = 10.5m },
            new Producto { Nombre = "Producto 2", Categoria = "Categoria B", Precio = 20.0m },
            new Producto { Nombre = "Producto 3", Categoria = "Categoria A", Precio = 15.0m },
            new Producto { Nombre = "Producto 4", Categoria = "Categoria C", Precio = 30.0m },
            new Producto { Nombre = "Producto 5", Categoria = "Categoria B", Precio = 25.0m }
        };
        List<decimal> precios = productos.Select(p => p.Precio).ToList();
        decimal sumaPrecios = precios.Sum();
        Console.WriteLine($"Suma de precios: {sumaPrecios}");
        decimal precioMaximo = precios.Max();
        Console.WriteLine($"Precio máximo: {precioMaximo}");
    }
}


