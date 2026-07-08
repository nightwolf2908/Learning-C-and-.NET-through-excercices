public interface INotificador
{
    void EnviarMensaje(string mensaje);
}

public class NotificadorEmail : INotificador
{
    private string DireccionCorreo;
    public NotificadorEmail(string DireccionCorreo)
    {
        this.DireccionCorreo = DireccionCorreo;
    }
    public void EnviarMensaje(string mensaje)
    {
        Console.WriteLine($"Enviando Email a {DireccionCorreo}: {mensaje}");
    }
}

public class NotificadorSMS : INotificador
{
    private string NumeroCelular;
    public NotificadorSMS(string NumeroCelular)
    {
        this.NumeroCelular = NumeroCelular;
    }
    public void EnviarMensaje(string mensaje)
    {
        Console.WriteLine($"Enviando SMS a {NumeroCelular}: {mensaje}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<INotificador> notificadores = new List<INotificador>();
        notificadores.Add(new NotificadorEmail("abdielitopro4800@gmail.com"));
        notificadores.Add(new NotificadorSMS("8182062242"));
        foreach (var notificador in notificadores)
        {
            notificador.EnviarMensaje("Tu paquete ha salido de almacen");
        }

    }
}