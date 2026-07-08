using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class Cliente
{
    public int Id { get; set; }
    public string ? Nombre { get; set; }
    public string ? Email { get; set; }
}

public class TiendaDbContext : DbContext
{
    public DbSet<Cliente> Clientes {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=localhost,1433;Database=TiendaDb;User Id=sa;Password=Abcd1234!;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }
}

class Program
{
    static async Task Main(string[] agrs)
    {
        using var context = new TiendaDbContext();
        await context.Database.EnsureCreatedAsync();
        Console.WriteLine("Conectado a Docker y Base de datos creada con éxito.");
        Cliente cliente = new Cliente { Nombre = "Juan", Email = "juan@example.com" };
        context.Clientes.Add(cliente);
        await context.SaveChangesAsync();
        Console.WriteLine("Cliente agregado con éxito.");

        var clientes = await context.Clientes.ToListAsync();
        Console.WriteLine("Clientes en la base de datos:");
        foreach (var c in clientes)
        {
            Console.WriteLine($"Id: {c.Id}, Nombre: {c.Nombre}, Email: {c.Email}");
        }
    }
}
