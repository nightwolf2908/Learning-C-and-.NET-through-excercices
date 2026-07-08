using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

public class ProductoBodega
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public int Stock { get; set; }
}

public class BodegaDbContext : DbContext
{
    public DbSet<ProductoBodega> Productos {get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=localhost,1433;Database=BodegaDb;User Id=sa;Password=Abcd1234!;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new BodegaDbContext();
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("Conectado a Docker y Base de datos creada con éxito.");
        ProductoBodega producto = new ProductoBodega { Nombre = "Teclado Mecanico", Stock = 10 };
        context.Productos.Add(producto);
        await context.SaveChangesAsync();
        Console.WriteLine("Producto agregado con éxito.");
        var productocambiado = await context.Productos.FirstOrDefaultAsync(p => p.Nombre == "Teclado Mecanico");
        if(productocambiado != null)
        {
            productocambiado.Stock = 5;
            await context.SaveChangesAsync();
        }
        var productoeliminado = await context.Productos.FirstOrDefaultAsync(p => p.Nombre == "Teclado Mecanico");
        if(productoeliminado != null)
        {
            Console.WriteLine($"Eliminando producto: Id: {productoeliminado.Id}, Nombre: {productoeliminado.Nombre}, Stock: {productoeliminado.Stock}");
            context.Productos.Remove(productoeliminado);
            await context.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("No se encontro el producto para eliminar.");
        }
        List<ProductoBodega> productosremanentes = await context.Productos.ToListAsync();
        if (productosremanentes.Count > 0)
        {
            Console.WriteLine("Productos en la base de datos: ");
            foreach(var p in productosremanentes)
            {
                Console.WriteLine($"Id: {p.Id}, Nombre: {p.Nombre}, Stock: {p.Stock}");
            }
        }
        else
        {
            Console.WriteLine("No hay productos en la base de datos.");
        }
    }
}