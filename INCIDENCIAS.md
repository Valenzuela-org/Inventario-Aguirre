# Incidencias del proyecto

## Corregidas

| N.º | Incidencia | Causa | Solución |
|---|---|---|---|
| 1 | Al cerrar y abrir el programa, la nueva orden volvía a numerarse como N.º 1 | El contador de órdenes empezaba en 1 y no se recalculaba al cargar `ordenes.txt` | Al cargar los datos, el contador continúa desde el número de orden más alto + 1 |
| 2 | La carga de órdenes podía fallar con una línea incompleta | La condición pedía 8 campos pero el código leía 10 | La condición ahora exige 10 campos |
| 3 | Al vaciar el inventario, las órdenes reaparecían al salir | Se borraba el archivo pero no la lista de órdenes en memoria | Al vaciar también se limpian las órdenes y se reinicia el contador |
| 4 | Se podían registrar varias órdenes abiertas del mismo producto | No existía ninguna validación | No se permite una nueva orden si el producto ya tiene una sin completar |
| 5 | El encargado se escribía a mano: el nombre se guardaba como se tecleaba (`anthony` o `ANTHONY`), no había DNI y el programa mostraba dos mensajes de error a la vez | Validación solo contra una lista de nombres | El encargado se elige de un menú y se valida con el DNI propio de cada uno |
| 6 | Conflictos al unir ramas por los archivos de `bin/` y `obj/` | Se habían subido al repositorio | Se agregaron al `.gitignore` y se dejaron de rastrear |
| 7 | Tras unir `main` con una rama, `ValidarEncargado` quedó con código de las dos versiones y no compilaba | Git combinó dos ediciones de la misma función sin marcar conflicto | Se dejó solo la versión con menú y DNI y se comprobó con `dotnet build` |
| 8 | El mensaje de opción inválida del menú principal decía "del 1 al 5" aunque el menú llega hasta la 7 | El texto no se actualizó al agregar opciones | Se corrigió el mensaje a "del 1 al 7" |

## Conocidas y pendientes

| N.º | Incidencia | Detalle |
|---|---|---|
| 9 | "Actualizar producto" no usa los validadores | Permite cambiar a un código repetido o con formato inválido, y cambiar el encargado sin elegirlo del menú ni usar su DNI |
| 10 | Los datos se guardan solo al elegir "Salir" | Si se cierra la ventana con la X, se pierden los cambios de esa sesión |
| 11 | Eliminar un producto no elimina sus órdenes | Pueden quedar órdenes de productos que ya no existen |
| 12 | Los DNI de los encargados están escritos en el código | Son datos de ejemplo; en un sistema real deberían guardarse de forma segura |
| 13 | Los archivos de datos (`productos.txt` y `ordenes.txt`) están dentro del repositorio | Cambian cada vez que se ejecuta el programa y pueden mezclarse con datos de prueba |