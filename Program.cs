using System;
using System.Collections.Generic;
using System.Linq;

//Colecciones globales para almacenar productos y órdenes de producción
List<Producto> productos = new List<Producto>();
List<OrdenProduccion> ordenesProduccion = new List<OrdenProduccion>();
int siguienteNumeroOrden = 1;

//Carga de datos desde archivos de texto si existen
if(System.IO.File.Exists("productos.txt"))
{
    string[] lineas = System.IO.File.ReadAllLines("productos.txt");
    foreach (string linea in lineas)
    {
        string[] partes = linea.Split('|');
        if (partes.Length >= 6)
                {
                    productos.Add(new Producto
                    {
                        Codigo = partes[0],
                        Nombre = partes[1],
                        Color = partes[2],
                        Talla = partes[3],
                        Encargado = partes[4],
                        FechaRegistro = DateTime.TryParse(partes[5], out DateTime f) ? f : DateTime.Now,
                        CantidadPedido = partes.Length > 6 ? partes[6] : "",
                        CantidadProducida = partes.Length > 7 ? partes[7] : "",
                        CantidadDefectuosa = partes.Length > 8 ? partes[8] : ""
                    });
                }
    }
}

//Cargondo las órdenes de producción desde el archivo de texto si existe
if(System.IO.File.Exists("ordenes.txt"))
{
    string[] lineasOrdenes = System.IO.File.ReadAllLines("ordenes.txt");
    foreach (string linea in lineasOrdenes)
    {
        string[] partes = linea.Split('|');
        if (partes.Length >= 8)
        {
            ordenesProduccion.Add(new OrdenProduccion
            {
                NumeroOrden = int.TryParse(partes[0], out int n) ? n : 0,
                CodigoProducto = partes[1],
                NombreProducto = partes[2],
                Color = partes[3],
                Talla = partes[4],
                Encargado = partes[5],
                CantidadProgramada = int.TryParse(partes[6], out int cp) ? cp : 0,
                CantidadProducida = int.TryParse(partes[7], out int cprod) ? cprod : 0,
                CantidadDefectuosa = int.TryParse(partes[8], out int cdef) ? cdef : 0,
                FechaRegistro = DateTime.TryParse(partes[9], out DateTime f) ? f : DateTime.Now
            });
        }
    }
}

//Bucle principal del programa
int opcion = 0;

