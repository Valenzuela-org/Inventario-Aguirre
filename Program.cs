//Mensaje de bienvenida
Console.WriteLine("Bienvenido al sistema de inventario de Aguirre");

//Declaración de variables
string codigoProducto= "";
string nombreProducto= "";
string colorProducto= "";
string tallaProducto= "";

int cantidadProgramada= 0;
int cantidadProducida= 0;
int cantidadDefectuosa= 0;

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