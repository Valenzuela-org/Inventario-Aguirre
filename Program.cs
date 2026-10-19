using System;
using System.Collections.Generic;
using System.Linq;

List<Producto> productos = new List<Producto>();

//Mensaje de bienvenida
int opcion = 0;

do
{
  //Declaración de variables
  string codigoProducto= "";
  string nombreProducto= "";
  string colorProducto= "";
  string tallaProducto= "";
  string cantidadPedido= "";
  string cantidadProducida= "";                  
  string cantidadDefectuosa= "";

Console.Clear();
Console.WriteLine("==========================================");
Console.WriteLine("     SISTEMA DE INVENTARIO - AGUIRRE     ");
Console.WriteLine($"Fecha y hora actual: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
Console.WriteLine("==========================================");

  //Menu de opciones
  Console.WriteLine("Seleccione una opción:");
  Console.WriteLine("1. Ingresar nuevo producto");
  Console.WriteLine("2. Actualizar producto existente");
  Console.WriteLine("3. Eliminar producto");
  Console.WriteLine("4. Buscar producto existente");
  Console.WriteLine("5. Salir");


  Console.Write("Seleccione una opción: ");

  if(int.TryParse(Console.ReadLine(),out opcion))
  {
    switch(opcion)
    {
        case 1:
            //Solicitar datos al usuario
            Console.WriteLine("Ingrese el código del producto: ");
            codigoProducto = Console.ReadLine() ?? "";

            //Validar que no exista un codigo duplicado
            if (productos.Exists(p=> p.Codigo.Equals(codigoProducto, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Error: El código del producto ya existe.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                break;
            }
            
            nombreProducto= Producto.ValidarProducto("Ingrese el nombre del producto: ");
            colorProducto= Producto.ValidarColor("Ingrese el color del producto: ");
            tallaProducto= Producto.ValidarTalla("Ingrese la talla del producto: ");

            //Pedir datos del encargadoo
            Console.WriteLine("Ingrese el nombre del encargado del producto: ");
            string encargadoProducto = Console.ReadLine() ?? "";     

            //para que la opción de búsqueda y actualización funcionen
            productos.Add(new Producto

            {
                Codigo = codigoProducto,
                Nombre = nombreProducto,
                Color = colorProducto,
                Talla = tallaProducto,
                Encargado = encargadoProducto,
                FechaRegistro = DateTime.Now,
                CantidadPedido = cantidadPedido,
                CantidadProducida = cantidadProducida,
                CantidadDefectuosa = cantidadDefectuosa
            
            });

            Console.WriteLine("Producto registrado correctamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            break;

        case 2:
            Console.Write("Ingrese el código del producto a actualizar: ");
            string codigoActualizar = Console.ReadLine() ?? "";

    Producto? productoActualizar = productos.Find(
        p => p.Codigo.Equals(codigoActualizar, StringComparison.OrdinalIgnoreCase)
    );

    if (productoActualizar != null)
    {
        Console.Write("Nuevo código: ");
        productoActualizar.Codigo = Console.ReadLine() ?? "";
        Console.Write("Nuevo nombre: ");
        productoActualizar.Nombre = Console.ReadLine() ?? "";

        Console.Write("Nuevo color: ");
        productoActualizar.Color = Console.ReadLine() ?? "";

        Console.Write("Nueva talla: ");
        productoActualizar.Talla = Console.ReadLine() ?? "";
        Console.WriteLine("Producto actualizado correctamente.");
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
        break;
    }
    else
    {
        Console.WriteLine("Producto no encontrado.");
    }
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
        break;
        case 3:
            Console.Write("Ingrese el código del producto a eliminar: ");
            string codigoEliminar = Console.ReadLine() ?? "";

            Producto? productoEliminar = productos.Find(
                p => p.Codigo.Equals(codigoEliminar, StringComparison.OrdinalIgnoreCase)
            );

            if (productoEliminar != null)
            {
                productos.Remove(productoEliminar);
                Console.WriteLine("Producto eliminado correctamente.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            break;
        case 4:            
            Console.Write("Ingrese el código del producto a buscar: ");
            string codigoBuscar = Console.ReadLine() ?? "";

            Producto? encontrado = productos.Find(
               p => p.Codigo.Equals(codigoBuscar,
                StringComparison.OrdinalIgnoreCase)
    );

    if (encontrado != null)
    {
        Console.WriteLine("\nProducto encontrado:");
        Console.WriteLine("Código: " + encontrado.Codigo);
        Console.WriteLine("Nombre: " + encontrado.Nombre);
        Console.WriteLine("Color: " + encontrado.Color);
        Console.WriteLine("Talla: " + encontrado.Talla);
        Console.WriteLine("Cantidad pedido: " + encontrado.CantidadPedido);

        //Encargado y fecha de registro
        Console.WriteLine("Encargado: " + encontrado.Encargado);
        Console.WriteLine("Fecha de registro: " + encontrado.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss"));

        Console.WriteLine("Cantidad producida: " + encontrado.CantidadProducida);
        Console.WriteLine("Cantidad defectuosa: " + encontrado.CantidadDefectuosa);
    }
    else

    {
        Console.WriteLine("Producto no encontrado.");
    }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            break;
        case 5:       
            Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
            break;

        default:
            Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida seleccione del 1 al 5.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            break;
    }
  }
  else
  {
    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida del 1 al 5.");
    Console.WriteLine("Presione cualquier tecla para continuar...");
    Console.ReadKey();

    opcion = 0; 
  }

} while (opcion != 5);

//clase producto
class Producto
{
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Color { get; set; } = "";
    public string Talla { get; set; } = "";

    //Encargado
    public string Encargado { get; set; } = "";

    //Fecha de producto
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public string CantidadPedido { get; set; } = "";
    public string CantidadProducida { get; set; } = "";
    public string CantidadDefectuosa { get; set; } = "";

    public static string ValidarProducto(string mensaje)
    {
        string entrada;
        string[] productosValidos = { "CAMISA","POLO" };
        while (true)
        {
            Console.Write(mensaje);
            entrada = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(entrada) && productosValidos.Contains(entrada.ToUpper()))
            {
                return entrada;
            }
            Console.WriteLine("Entrada inválida. Por favor, ingrese un valor válido.");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }

    public static string ValidarColor(string mensaje)
    {
        string entrada;
        string[] coloresValidos = { "ROJO", "AZUL", "VERDE", "AMARILLO", "NEGRO", "BLANCO" };
        while (true)
        {
            Console.Write(mensaje);
            entrada =( Console.ReadLine() ?? "").Trim().ToUpper();
            if (!string.IsNullOrWhiteSpace(entrada) && coloresValidos.Contains(entrada))  
            {
                return entrada;
            }
            Console.WriteLine("Entrada inválida. Por favor, ingrese un color disponible (rojo, azul, verde, amarillo, negro, blanco).");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
        }
    
    public static string ValidarTalla(string mensaje)
    {
        string entrada;
        string[] tallasValidas = { "XS", "S", "M", "L", "XL", "XXL" };
        while (true)
        {
            Console.Write(mensaje);
            entrada = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(entrada) && tallasValidas.Contains(entrada.ToUpper()))
            {
                return entrada;
            }
            Console.WriteLine("Entrada inválida. Por favor, ingrese un valor válido (XS, S, M, L, XL, XXL).");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }
    

    public static string ValidarTexto(string mensaje)
    {
        string entrada;
        while (true)
        {
            Console.Write(mensaje);
            entrada = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(entrada)&& entrada.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                return entrada;
            }
            Console.WriteLine("Entrada inválida. Por favor, ingrese un valor válido.");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }
}