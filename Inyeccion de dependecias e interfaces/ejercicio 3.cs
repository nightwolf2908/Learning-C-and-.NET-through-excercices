using System;
public interface IChecadorConexion
{
    bool EstaVivo(string ip);
}

public interface INotificadorDevOps
{
    void EnviarAlerta(string mensaje);
}

public class ChecadorHttp : IChecadorConexion
{
    public bool EstaVivo(string ip)
    {
        Console.WriteLine($"Haciendo ping HTTP a {ip}...");
        return false;
    }
}

public class NotificadorDiscord : INotificadorDevOps
{
    public void EnviarAlerta(string mensaje)
    {
        Console.WriteLine($"Enviando alerta a Discord: {mensaje}");
    }
}

public class MonitorServidor
{
    private readonly IChecadorConexion _checadorConexion;
    private readonly INotificadorDevOps _notificadorDevOps;

    public MonitorServidor(IChecadorConexion checadorConexion, INotificadorDevOps notificadorDevOps)
    {
        _checadorConexion = checadorConexion;
        _notificadorDevOps = notificadorDevOps;
    }
    public void ControlarServidor(string ip)
    {
        if (!_checadorConexion.EstaVivo(ip))
        {
            _notificadorDevOps.EnviarAlerta($"El servidor con IP {ip} no responde.");
        }
        else
        {
            Console.WriteLine($"El servidor con IP {ip} está vivo.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        IChecadorConexion checador = new ChecadorHttp();
        INotificadorDevOps notificador = new NotificadorDiscord();
        MonitorServidor monitor = new MonitorServidor(checador, notificador);
        monitor.ControlarServidor("192.168.1.10");
    }
}