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

## Conocidas y pendientes

| N.º | Incidencia | Detalle |
|---|---|---|
| 7 | "Actualizar producto" no usa los validadores | Permite cambiar a un código repetido o con formato inválido |
| 8 | Los datos se guardan solo al elegir "Salir" | Si se cierra la ventana con la X, se pierden los cambios de esa sesión |
| 9 | Eliminar un producto no elimina sus órdenes | Pueden quedar órdenes de productos que ya no existen |
| 10 | Los DNI de los encargados están escritos en el código | Son datos de ejemplo; en un sistema real deberían guardarse de forma segura |