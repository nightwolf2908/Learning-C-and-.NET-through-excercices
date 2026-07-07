using FluentValidation;
using System.Collections.Generic;
public class DetalleProducto
{
    public string? NombreProducto {get; set;}
    public decimal Precio {get; set;}
}

public class OrdenCompra
{
    public string? NumeroOrden {get; set;}
    public string? CorreoCliente {get; set;}
    public List<DetalleProducto> Productos {get; set;} = new();
}

public class DetalleProductoValidator : AbstractValidator<DetalleProducto>
{
    public DetalleProductoValidator()
    {
        RuleFor(x => x.NombreProducto).NotEmpty();
        RuleFor(x => x.Precio).GreaterThan(0);
    }
}

public class OrdenCompraValidator : AbstractValidator<OrdenCompra>
{
    public OrdenCompraValidator()
    {
        RuleFor(x => x.CorreoCliente).NotEmpty().EmailAddress();
        RuleFor(x => x.NumeroOrden).NotEmpty().Must(x => x != null && x.StartsWith("ORD-")).WithMessage("El numero de orden debe comenzar con 'ORD-'");
        RuleForEach(x => x.Productos).SetValidator(new DetalleProductoValidator());
    }
}

class Program
{
    static void Main(string[] args)
    {
        var orden = new OrdenCompra
        {
            NumeroOrden = "ORD-12345",
            CorreoCliente = "abdielitopro4800@gmail.com",
            Productos = new List<DetalleProducto>
            {
                new DetalleProducto { NombreProducto = "Producto 1", Precio = 16.5m}
                ,
                new DetalleProducto { NombreProducto = "Producto 2", Precio = 25.0m},
                new DetalleProducto { NombreProducto = "Producto 3", Precio = 10.0m}
            }
        };
        var validator = new OrdenCompraValidator();
        var result = validator.Validate(orden);
        if (result.IsValid)
        {
            Console.WriteLine("La orden de compra es valida.");
        }
        else
        {
            Console.WriteLine("La orden de compra no es valida. Errores:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($". {error.PropertyName}: {error.ErrorMessage}");
            }
        }
    }
}