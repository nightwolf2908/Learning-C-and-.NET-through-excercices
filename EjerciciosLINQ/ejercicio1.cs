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
    static void Main(string[] args)
    {
        List<Producto> productos = new List<Producto>
        {
            new Producto { Nombre = "Producto 1", Categoria = "Categoria A", Precio = 10.5m },
            new Producto { Nombre = "Producto 2", Categoria = "Categoria B", Precio = 20.0m },
            new Producto { Nombre = "Producto 3", Categoria = "Categoria A", Precio = 15.0m },
            new Producto { Nombre = "Producto 4", Categoria = "Categoria C", Precio = 30.0m },
            new Producto { Nombre = "Producto 5", Categoria = "Categoria B", Precio = 25.0m }
        };
        List<Producto> productosFiltrados = productos.Where(p => p.Categoria == "Categoria A").ToList();
        List<Producto> productosOrdenados = productos.Where(p => p.Precio > 15.0m).OrderByDescending(p => p.Precio).ToList();
        Producto ? buscarProducto = productos.FirstOrDefault(p => p.Nombre == "Producto 3");
        foreach (var producto in productosFiltrados)
        {
            System.Console.WriteLine($"Productos Filtrados Nombre: {producto.Nombre}, Categoria: {producto.Categoria}, Precio: {producto.Precio}");
        }
        foreach (var producto in productosOrdenados)
        {
            System.Console.WriteLine($"Producto Ordenado Nombre: {producto.Nombre}, Categoria: {producto.Categoria}, Precio: {producto.Precio}");
        }
        if (buscarProducto != null)
        {
            System.Console.WriteLine($"Producto encontrado: Nombre: {buscarProducto.Nombre}, Categoria: {buscarProducto.Categoria}, Precio: {buscarProducto.Precio}");
        }
        else
        {
            System.Console.WriteLine("Producto no encontrado.");
        }
    }
}