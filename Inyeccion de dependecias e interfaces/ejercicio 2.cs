using System;

public interface ILogger
{
    void Registrar(string mensaje);
}

public interface IRepositorioUsuario
{
    void Guardar(string nombreUsuario);
}

public class LoggerConsola : ILogger
{
    public void Registrar(string mensaje)
    {
        Console.WriteLine($"[LOG]: {mensaje}");
    }
}

public class SqlRepositorioUsuario : IRepositorioUsuario
{
    private readonly ILogger _logger;
    public SqlRepositorioUsuario(ILogger logger)
    {
        _logger = logger;
    }
    public void Guardar(string nombreUsuario)
    {
        _logger.Registrar($"Guardando usuario {nombreUsuario} en SQL Server...");
    }
}

public class ServicioUsuario
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    public ServicioUsuario(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }
    public void RegistrarUsuario(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre de usuario no puede estar vacio");
        }
        _repositorioUsuario.Guardar(nombre);
    }
}

class Program
{
    static void Main(string[] args)
    {
        ILogger logger = new LoggerConsola();
        IRepositorioUsuario repositorioUsuario = new SqlRepositorioUsuario(logger);
        ServicioUsuario servicioUsuario = new ServicioUsuario(repositorioUsuario);
        servicioUsuario.RegistrarUsuario("JuanPerez");
    }
}