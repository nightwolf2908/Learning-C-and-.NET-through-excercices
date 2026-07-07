using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public record CanjearPremioCommand(int ClienteId, string NombrePremio, int puntosRequeridos) : IRequest<ResultadoCanje>;
public class ResultadoCanje
{
    public bool EsExitoso { get; set; }
    public string? Mensaje { get; set; }
    public string? CodigoDeRetiro { get; set;}
}

public class CanjearPremioHandler : IRequestHandler<CanjearPremioCommand, ResultadoCanje>
{
    public Task<ResultadoCanje> Handle(CanjearPremioCommand request, CancellationToken cancellationToken)
    {
        if (request.puntosRequeridos > 500)
        {
            return Task.FromResult(new ResultadoCanje{
                EsExitoso = false,
                Mensaje = "Error: No tienes suficientes puntos para canjear este premio.",
                CodigoDeRetiro = null
            });
        }
        else
        {
            return Task.FromResult(
                new ResultadoCanje
                {
                    EsExitoso = true,
                    Mensaje = "Felicidades",
                    CodigoDeRetiro = Guid.NewGuid().ToString()
                }
            );
        }
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        var provider = services.BuildServiceProvider();
        IMediator mediator = provider.GetRequiredService<IMediator>();
        var comando = new CanjearPremioCommand(1, "Premio Especial", 400);
        var resultado = await mediator.Send(comando);
        Console.WriteLine($"Resultado del canje: {resultado.Mensaje}");
        if (resultado.EsExitoso)
        {
            Console.WriteLine($"Codigo de retiro: {resultado.CodigoDeRetiro}");
        }
    }
}