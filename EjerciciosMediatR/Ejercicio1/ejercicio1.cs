using MediatR;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Logging;

public record ObtenerNombreUsuarioQuery(int Id) : IRequest<string>;
public class ObtenerNombreUsuarioHandler : IRequestHandler<ObtenerNombreUsuarioQuery, string>
{
    public Task<string> Handle(ObtenerNombreUsuarioQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == 1)
        {
            return Task.FromResult("Abdiel");
        }
        else
        {
            return Task.FromResult("Usuario desconocido");
        }
    }
}

class PruebaMediatR
{
    static async Task Main(string[] args)
    {
        
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PruebaMediatR).Assembly));
        var provider = services.BuildServiceProvider();
        IMediator mediator = provider.GetRequiredService<IMediator>();
        var comando = new ObtenerNombreUsuarioQuery(1);
        var resultado = await mediator.Send(comando);
        Console.WriteLine($"El nombre del usuario es: {resultado}");
    }
}