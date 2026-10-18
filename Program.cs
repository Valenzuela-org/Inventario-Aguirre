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
  string cantidadPedido= "";
  string cantidadProducida= "";                  
  string cantidadDefectuosa= "";



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

            //Validar que no exista un codigo duplicado
            if (productos.Exists(p=> p.Codigo.Equals(codigoProducto, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Error: El código del producto ya existe.");
                break;
            }

            Console.WriteLine("Ingrese el nombre del producto: ");
            nombreProducto = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese el color del producto: ");
            colorProducto = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese la talla del producto: ");
            tallaProducto = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese la cantidad pedida del producto: ");
            cantidadPedido = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese la cantidad producida del producto: ");
            cantidadProducida = Console.ReadLine() ?? "";
            Console.WriteLine("Ingrese la cantidad defectuosa del producto: ");
            cantidadDefectuosa = Console.ReadLine() ?? "";
            

            //para que la opción de búsqueda y actualización funcionen
            productos.Add(new Producto

            {
                Codigo = codigoProducto,
                Nombre = nombreProducto,
                Color = colorProducto,
                Talla = tallaProducto
            
            });

            Console.WriteLine("Producto registrado correctamente.");

            //TEMPORAL imprimir los datos ingresados
            Console.WriteLine("Datos del producto ingresados:");
            Console.WriteLine("Código: " + codigoProducto);
            Console.WriteLine("Nombre: " + nombreProducto);
            Console.WriteLine("Color: " + colorProducto);
            Console.WriteLine("Talla: " + tallaProducto);
            Console.WriteLine("Cantidad pedido: " + cantidadPedido);
            Console.WriteLine("Cantidad producida: " + cantidadProducida);
            Console.WriteLine("Cantidad defectuosa: " + cantidadDefectuosa);
            break;
        case 2:
            Console.WriteLine("Opción 2 seleccionada: Actualizar producto existente");
            Console.Write("Ingrese el código del producto a actualizar: ");
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

        Console.Write("Nueva cantidad pedido: ");
        productoActualizar.CantidadPedido = Console.ReadLine() ?? "";

        Console.Write("Nueva cantidad producida: ");
        productoActualizar.CantidadProducida = Console.ReadLine() ?? "";

        Console.Write("Nueva cantidad defectuosa: ");
        productoActualizar.CantidadDefectuosa = Console.ReadLine() ?? "";

        Console.WriteLine("Producto actualizado correctamente.");
    }
    else
    {
        Console.WriteLine("Producto no encontrado.");
    }
            break;
        case 3:
            Console.WriteLine("Opción 3 seleccionada: Eliminar producto");
            Console.Write("Ingrese el código del producto a eliminar: ");
            string codigoEliminar = Console.ReadLine() ?? "";

            Producto? productoEliminar = productos.Find(
                p => p.Codigo == codigoEliminar
            );

            if (productoEliminar != null)
            {
                productos.Remove(productoEliminar);
                Console.WriteLine("Producto eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }

            if (productoEliminar != null)
            {
                productos.Remove(productoEliminar);
                Console.WriteLine("Producto eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
            break;
        case 4:
            Console.WriteLine("Opción 4 seleccionada: Buscar producto existente");
            
            Console.Write("Ingrese el código del producto a buscar: ");
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
         Console.WriteLine("Cantidad pedido: " + encontrado.CantidadPedido);
        Console.WriteLine("Cantidad producida: " + encontrado.CantidadProducida);
        Console.WriteLine("Cantidad defectuosa: " + encontrado.CantidadDefectuosa);
    }
    else

    {
        Console.WriteLine("Producto no encontrado.");
    }

            break;
        case 5:    
        // bloque para registrar la orden de producción    
            Console.WriteLine("Opción 5 seleccionada: Registrar Orden de Producción");
            
            Console.Write("Ingrese el número de la orden: ");
            string numeroOrden = Console.ReadLine() ?? "";

            //validar que el numero de orden no este vacio
            if (string.IsNullOrWhiteSpace(numeroOrden))
            {
                Console.WriteLine("El número de orden no puede estar vacío. Por favor, intente nuevamente.");
                break;
            }

            //verificar que no exista una orden con el mismo numero de orden
            bool ordenExistente = ordenesProduccion.Exists(o => o.NumeroOrden == numeroOrden);
            if (ordenExistente)
            {
                Console.WriteLine("Ya existe una orden con el número ingresado. Por favor, intente nuevamente.");
                break;
            }

            Console.Write("ingrese el codigo del Producto: ");
            string codProdOrden = Console.ReadLine() ?? "";

            //COMPROBAR QUE EL PRODCUTO ESTE REGISTRADO
            Producto? productoOrden = productos.Find(p => p.Codigo == codProdOrden);
            if (productoOrden == null)
            {
                Console.WriteLine("El producto no existe, Registre el producto primero.");
                break;
            }

            Console.Write("Ingrese la cantidad programada: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidadProgramada) || cantidadProgramada <= 0)
            {
                Console.WriteLine("LA cantidad debe ser un Numero mayor a cero.");
                break;
            }

            //crear la orden de produccion
            OrdenProduccion nuevaOrden = new OrdenProduccion
            {
                NumeroOrden = numeroOrden,
                CodigoProducto = codProdOrden,
                CantidadProgramada = cantidadProgramada
            };

            //Guardar la orden en la lista

            ordenesProduccion.Add(nuevaOrden);

            //Confirmar el registro

            Console.WriteLine("Número de orden: " + nuevaOrden.NumeroOrden);
            Console.WriteLine("\n---- Datos de la orden Guardada ----");
            Console.WriteLine("N° Orden de producción registrada correctamente:"); 
            Console.WriteLine("Código del producto: " + nuevaOrden.CodigoProducto);
            Console.WriteLine("Cantidad programada: " + nuevaOrden.CantidadProgramada);
            break;

        case 6:
            Console.WriteLine("SALIENDO DEL SISTEMA. ¡Hasta luego!");
            break;

        default:
            Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 6.");
            break;
    }

  }

  else
  {
    Console.WriteLine("Entrada no válida. Por favor, ingrese un número del 1 al 6.");
  }

} while (opcion != 6);

//clase producto
class Producto
{
    public string Codigo { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Color { get; set; } = "";
    public string Talla { get; set; } = "";
}
// Clase Para la Orden de producción
class OrdenProduccion
{
    public string NumeroOrden { get; set; } = "";
    public string CodigoProducto { get; set; } = "";
    public int CantidadProgramada { get; set; } = 0;
}
