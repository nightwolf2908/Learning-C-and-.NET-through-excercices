using System;
using System.Collections.Generic;
public abstract class Empleado
{
    protected string Nombre {get; set;}
    private decimal _salarioBase;
    public string NombrePublico => Nombre;
    public Empleado(string nombre, decimal salarioBase)
    {
        Nombre = nombre;
        _salarioBase = salarioBase;
    }
    public decimal salarioBase
    {
        get {return _salarioBase;}
        set {if (value >=0) _salarioBase = value; else{_salarioBase = 0;}}
    }
    public abstract decimal CalcularPago();
}

public class EmpleadoTiempoCompleto : Empleado
{
    private decimal Bono { get; set; }
    public EmpleadoTiempoCompleto(string nombre, decimal salarioBase, decimal bono) : base(nombre, salarioBase)
    {
        Bono = bono;
    }
    public override decimal CalcularPago()
    {
        return salarioBase + Bono;
    }
}

public class EmpleadoPorHoras : Empleado
{
    private int HorasTrabajadas { get; set; }
    private decimal TarifaPorHora { get; set; }

    public EmpleadoPorHoras(string nombre, int horasTrabajadas, decimal tarifaPorHora) : base(nombre, 0)
    {
        HorasTrabajadas = horasTrabajadas;
        TarifaPorHora = tarifaPorHora;
    }

    public override decimal CalcularPago()
    {
        return HorasTrabajadas * TarifaPorHora;
    }
}
 
 class Program
{
    static void Main(string[] args)
    {
        var empleados = new List<Empleado>
        {
            new EmpleadoTiempoCompleto("Juan", 3000, 500),
            new EmpleadoPorHoras("Maria", 40, 15),
            new EmpleadoTiempoCompleto("Pedro", 4000, 800),
        };
        foreach (var empleado in empleados)
        {
            Console.WriteLine($"Empleado: {empleado.NombrePublico} - Pago: {empleado.CalcularPago()}");
        }
    }
}
 