do
{
  //Declaración de variables temporales para el registro de productos
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

  //Menu de opciones del sistema
  Console.WriteLine("Seleccione una opción:");
  Console.WriteLine("1. Ingresar nuevo producto");
  Console.WriteLine("2. Actualizar producto existente");
  Console.WriteLine("3. Eliminar producto");
  Console.WriteLine("4. Buscar producto existente");
  Console.WriteLine("5. Gestión de producción");
  Console.WriteLine("6. Reporte general de producción");
  Console.WriteLine("7. Salir");


  Console.Write("Seleccione una opción: ");

  if(int.TryParse(Console.ReadLine(),out opcion))
  {
    switch(opcion)
    {
          case 1:
            //Mostrar encabezado para el ingreso de lote masivos de productos
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("     INGRESO DE LOTE DE PRODUCTOS");
            Console.WriteLine("==========================================");

            //Validación del encargado del lote
            string encargadoLote = Producto.ValidarEncargado("Ingrese el nombre del encargado del lote: ");

            int cantidadProductos = 0;
            while (true)
            {
                Console.Write("¿Cuántos productos deseas registrar en este lote?: ");
                if (int.TryParse(Console.ReadLine(), out cantidadProductos) && cantidadProductos > 0)
                {
                    break;
                }
                Console.WriteLine("Error: Debe ingresar un número entero mayor que cero. Intente de nuevo.\n");
            }

            //Bucle para registrar cada producto en el lote
            for (int i = 0; i < cantidadProductos; i++)
            {
                Console.WriteLine($"\n--- Registro del producto {i + 1} de {cantidadProductos} ---");

                codigoProducto = "";
                bool codigoValido = false;

                //Evitar que se repita el código del producto en el inventario
                while (!codigoValido)
                {
                    codigoProducto = Producto.ValidarCodigo("Ingrese el código del producto: ");
                    
                    if (codigoProducto.Equals("SALIR", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    if (productos.Exists(p => p.Codigo.Equals(codigoProducto, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine("Error: El código del producto ya existe en el inventario. Intente con otro.");
                    }
                    else
                    {
                        codigoValido = true;
                    }
                }

                if (codigoProducto.Equals("SALIR", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                //Validación de otros campos del producto
                nombreProducto = Producto.ValidarProducto("Ingrese el nombre del producto: ");
                colorProducto = Producto.ValidarColor("Ingrese el color del producto: ");
                tallaProducto = Producto.ValidarTalla("Ingrese la talla del producto: ");

                //Inserta el producto en la lista de productos
                productos.Add(new Producto
                {
                    Codigo = codigoProducto,
                    Nombre = nombreProducto,
                    Color = colorProducto,
                    Talla = tallaProducto,
                    Encargado = encargadoLote,
                    FechaRegistro = DateTime.Now,
                    CantidadPedido = cantidadPedido,
                    CantidadProducida = cantidadProducida,
                    CantidadDefectuosa = cantidadDefectuosa
                });

                Console.WriteLine("¡Producto registrado correctamente en este lote!");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            break;
            
        case 2:
            //Mostrar encabezado para la actualización de productos existentes
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("     ACTUALIZAR PRODUCTO EXISTENTE");
            Console.WriteLine("==========================================");

            Producto? productoActualizar = null;

            // Bucle para permitir reintentar si el código no se encuentra
            while (productoActualizar == null)
            {
                Console.Write("Ingrese el código del producto a actualizar (o escriba 'SALIR' para volver): ");
                string codigoActualizar = Console.ReadLine() ?? "";

                if (codigoActualizar.Equals("SALIR", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                productoActualizar = productos.Find(
                    p => p.Codigo.Equals(codigoActualizar, StringComparison.OrdinalIgnoreCase)
                );
                if (productoActualizar == null)
                {
                    Console.WriteLine("Error: Producto no encontrado. Intente de nuevo.");
                }
            }

            // Si encontró el producto, actualiza
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

                Console.WriteLine("\n¡Producto actualizado correctamente!");
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            break;
        case 3:
            //Mostrar encabezado para la eliminación de productos existentes
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("          ELIMINAR PRODUCTO EXISTENTE");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Eliminar producto por código");
            Console.WriteLine("2. Vaciar todo el inventario");
            Console.Write("Seleccione una opción: ");
            string opcionEliminar = Console.ReadLine() ?? "";

            if (opcionEliminar == "2")
            {
                // Confirmación antes de vaciar todo el inventario
                if (productos.Count == 0)
                {
                    Console.WriteLine("\nEl inventario ya está vacío.");
                }
                else
                {
                    Console.Write("¿Está seguro de eliminar TODOS los productos del inventario? (S/N): ");
                    string confirmacion = (Console.ReadLine() ?? "").Trim().ToUpper();

                    if (confirmacion == "S")
                    {
                        productos.Clear();
                        if (System.IO.File.Exists("productos.txt"))
                        {
                            System.IO.File.Delete("productos.txt");
                        }
                        if (System.IO.File.Exists("ordenes.txt"))
                        {
                            System.IO.File.Delete("ordenes.txt");
                        }
                        Console.WriteLine("\n¡Todo el inventario ha sido borrado correctamente!");
                    }
                    else
                    {
                        Console.WriteLine("\nOperación cancelada. No se borró nada.");
                    }
                }
            }
            else
            {
                Producto? productoEliminar = null;

                while (productoEliminar == null)
                {
                    Console.Write("Ingrese el código del producto a eliminar (o escriba 'SALIR' para volver): ");
                    string codigoEliminar = Console.ReadLine() ?? "";

                    if (codigoEliminar.Equals("SALIR", StringComparison.OrdinalIgnoreCase))
                    {
                        break; 
                    }

                    productoEliminar = productos.Find(
                        p => p.Codigo.Equals(codigoEliminar, StringComparison.OrdinalIgnoreCase)
                    );

                    if (productoEliminar == null)
                    {
                        Console.WriteLine("Error: Producto no encontrado. Intente de nuevo.");
                    }
                }

                if (productoEliminar != null)
                {
                    productos.Remove(productoEliminar);
                    Console.WriteLine("\n¡Producto eliminado correctamente!");
                }
            }

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            break;
        case 4:
            //Mostrar encabezado para la consulta de productos existentes
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("          CONSULTA DE PRODUCTOS");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Buscar producto por código");
            Console.WriteLine("2. Mostrar todos los productos registrados");
            Console.Write("Seleccione una opción: ");
            string opcionBusqueda = Console.ReadLine() ?? "";

            if (opcionBusqueda == "2")
            {
                // Mostrar todos los productos registrados en el inventario
                if (productos.Count == 0)
                {
                    Console.WriteLine("\nNo hay productos registrados en el inventario.");
                }
                else
                {
                    Console.WriteLine($"\n--- LISTADO GENERAL DE INVENTARIO ({productos.Count} productos) ---");
                    foreach (Producto p in productos)
                    {
                        Console.WriteLine("------------------------------------------");
                        Console.WriteLine("Código: " + p.Codigo);
                        Console.WriteLine("Nombre: " + p.Nombre);
                        Console.WriteLine("Color: " + p.Color);
                        Console.WriteLine("Talla: " + p.Talla);
                        Console.WriteLine("Encargado: " + p.Encargado);
                        Console.WriteLine("Fecha de registro: " + p.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                    Console.WriteLine("------------------------------------------");
                }
            }
            else
            {
                // Buscar producto por código
                Producto? encontrado = null;
                while (encontrado == null)
                {
                    Console.Write("Ingrese el código del producto a buscar (o escriba 'SALIR' para volver): ");
                    string codigoBuscar = Console.ReadLine() ?? "";

                    if (codigoBuscar.Equals("SALIR", StringComparison.OrdinalIgnoreCase))
                    {
                        break; 
                    }

                    encontrado = productos.Find(
                        p => p.Codigo.Equals(codigoBuscar, StringComparison.OrdinalIgnoreCase)
                    );

                    if (encontrado == null)
                    {
                        Console.WriteLine("Error: Producto no encontrado. Intente de nuevo.");
                    }
                }

                if (encontrado != null)
                {
                    Console.WriteLine("\n¡Producto encontrado!");
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Código: " + encontrado.Codigo);
                    Console.WriteLine("Nombre: " + encontrado.Nombre);
                    Console.WriteLine("Color: " + encontrado.Color);
                    Console.WriteLine("Talla: " + encontrado.Talla);
                    Console.WriteLine("Cantidad pedido: " + encontrado.CantidadPedido);
                    Console.WriteLine("Encargado: " + encontrado.Encargado);
                    Console.WriteLine("Fecha de registro: " + encontrado.FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss"));
                    Console.WriteLine("Cantidad producida: " + encontrado.CantidadProducida);
                    Console.WriteLine("Cantidad defectuosa: " + encontrado.CantidadDefectuosa);
                    Console.WriteLine("------------------------------------------");
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            break;
        case 5:   
        //Mostrar encabezado para la gestión de producción
        bool salirProduccion = false;
        while (!salirProduccion)    
        {        
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
        //Mostrar encabezado para el registro de órdenes de producción
     Console.Clear();
     Console.WriteLine("==========================================");
     Console.WriteLine("     REGISTRAR ORDEN DE PRODUCCIÓN");
     Console.WriteLine("==========================================");


     //Validar el código antes de buscar el producto
     string codigoOrden = Producto.ValidarCodigo(
        "Ingrese el código del producto: ");

     if (string.IsNullOrWhiteSpace(codigoOrden))
     {
        Console.WriteLine("Error: debe ingresar un código de producto válido.");
        break;
     }

     Producto? productoOrden = productos.Find(
        p => p.Codigo.Equals(
            codigoOrden,
            StringComparison.OrdinalIgnoreCase )
     );

     if(productoOrden == null)
     {
        Console.WriteLine("Error: el producto no existe en el inventario.");
        break;
     }

     Console.WriteLine($"Producto: {productoOrden.Nombre}");
     Console.WriteLine($"Color registrado: {productoOrden.Color}");
     Console.WriteLine($"Talla registrada: {productoOrden.Talla}");

     Console.Write("Ingrese la cantidad programada: ");
     string entradaCantidad = Console.ReadLine() ?? "";

     // Aceptar únicamente cantidades enteras mayores que cero

     if (!int.TryParse(entradaCantidad, out int cantidadProgramada)
        || cantidadProgramada <= 0)
     {
        Console.WriteLine(
            "Error: la cantidad debe ser un número entero mayor que cero.");
        break;
     }

    //Crear la nueva orden de producción y agregarla a la lista
     OrdenProduccion nuevaOrden = new OrdenProduccion
     {
        NumeroOrden = siguienteNumeroOrden,
        CodigoProducto = productoOrden.Codigo,
        NombreProducto = productoOrden.Nombre,
        Color = productoOrden.Color,
        Talla = productoOrden.Talla,
        Encargado = productoOrden.Encargado,
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
     Console.WriteLine($"Estado: {nuevaOrden.Estado}");

     Console.WriteLine("\nPresione cualquier tecla para continuar...");
     Console.ReadKey();
     break;
            
     
        case "2":
        //Actualizar el avance de producción de una orden existente
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("    ACTUALIZAR AVANCE DE PRODUCCIÓN");
            Console.WriteLine("==========================================");

            // Verifica si existen órdenes para actualizar.
            if (ordenesProduccion.Count == 0)
            {
                Console.WriteLine("No hay órdenes de producción registradas.");
                break;
            }

            // Muestra las órdenes y su avance actual.
            Console.WriteLine("\nÓrdenes registradas:");

            foreach (OrdenProduccion orden in ordenesProduccion)
            {
                // Calcula las unidades aceptadas y las pendientes.
                int aceptadasOrden =
                    orden.CantidadProducida - orden.CantidadDefectuosa;

                int pendientesOrden =
                    orden.CantidadProgramada - orden.CantidadProducida;

                // Calcula el porcentaje de cumplimiento de la orden.
                double cumplimientoOrden =
                    (double)orden.CantidadProducida /
                    orden.CantidadProgramada * 100;

                Console.WriteLine(
                    $"Orden N.° {orden.NumeroOrden} | " +
                    $"{orden.NombreProducto} | {orden.Color} | " +
                    $"{orden.Talla} | Programado: {orden.CantidadProgramada} | " +
                    $"Producido: {orden.CantidadProducida} | " +
                    $"Aceptado: {aceptadasOrden} | " +
                    $"Pendiente: {pendientesOrden} | " +
                    $"Cumplimiento: {cumplimientoOrden:F2}%"
                );
            }

            // Solicita el número de la orden que se actualizará.
            Console.Write("\nIngrese el número de orden: ");

            if (!int.TryParse(Console.ReadLine(), out int numeroOrden))
            {
                Console.WriteLine("Error: ingrese un número de orden válido.");
                break;
            }

            // Busca la orden seleccionada en la lista.
            OrdenProduccion? ordenActualizar = ordenesProduccion.Find(
                o => o.NumeroOrden == numeroOrden
            );

            // Impide actualizar una orden que no existe.
            if (ordenActualizar == null)
            {
                Console.WriteLine("Error: no se encontró esa orden.");
                break;
            }

            // Muestra el estado actual antes de modificarlo.
            Console.WriteLine("\n--- AVANCE ACTUAL ---");
            Console.WriteLine($"Producto: {ordenActualizar.NombreProducto}");
            Console.WriteLine($"Cantidad programada: {ordenActualizar.CantidadProgramada}");
            Console.WriteLine($"Unidades producidas: {ordenActualizar.CantidadProducida}");
            Console.WriteLine($"Unidades defectuosas: {ordenActualizar.CantidadDefectuosa}");
            Console.WriteLine(
                $"Unidades aceptadas: " +
                $"{ordenActualizar.CantidadProducida - ordenActualizar.CantidadDefectuosa}"
            );

            // Solicita el total acumulado de unidades producidas.
            Console.Write("\nIngrese el total acumulado de unidades producidas: ");

            // Comprueba que el nuevo total no sea inferior al anterior
            // ni supere la cantidad programada.
            if (!int.TryParse(Console.ReadLine(), out int producidas)
                || producidas < ordenActualizar.CantidadProducida
                || producidas > ordenActualizar.CantidadProgramada)
            {
                Console.WriteLine(
                    "Error: el total producido no puede ser menor que el avance actual " +
                    "ni superar la cantidad programada."
                );
                break;
            }

            // Solicita el total acumulado de unidades defectuosas.
            Console.Write("Ingrese el total acumulado de unidades defectuosas: ");

            // Evita reducir los defectos registrados o superar las unidades producidas.
            if (!int.TryParse(Console.ReadLine(), out int defectuosas)
                || defectuosas < ordenActualizar.CantidadDefectuosa
                || defectuosas > producidas)
            {
                Console.WriteLine(
                    "Error: las unidades defectuosas no pueden ser menores que " +
                    "las registradas anteriormente ni superar las producidas."
                );
                break;
            }

            // Guarda los valores anteriores para informar el cambio realizado.
            int produccionAnterior = ordenActualizar.CantidadProducida;
            int defectuosasAnteriores = ordenActualizar.CantidadDefectuosa;

            // Actualiza los acumulados de producción de la orden seleccionada.
            ordenActualizar.CantidadProducida = producidas;
            ordenActualizar.CantidadDefectuosa = defectuosas;

            // Calcula los resultados después de la actualización.
            int aceptadas = producidas - defectuosas;
            int pendientes =
                ordenActualizar.CantidadProgramada - producidas;

            double cumplimiento =
                (double)producidas /
                ordenActualizar.CantidadProgramada * 100;

            // Muestra el cambio y el estado final de la producción.
            Console.WriteLine("\n¡Avance de producción actualizado!");
            Console.WriteLine(
                $"Unidades producidas: {produccionAnterior} -> {producidas}"
            );
            Console.WriteLine(
                $"Unidades defectuosas: {defectuosasAnteriores} -> {defectuosas}"
            );
            Console.WriteLine($"Unidades aceptadas: {aceptadas}");
            Console.WriteLine($"Unidades pendientes: {pendientes}");
            Console.WriteLine($"Cumplimiento: {cumplimiento:F2}%");

            break;


        
     case "3":
     {
       Console.Clear();
       Console.WriteLine("==================================================");
       Console.WriteLine("          REPORTE DE PRODUCCIÓN");
       Console.WriteLine("==================================================");

       //Verificar si existen órdenes registradas
       if (ordenesProduccion.Count == 0)
       {
        Console.WriteLine("No hay órdenes de producción registradas.");
        break;
       }

       Console.WriteLine("\nSeleccione el tipo de consulta:");
       Console.WriteLine("1. Buscar por código de producto");
       Console.WriteLine("2. Buscar por color");
       Console.WriteLine("3. Buscar por talla");
       Console.WriteLine("4. Buscar por encargado");
       Console.WriteLine("5. Mostrar todas las órdenes");
       Console.Write("Seleccione una opción: ");

       string opcionFiltro = Console.ReadLine() ?? "";


        //Guardar las órdenes que coincidan con el filtro elegido
        List<OrdenProduccion> ordenesFiltradas =
         new List<OrdenProduccion>();

        //Filtrar las órdenes según la opción seleccionada
       if (opcionFiltro == "5")
       {
        ordenesFiltradas.AddRange(ordenesProduccion);
       }
       else if (opcionFiltro == "1")
       {
        Console.Write("Ingrese el código del producto: ");
        string codigoBuscado = Console.ReadLine() ?? "";

         foreach (OrdenProduccion orden in ordenesProduccion)
        {
            if (string.Equals(orden.CodigoProducto, codigoBuscado,
                StringComparison.OrdinalIgnoreCase))
            {
                ordenesFiltradas.Add(orden);
            }
        }
       }
       else if (opcionFiltro == "2")

        
       {
        Console.Write("Ingrese el color: ");
        string colorBuscado = Console.ReadLine() ?? "";

        foreach (OrdenProduccion orden in ordenesProduccion)
        {
            if (string.Equals(orden.Color, colorBuscado,
                StringComparison.OrdinalIgnoreCase))
            {
                ordenesFiltradas.Add(orden);
            }
        }
       }
       else if (opcionFiltro == "3")
       {
        Console.Write("Ingrese la talla: ");
        string tallaBuscada = Console.ReadLine() ?? "";

        foreach (OrdenProduccion orden in ordenesProduccion)
        {
            if (string.Equals(orden.Talla, tallaBuscada,
                StringComparison.OrdinalIgnoreCase))
            {
                ordenesFiltradas.Add(orden);
            }
        }
       }
       else if(opcionFiltro=="4")
       {
        Console.WriteLine("Ingrese el nombre del encargado: ");
        string encargadoBuscado = Console.ReadLine() ?? "";

        foreach (OrdenProduccion orden in ordenesProduccion)
        {
            if (string.Equals(orden.Encargado, encargadoBuscado,
                StringComparison.OrdinalIgnoreCase))
            {
                ordenesFiltradas.Add(orden);
            }
        }
       }
       else

       {
        Console.WriteLine("Opción inválida. Seleccione del 1 al 5.");
        break;
       }

       // Avisar cuando el filtro no encuentra ninguna orden.
       if (ordenesFiltradas.Count == 0)
       {
        Console.WriteLine("\nNo se encontraron órdenes con ese criterio.");
        break;
       }

        //Calcular totales de producción y defectuosos para el resumen del reporte
       int totalProgramado = 0;
       int totalProducido = 0;
       int totalDefectuoso = 0;

       foreach (OrdenProduccion ordenReporte in ordenesFiltradas)
       {
        int cantidadSinDefectos =
            ordenReporte.CantidadProducida -
            ordenReporte.CantidadDefectuosa;

        Console.WriteLine("\n------------------------------------------");
        Console.WriteLine($"Orden N.º: {ordenReporte.NumeroOrden}");
        Console.WriteLine($"Código: {ordenReporte.CodigoProducto}");
        Console.WriteLine($"Producto: {ordenReporte.NombreProducto}");
        Console.WriteLine($"Estado: {ordenReporte.Estado}");
        Console.WriteLine($"Color: {ordenReporte.Color}");
        Console.WriteLine($"Talla: {ordenReporte.Talla}");
        Console.WriteLine($"Encargado: {ordenReporte.Encargado}");
        Console.WriteLine($"Fecha de registro: {ordenReporte.FechaRegistro:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine($"Cantidad programada: {ordenReporte.CantidadProgramada}");
        Console.WriteLine($"Cantidad producida: {ordenReporte.CantidadProducida}");
        Console.WriteLine($"Cantidad defectuosa: {ordenReporte.CantidadDefectuosa}");
        Console.WriteLine($"Cantidad sin defectos: {cantidadSinDefectos}");

        //Acumular las cantidades de las órdenes encontradas.
        totalProgramado += ordenReporte.CantidadProgramada;
        totalProducido += ordenReporte.CantidadProducida;
        totalDefectuoso += ordenReporte.CantidadDefectuosa;
      }    

        //Presentar un resumen del reporte con totales y porcentajes
       Console.WriteLine("\n==================================================");
       Console.WriteLine("              RESUMEN DEL REPORTE");
       Console.WriteLine("==================================================");
       Console.WriteLine($"Órdenes encontradas: {ordenesFiltradas.Count}");
       Console.WriteLine($"Total programado: {totalProgramado}");
       Console.WriteLine($"Total producido: {totalProducido}");
       Console.WriteLine($"Total defectuoso: {totalDefectuoso}");
       Console.WriteLine($"Total sin defectos: {totalProducido - totalDefectuoso}");
       Console.WriteLine($"Pendiente de producir: {totalProgramado - totalProducido}");

       //Calcular los porcentajes sin dividir entre cero
       double porcentajeAvance = totalProgramado > 0
           ? (double)totalProducido / totalProgramado * 100
           : 0;

       double porcentajeDefectuoso = totalProducido > 0
           ? (double)totalDefectuoso / totalProducido * 100
           : 0;

        Console.WriteLine($"Porcentaje de avance: {porcentajeAvance:F2}%");
        Console.WriteLine($"Porcentaje de unidades defectuosas: {porcentajeDefectuoso:F2}%");
       break;
     }  

         case "4":
         salirProduccion = true;
            // Volver al menú principal
            break;

        default:
            Console.WriteLine("Opción inválida. Seleccione una opción del 1 al 4.");
            Console.ReadKey();
            break;
    }

    if (opcionProduccion != "4")
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
        }
    break;

case 6:
    //Consolida el reporte general de producción de todas las órdenes registradas
    Console.Clear();
    Console.WriteLine("==========================================");
    Console.WriteLine("       REPORTE GENERAL DE PRODUCCIÓN");
    Console.WriteLine("==========================================");

    if (ordenesProduccion.Count == 0)
    {
        Console.WriteLine("No hay órdenes de producción registradas.");
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadKey();
        break;
    }

    int totalProgramadoGeneral = 0;
    int totalProducidoGeneral = 0;
    int totalDefectuosoGeneral = 0;

    foreach (OrdenProduccion orden in ordenesProduccion)
    {
        totalProgramadoGeneral += orden.CantidadProgramada;
        totalProducidoGeneral += orden.CantidadProducida;
        totalDefectuosoGeneral += orden.CantidadDefectuosa;
    }

    double porcentajeAvanceGeneral = totalProgramadoGeneral > 0
        ? (double)totalProducidoGeneral / totalProgramadoGeneral * 100
        : 0;

    double porcentajeDefectuosoGeneral = totalProducidoGeneral > 0
        ? (double)totalDefectuosoGeneral / totalProducidoGeneral * 100
        : 0;

    Console.WriteLine($"Total de órdenes: {ordenesProduccion.Count}");
    Console.WriteLine($"Total programado: {totalProgramadoGeneral}");
    Console.WriteLine($"Total producido: {totalProducidoGeneral}");
    Console.WriteLine($"Total defectuoso: {totalDefectuosoGeneral}");
    Console.WriteLine($"Total sin defectos: {totalProducidoGeneral - totalDefectuosoGeneral}");
    Console.WriteLine($"Pendiente de producir: {totalProgramadoGeneral - totalProducidoGeneral}");
    Console.WriteLine($"Porcentaje de avance general: {porcentajeAvanceGeneral:F2}%");
    Console.WriteLine($"Porcentaje de unidades defectuosas general: {porcentajeDefectuosoGeneral:F2}%");

    Console.WriteLine("\nPresione cualquier tecla para continuar...");
    Console.ReadKey();
    break;

case 7:
    
    //Guardar los datos de productos y órdenes de producción en archivos de texto antes de salir
    List<string> lineasGuardar = new List<string>();
    foreach (Producto p in productos)
    {
        lineasGuardar.Add($"{p.Codigo}|{p.Nombre}|{p.Color}|{p.Talla}|{p.Encargado}|{p.FechaRegistro:O}|{p.CantidadPedido}|{p.CantidadProducida}|{p.CantidadDefectuosa}");
    }
    System.IO.File.WriteAllLines("productos.txt", lineasGuardar);

    List<string> lineasOrdenes = new List<string>();
    foreach (OrdenProduccion o in ordenesProduccion)
    {
        lineasOrdenes.Add($"{o.NumeroOrden}|{o.CodigoProducto}|{o.NombreProducto}|{o.Color}|{o.Talla}|{o.Encargado}|{o.CantidadProgramada}|{o.CantidadProducida}|{o.CantidadDefectuosa}|{o.FechaRegistro:O}");
    }
    System.IO.File.WriteAllLines("ordenes.txt", lineasOrdenes);

    Console.WriteLine("Saliendo del sistema. ¡Datos guardados correctamente!");
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

} while (opcion != 7);

//Clase Producto con validadores estaticos
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

    //Validar encargado
    public static string ValidarEncargado(string mensaje)
    {
        string entrada;
        string[] encargadosAutorizados = { "ANTHONY", "IZBET", "SEBASTIAN", "DANIEL", "RENATO" };
        while (true)
        {
            Console.Write(mensaje);
            entrada = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(entrada) && entrada.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                if (encargadosAutorizados.Contains(entrada.ToUpper()))
                {
                    return entrada;
                }
                Console.WriteLine("El nombre del encargado no está autorizado. Por favor, ingrese un encargado autorizado.");
            }
            Console.WriteLine("El nombre del encargado es inválido. Por favor, ingrese solo letras y espacios.");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }

    //Validar codigo
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
            Console.WriteLine("El código es inválido. El código debe tener el formato A00-XXXXX");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }

    //Validar producto
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
            Console.WriteLine("La prenda es inválida. Por favor, ingrese un producto disponible (camisa o polo).");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }

    //Validar color
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
            Console.WriteLine("El color es inválido. Por favor, ingrese un color disponible (rojo, azul, verde, amarillo, negro, blanco).");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
        }
    
    //Validar talla
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
            Console.WriteLine("La talla es inválida. Por favor, ingrese un valor válido (XS, S, M, L, XL, XXL).");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }
    
    //Validador generico de texto sin números ni simbolos donde no debe
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
            Console.WriteLine("Entrada inválida. Por favor, ingrese solo letras y espacios. (No se permiten números ni caracteres especiales)");
            Console.WriteLine("Intenta escribir de nuevo...");
            Console.ReadKey();
        }
    }
}

//Clase para gestion de ordenes 
class OrdenProduccion
{
    public int NumeroOrden { get; set; }
    public string CodigoProducto { get; set; } = "";
    public string NombreProducto { get; set; } = "";
    public string Color { get; set; } = "";
    public string Talla { get; set; } = "";
    public string Encargado { get; set; } = "";
    public int CantidadProgramada { get; set; }
    public int CantidadProducida { get; set; }
    public int CantidadDefectuosa { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    
    //Determinar automáticamente el estado de la orden
    public string Estado
    {
        get
        {
            if(CantidadProducida == 0)
                return "Pendiente";
                
            if(CantidadProducida >= CantidadProgramada)
                return "Completada";

            return "En producción";
        }
    }
}
