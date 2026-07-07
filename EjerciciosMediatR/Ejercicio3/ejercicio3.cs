using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public record AlertaSeguridadNotificacion(string Zona, DateTime HoraDeteccion) : INotification;

public class EncenderLucesHandler : INotificationHandler<AlertaSeguridadNotificacion>
{
    public Task Handle(AlertaSeguridadNotificacion notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Encendiendo todas las luces de la zona: {notification.Zona}!");
        return Task.CompletedTask;
    }
}

public class NotificarGuardaHandler : INotificationHandler<AlertaSeguridadNotificacion>
{
    public Task Handle(AlertaSeguridadNotificacion notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Correo enviado al guardia sobre la alerta en {notification.Zona} generada a las {notification.HoraDeteccion}");
        return Task.CompletedTask;
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();
        await mediator.Publish(new AlertaSeguridadNotificacion("Zona 1", DateTime.Now));

    }
}

