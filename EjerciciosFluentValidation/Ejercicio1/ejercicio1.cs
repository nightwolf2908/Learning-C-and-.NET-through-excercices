using FluentValidation;

public class Auto
{
    public string ? Patente {get; set;}
    public string ? Marca {get; set;}
    public int AnioModelo {get; set;}

}

public class AutoValidator : AbstractValidator<Auto>
{
    public AutoValidator()
    {
        RuleFor(auto => auto.Patente).NotEmpty()
            .WithMessage("La patente no puede estar vacia")
            .Length(6)
            .WithMessage("La patente debe tener 6 caracteres");

        RuleFor(auto => auto.Marca).NotEmpty()
            .WithMessage("La marca no puede estar vacia");
        
        RuleFor(auto => auto.AnioModelo).GreaterThanOrEqualTo(2010)
            .WithMessage("El año del modelo debe ser mayor o igual a 2010")
            .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("El año del modelo no puede ser mayor al año actual");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var auto = new Auto
        {
            Patente = "ABC123",
            Marca = "Toyota",
            AnioModelo = 2022
        };
        var validator = new AutoValidator();
        var resultado = validator.Validate(auto);

        if (!resultado.IsValid)
        {
            Console.WriteLine("Errores de validacion encontrados:");
            foreach (var error in resultado.Errors)
            {
                Console.WriteLine($"Propiedad: {error.PropertyName} | Error: {error.ErrorMessage}");
            }
        }
        else
        {
            Console.WriteLine("El auto es valido.");
        }
    }
}