using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

public class Categoria
{
    public int Id {get;set;}
    public string? Nombre {get;set;}
    public List<Producto> Productos {get;set;} = new();
}

public class Producto
{
    public int Id {get;set;}
    public string? Nombre {get;set;}
    public decimal Precio {get;set;}
    public int CategoriaId {get;set;}
    public Categoria? Categoria {get;set;}
}

public class CatalogoDbContext : DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Producto> Productos {get;set;}
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=localhost,1433;Database=CatalogoDb;User Id=sa;Password=Abcd1234!;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new CatalogoDbContext();
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("Conectado a Docker y Base de datos creada con éxito.");
        Categoria categoria = new Categoria { Nombre = "Componentes", Productos = new List<Producto>
        {
            new Producto { Nombre = "Tarjeta Madre", Precio = 150.00m },
            new Producto { Nombre = "Procesador", Precio = 250.00m },
            new Producto { Nombre = "Memoria RAM", Precio = 80.00m }
        }};
        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();

        var listaCategorias = await context.Categorias.Include(c => c.Productos).ToListAsync();
        foreach(var cat in listaCategorias)
        {
            Console.WriteLine($"Categoria: {cat.Nombre}");
            foreach(var prod in cat.Productos)
            {
                Console.WriteLine($"  Producto: {prod.Nombre}, Precio: {prod.Precio}");
            }
        }
    }
}