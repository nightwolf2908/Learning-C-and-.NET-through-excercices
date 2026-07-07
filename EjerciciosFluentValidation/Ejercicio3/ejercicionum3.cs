using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentValidation;

public record TransferenciaResult(bool Completada, string Mensaje);

public record RealizarTransferenciaCommand(string CuentaOrigen, string CuentaDestino, decimal Monto) : IRequest<TransferenciaResult>;

public class RealizarTransferenciaValidator : AbstractValidator<RealizarTransferenciaCommand>
{
    public RealizarTransferenciaValidator()
    {
        RuleFor(x => x.CuentaOrigen)
            .NotEmpty().WithMessage("La cuenta de origen es obligatoria.")
            .NotEqual(x => x.CuentaDestino).WithMessage("La cuenta de origen y destino no pueden ser iguales.");

        RuleFor(x => x.CuentaDestino)
            .NotEmpty().WithMessage("La cuenta de destino es obligatoria.")
            .NotEqual(x => x.CuentaOrigen).WithMessage("La cuenta de origen y destino no pueden ser iguales.");

        RuleFor(x => x.Monto)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a cero.");
    }
}

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator != null)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await _validator.ValidateAsync(context, cancellationToken);

            if (!validationResult.IsValid)
            {
                // Si falla, detenemos el pipeline lanzando una excepción de FluentValidation
                throw new ValidationException(validationResult.Errors);
            }
        }
        return await next(); // Si todo es correcto, continúa al Handler
    }
}

public class RealizarTransferenciaHandler : IRequestHandler<RealizarTransferenciaCommand, TransferenciaResult>
{
    public Task<TransferenciaResult> Handle(RealizarTransferenciaCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new TransferenciaResult(true, "Transferencia realizada con exito."));
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMediatR(cfg => {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        // ESTA LÍNEA AGREGA EL INTERCEPTOR AL PIPELINE DE MEDIATR:
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));});
        services.AddScoped<IValidator<RealizarTransferenciaCommand>, RealizarTransferenciaValidator>();
        var provider = services.BuildServiceProvider();
        IMediator mediator = provider.GetRequiredService<IMediator>();
        try
        {
            var command = new RealizarTransferenciaCommand("1234567890", "0348392", 100);
            var result = await mediator.Send(command);
            Console.WriteLine($"Resultado: {result.Completada}, Mensaje: {result.Mensaje}");
        }
        catch(ValidationException ex)
        {
            Console.WriteLine("Errores de validacion: ");
            foreach(var error in ex.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
        }
    }
}