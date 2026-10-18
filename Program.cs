using System;
using System.Collections.Generic;
using System.Linq;

List<Producto> productos = new List<Producto>();

//lista para guardar las ordenes de producción
List<OrdenProduccion> ordenesProduccion = new List<OrdenProduccion>();

//Mensaje de bienvenida
int opcion = 0;
do
{
  Console.WriteLine("Bienvenido al sistema de inventario de Aguirre");

  //Declaración de variables
  string codigoProducto= "";
  string nombreProducto= "";
  string colorProducto= "";
  string tallaProducto= "";



  //Menu de opciones
  Console.WriteLine("Seleccione una opción:");
  Console.WriteLine("1. Ingresar nuevo producto");
  Console.WriteLine("2. Actualizar producto existente");
  Console.WriteLine("3. Eliminar producto");
  Console.WriteLine("4. Buscar producto existente");
  Console.WriteLine("5. Registrar orden de producción"); //agrergado
  Console.WriteLine("6. Salir");


  Console.Write("Seleccione una opción: ");
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
            
            //para que la opción de búsqueda y actualización funcionen
            productos.Add(new Producto

            {
                Codigo = codigoProducto,
                Nombre = nombreProducto,
                Color = colorProducto,
                Talla = tallaProducto
            
            }
            );

  



   Console.WriteLine("Producto registrado correctamente.");

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
            Console.WriteLine("Ingrese el código del producto a buscar: " + codigoBuscar);

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
        // bloque para registrar la orden de producción    
            Console.WriteLine("Opción 5 seleccionada: Registrar Orden de Producción");
            
            Console.Write("Ingrese el número de la orden de producción: ");
            string numeroOrden = Console.ReadLine() ?? "";

            Console.Write("ingrese el codigo del Producto: ");
            string codProdOrden = Console.ReadLine() ?? "";

            Console.Write("Ingrese la cantidad programada: ");
            int cantidadProgramada = 0;
            int.TryParse(Console.ReadLine(), out cantidadProgramada);

            ordenesProduccion.Add(nuevaOrden);

            //Verificacion de que la orden se registro


            Console.WriteLine("\nOrden de producción registrada correctamente:"); 
            Console.WriteLine("---- Datos de la orden Guardada ----");
            Console.WriteLine(N° de orden: " + nuevaOrden.NumeroOrden);
            Console.WriteLine("Código del producto: " + nuevaOrden.CodigoProducto);
            Console.WriteLine("Cantidad programada: " + nuevaOrden.CantidadProgramada);
            break;


        default:
            Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 5.");
            break;
    }

  }

  else
  {
    Console.WriteLine("Entrada no válida. Por favor, ingrese un número del 1 al 5.");
  }

} while (opcion != 5);








class Producto
{
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Color { get; set; } = "";
    public string Talla { get; set; } = "";
}
