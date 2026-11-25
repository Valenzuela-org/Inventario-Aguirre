# Inventario Aguirre

Sistema de consola en C# para controlar el inventario de prendas y las órdenes de producción de la empresa Textileria Aguirre. Permite registrar productos por lote, crear órdenes de producción, registrar su avance y consultar reportes.

## Funcionalidades

El programa tiene un menú principal con estas opciones:

1. **Ingresar nuevo producto**: registro por lote, con un encargado para todo el lote.
2. **Actualizar producto existente**.
3. **Eliminar producto**: por código o vaciando todo el inventario.
4. **Buscar producto**: por código o listando todos.
5. **Gestión de producción**: registrar orden, actualizar avance y consultar reportes filtrados por código, color, talla o encargado.
6. **Reporte general de producción**: totales y porcentajes de avance y defectuosos.
7. **Salir**: guarda los datos en archivos de texto.

## Reglas de validación

- **Código de producto:** formato `A00-XXXXX` (hasta 5 dígitos) y único en el inventario.
- **Prendas válidas:** camisa o polo.
- **Colores válidos:** rojo, azul, verde, amarillo, negro, blanco.
- **Tallas válidas:** XS, S, M, L, XL, XXL.
- **Encargado:** se elige de un menú y se confirma con su DNI (cada encargado tiene el suyo).
- **Órdenes:** un producto no puede tener una nueva orden mientras otra suya siga sin completar. Los estados son Pendiente, En producción y Completada.

## Cómo ejecutarlo

Requisitos: [.NET SDK 10](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/Valenzuela-org/Inventario-Aguirre.git
cd Inventario-Aguirre
dotnet run
```

## Datos

Los datos se guardan al elegir la opción 7 (Salir) en `productos.txt` y `ordenes.txt`, con campos separados por `|`. Si los archivos no existen, el programa empieza vacío.

## Integrantes

- [RENATO WILFREDO AGUIRRE GARAY]
- [ROBERTO SEBASTIAN CCAHUANCAMA SOLIER]
- [ANDY IZBET CIEZA DIAZ]
- [DANIEL ENRIQUE SIFUENTES CHAVEZ]
- [JHON ANTHONY VALENZUELA ARMAS]

## Incidencias

Los errores detectados durante el proyecto y cómo se resolvieron están en [INCIDENCIAS.md](INCIDENCIAS.md).