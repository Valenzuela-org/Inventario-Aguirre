using System;
using System.Collections.Generic;
using System.Linq;

List<Producto> productos = new List<Producto>();
List<OrdenProduccion> ordenesProduccion = new List<OrdenProduccion>();

int siguienteNumeroOrden = 1;

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
  Console.WriteLine("5. Gestión de producción");
  Console.WriteLine("6. Salir");


  Console.Write("Seleccione una opción: ");

  if(int.TryParse(Console.ReadLine(),out opcion))
  {
    switch(opcion)
    {
        case 1:
            //Solicitar datos al usuario
            codigoProducto = Producto.ValidarCodigo("Ingrese el código del producto: ");

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
           Console.Clear();
           Console.WriteLine("==========================================");
           Console.WriteLine("       GESTIÓN DE PRODUCCIÓN");
           Console.WriteLine("==========================================");
           Console.WriteLine("1. Registrar orden de producción");
           Console.WriteLine("2. Actualizar avance de producción");
           Console.WriteLine("3. Consultar reportes de producción");
           Console.WriteLine("4. Volver al menú principal");
           Console.Write("Seleccione una opción: ");

    string opcionProduccion = Console.ReadLine() ?? "";

    switch (opcionProduccion)
    {
        case "1":
     Console.Clear();
     Console.WriteLine("==========================================");
     Console.WriteLine("     REGISTRAR ORDEN DE PRODUCCIÓN");
     Console.WriteLine("==========================================");

     string codigoOrden = Producto.ValidarCodigo(
        "Ingrese el código del producto: ");

     Producto? productoOrden = productos.Find(
        p => p.Codigo.Equals(
            codigoOrden,
            StringComparison.OrdinalIgnoreCase)
     );

     if (productoOrden == null)
     {
        Console.WriteLine("Error: el producto no existe en el inventario.");
        break;
     }

     Console.WriteLine($"Producto: {productoOrden.Nombre}");
     Console.WriteLine($"Color registrado: {productoOrden.Color}");
     Console.WriteLine($"Talla registrada: {productoOrden.Talla}");

     Console.Write("Ingrese la cantidad programada: ");

     if (!int.TryParse(Console.ReadLine(), out int cantidadProgramada)
        || cantidadProgramada <= 0)
     {
        Console.WriteLine(
            "Error: la cantidad debe ser un número entero mayor que cero.");
        break;
     }

     OrdenProduccion nuevaOrden = new OrdenProduccion
     {
        NumeroOrden = siguienteNumeroOrden,
        CodigoProducto = productoOrden.Codigo,
        NombreProducto = productoOrden.Nombre,
        Color = productoOrden.Color,
        Talla = productoOrden.Talla,
        CantidadProgramada = cantidadProgramada,
        CantidadProducida = 0,
        CantidadDefectuosa = 0,
        FechaRegistro = DateTime.Now
     };

     ordenesProduccion.Add(nuevaOrden);
     siguienteNumeroOrden++;

     Console.WriteLine("\n¡Orden registrada correctamente!");
     Console.WriteLine($"Número de orden: {nuevaOrden.NumeroOrden}");
     Console.WriteLine($"Producto: {nuevaOrden.NombreProducto}");
     Console.WriteLine($"Cantidad programada: {nuevaOrden.CantidadProgramada}");

     Console.WriteLine("\nPresione cualquier tecla para continuar...");
     Console.ReadKey();
     break;

        
     case "2":
     Console.Clear();
     Console.WriteLine("==========================================");
     Console.WriteLine("    ACTUALIZAR AVANCE DE PRODUCCIÓN");
     Console.WriteLine("==========================================");

     if (ordenesProduccion.Count == 0)
     {
        Console.WriteLine("No hay órdenes de producción registradas.");
        break;
     }

     Console.WriteLine("\nÓrdenes registradas:");

     foreach (OrdenProduccion orden in ordenesProduccion)
     {
        Console.WriteLine(
            $"Orden N.° {orden.NumeroOrden} | " +
            $"{orden.NombreProducto} | {orden.Color} | " +
            $"{orden.Talla} | Programado: {orden.CantidadProgramada}"
        );
     }

     Console.Write("\nIngrese el número de orden: ");

     if (!int.TryParse(Console.ReadLine(), out int numeroOrden))
     {
        Console.WriteLine("Error: ingrese un número de orden válido.");
        break;
     }

     OrdenProduccion? ordenActualizar = ordenesProduccion.Find(
        o => o.NumeroOrden == numeroOrden
     );

     if (ordenActualizar == null)
     {
        Console.WriteLine("Error: no se encontró esa orden.");
        break;
     }

     Console.WriteLine($"\nProducto: {ordenActualizar.NombreProducto}");
     Console.WriteLine($"Cantidad programada: {ordenActualizar.CantidadProgramada}");
     Console.WriteLine($"Producidas anteriormente: {ordenActualizar.CantidadProducida}");
     Console.WriteLine($"Defectuosas anteriormente: {ordenActualizar.CantidadDefectuosa}");

     Console.Write("\nIngrese el total acumulado de unidades producidas: ");

     if (!int.TryParse(Console.ReadLine(), out int producidas)
        || producidas < 0
        || producidas > ordenActualizar.CantidadProgramada)
     {
        Console.WriteLine(
            "Error: la cantidad debe estar entre 0 y la cantidad programada."
        );
        break;
     }

     Console.Write("Ingrese el total acumulado de unidades defectuosas: ");

     if (!int.TryParse(Console.ReadLine(), out int defectuosas)
        || defectuosas < 0
        || defectuosas > producidas)
     {
        Console.WriteLine(
            "Error: las unidades defectuosas deben estar entre 0 y las producidas."
        );
        break;
     }

     ordenActualizar.CantidadProducida = producidas;
     ordenActualizar.CantidadDefectuosa = defectuosas;

     int aceptadas = producidas - defectuosas;
     int pendientes = ordenActualizar.CantidadProgramada - producidas;
     double cumplimiento =
        (double)producidas / ordenActualizar.CantidadProgramada * 100;

     Console.WriteLine("\n¡Avance de producción actualizado!");
     Console.WriteLine($"Unidades producidas: {producidas}");
     Console.WriteLine($"Unidades defectuosas: {defectuosas}");
     Console.WriteLine($"Unidades aceptadas: {aceptadas}");
     Console.WriteLine($"Unidades pendientes: {pendientes}");
     Console.WriteLine($"Cumplimiento: {cumplimiento:F2}%");

     break;


        
     case "3":
     Console.Clear();
     Console.WriteLine("==================================================");
     Console.WriteLine("          REPORTE DE PRODUCCIÓN");
     Console.WriteLine("==================================================");

     if (ordenesProduccion.Count == 0)
     {
        Console.WriteLine("No hay órdenes de producción registradas.");
        break;
     }

     int totalProgramado = 0;
     int totalProducido = 0;
     int totalDefectuoso = 0;

     foreach (OrdenProduccion ordenReporte in ordenesProduccion)
     {
        int cantidadSinDefectos =
            ordenReporte.CantidadProducida -
            ordenReporte.CantidadDefectuosa;

        Console.WriteLine("\n------------------------------------------");
        Console.WriteLine($"Orden N.º: {ordenReporte.NumeroOrden}");
        Console.WriteLine($"Código: {ordenReporte.CodigoProducto}");
        Console.WriteLine($"Producto: {ordenReporte.NombreProducto}");
        Console.WriteLine($"Color: {ordenReporte.Color}");
        Console.WriteLine($"Talla: {ordenReporte.Talla}");
        Console.WriteLine($"Fecha de registro: {ordenReporte.FechaRegistro:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine($"Cantidad programada: {ordenReporte.CantidadProgramada}");
        Console.WriteLine($"Cantidad producida: {ordenReporte.CantidadProducida}");
        Console.WriteLine($"Cantidad defectuosa: {ordenReporte.CantidadDefectuosa}");
        Console.WriteLine($"Cantidad sin defectos: {cantidadSinDefectos}");

        totalProgramado += ordenReporte.CantidadProgramada;
        totalProducido += ordenReporte.CantidadProducida;
        totalDefectuoso += ordenReporte.CantidadDefectuosa;
     }

     Console.WriteLine("\n==================================================");
     Console.WriteLine("              RESUMEN GENERAL");
     Console.WriteLine("==================================================");
     Console.WriteLine($"Total programado: {totalProgramado}");
     Console.WriteLine($"Total producido: {totalProducido}");
     Console.WriteLine($"Total defectuoso: {totalDefectuoso}");
     Console.WriteLine($"Total sin defectos: {totalProducido - totalDefectuoso}");
     Console.WriteLine($"Pendiente de producir: {totalProgramado - totalProducido}");

     break;


         case "4":
            // Volver al menú principal
            break;

        default:
            Console.WriteLine("Opción inválida.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            break;
    }

    if (opcionProduccion != "4")
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    break;

case 6:
    Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
    break;



        default:
            Console.WriteLine("Opción inválida. Seleccione una opción del 1 al 6.");
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

} while (opcion != 6);

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

    public static string ValidarCodigo(string mensaje)
    {
        string entrada;
        string patron = @"^A00-\d{1,5}$"; 
        while (true)
        {
            Console.Write(mensaje);
            entrada = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(entrada) && System.Text.RegularExpressions.Regex.IsMatch(entrada, patron))
            {
                return entrada;
            }
            Console.WriteLine("Entrada inválida. Por favor, ingrese un valor válido.");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }

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
class OrdenProduccion
{
    public int NumeroOrden { get; set; }
    public string CodigoProducto { get; set; } = "";
    public string NombreProducto { get; set; } = "";
    public string Color { get; set; } = "";
    public string Talla { get; set; } = "";
    public int CantidadProgramada { get; set; }
    public int CantidadProducida { get; set; }
    public int CantidadDefectuosa { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}