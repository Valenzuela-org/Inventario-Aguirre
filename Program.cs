using System;
using System.Collections.Generic;
using System.Linq;

List<Producto> productos = new List<Producto>();
//Mensaje de bienvenida
Console.WriteLine("Bienvenido al sistema de inventario de Aguirre");

//Declaración de variables
string codigoProducto= "";
string nombreProducto= "";
string colorProducto= "";
string tallaProducto= "";

int opcion= 0;
int cantidadProgramada= 0;
int cantidadProducida= 0;
int cantidadDefectuosa= 0;

//Menu de opciones
Console.WriteLine("Seleccione una opción:");
Console.WriteLine("1. Ingresar nuevo producto");
Console.WriteLine("2. Actualizar producto existente");
Console.WriteLine("3. Eliminar producto");
Console.WriteLine("4. Buscar producto existente");
Console.WriteLine("5. Salir");
if(int.TryParse(Console.ReadLine(),out opcion))
{
    switch(opcion)
    {
        case 1:
            Console.WriteLine("Opción 1 seleccionada: Ingresar nuevo producto");
            //Solicitar datos al usuario
            Console.WriteLine("Ingrese el código del producto: ");
            codigoProducto = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese el nombre del producto: ");
            nombreProducto = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese el color del producto: ");
            colorProducto = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese la talla del producto: ");
            tallaProducto = Console.ReadLine() ?? "";

            //TEMPORAL imprimir los datos ingresados
            Console.WriteLine("Datos del producto ingresados:");
            Console.WriteLine("Código: " + codigoProducto);
            Console.WriteLine("Nombre: " + nombreProducto);
            Console.WriteLine("Color: " + colorProducto);
            Console.WriteLine("Talla: " + tallaProducto);
            break;
        case 2:
            Console.WriteLine("Opción 2 seleccionada: Actualizar producto existente");
            string codigoActualizar = Console.ReadLine() ?? "";

    Producto? productoActualizar = productos.Find(
        p => p.Codigo == codigoActualizar
    );

    if (productoActualizar != null)
    {
        Console.Write("Nuevo nombre: ");
        productoActualizar.Nombre = Console.ReadLine() ?? "";

        Console.Write("Nuevo color: ");
        productoActualizar.Color = Console.ReadLine() ?? "";

        Console.Write("Nueva talla: ");
        productoActualizar.Talla = Console.ReadLine() ?? "";

        Console.WriteLine("Producto actualizado correctamente.");
    }
    else
    {
        Console.WriteLine("Producto no encontrado.");
    }
            break;
        case 3:
            Console.WriteLine("Opción 3 seleccionada: Eliminar producto");
            break;
        case 4:
            Console.WriteLine("Opción 4 seleccionada: Buscar producto existente");
            string codigoBuscar = Console.ReadLine() ?? "";

    Producto? encontrado = productos.Find(
        p => p.Codigo == codigoBuscar
    );

    if (encontrado != null)
    {
        Console.WriteLine("\nProducto encontrado:");
        Console.WriteLine("Código: " + encontrado.Codigo);
        Console.WriteLine("Nombre: " + encontrado.Nombre);
        Console.WriteLine("Color: " + encontrado.Color);
        Console.WriteLine("Talla: " + encontrado.Talla);
    }
    else
    {
        Console.WriteLine("Producto no encontrado.");
    }

            break;
        case 5:
            Console.WriteLine("Opción 5 seleccionada: Salir");
            break;
        default:
            Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 5.");
            break;
    }

}

//Solicitar opción al usuario
Console.WriteLine("Ingrese su opción: "); 


//Solicitar cantidad programada
while(true)
{
    Console.WriteLine("Ingrese la cantidad programada en unidades: ");
    if(int.TryParse(Console.ReadLine(), out cantidadProgramada) && cantidadProgramada >= 0)
    {
        break;
    }
    else
    {
        Console.WriteLine("Por favor, ingrese un número válido para la cantidad programada.");
    }
}

//Unidades producidas
while(true)
{
    Console.WriteLine("Ingrese la cantidad producida en unidades: ");
    if(int.TryParse(Console.ReadLine(), out cantidadProducida) && cantidadProducida >= 0)
    {
        break;
    }
    else
    {
        Console.WriteLine("Por favor, ingrese un número válido para la cantidad producida.");
    }
}
class Producto
{
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Color { get; set; } = "";
    public string Talla { get; set; } = "";
}