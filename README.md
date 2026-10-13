# GameVault

Proyecto del segundo parcial de Móviles II. Es una app para llevar el control de tu colección de videojuegos: qué juegos tienes, en qué plataforma, cuánto valen, cuáles ya terminaste y cuáles quieres comprar. Cada juego puede tener reseñas.

Son dos proyectos en una sola solución:

- **GameVault.Api**: servicio OData hecho con ASP.NET Core y SQLite. Guarda los videojuegos y sus reseñas.
- **GameVault**: app .NET MAUI con Blazor Hybrid (un `BlazorWebView`). Consume la API y se ve con el estilo de SAP Fiori.

## Cómo correrlo

1. Levantar la API (escucha en el puerto 5190):

   ```
   dotnet run --project GameVault.Api
   ```

   La base se borra y se vuelve a crear con datos de ejemplo cada vez que arranca, así que la demo siempre empieza igual.

2. En otra terminal, correr la app (en Windows):

   ```
   dotnet run --project GameVault -f net10.0-windows10.0.19041.0
   ```

   En el emulador de Android la app usa `10.0.2.2` para llegar a la API de la computadora.

## Qué hace la app

- **Colección, Wishlist y Terminados**: tres pestañas que usan la misma lista con distinto filtro.
- **Buscador** por título (ignora mayúsculas y acentos).
- **Panel de detalle**: al seleccionar un juego se ve su información y sus reseñas. Se puede ampliar o cerrar. En pantallas angostas se abre como una hoja encima de la lista.
- **Detalle completo** de cada juego, con botón para volver a la pestaña de donde saliste, y para marcarlo como terminado o ponerlo en la wishlist.
- **Nuevo juego**: formulario que hace POST a la API.

## Cómo cumplimos cada requerimiento

### 1. Servicio OData propio con dos entidades relacionadas

`GameVault.Api` usa ASP.NET Core con SQLite (Entity Framework Core) y `Microsoft.AspNetCore.OData`. Hay dos entidades, `Videojuego` y `Resena`, con relación uno a muchos: `Videojuego.Resenas` es la propiedad de navegación y `Resena.VideojuegoId` es la llave foránea.

Endpoints principales (todos bajo `/odata`):

- `/odata/Videojuegos?$expand=Resenas` trae los juegos con sus reseñas.
- `/odata/Videojuegos?$filter=Plataforma eq 'PC'` filtra por plataforma.
- `/odata/Videojuegos(1)?$expand=Resenas` trae un solo juego.
- `/odata/Resenas?$filter=VideojuegoId eq 1` trae las reseñas de un juego.

También están POST, PATCH y DELETE para los videojuegos. La base es un archivo SQLite que se recrea en cada arranque.

### 2. Cliente MAUI con servicio tipado

- En `MauiProgram.cs` el servicio se registra con `AddHttpClient<IVideojuegoApiService, VideojuegoApiService>`, sin usar `HttpClient` suelto.
- Los DTOs están en `GameVault/Models` (`VideojuegoDto`, `ResenaDto`, `VideojuegoFormDto`).
- `ODataResponse<T>` (en `Services`) es la envoltura de la respuesta de OData (`@odata.context`, `@odata.count`, `value`).
- Propiedades calculadas que dependen de las reseñas, en `VideojuegoDto`: `PromedioTexto`, `TotalResenasTexto`, `CalificacionPromedio` y `TieneResenas`.

### 3. Master-detail con Cascading Parameters

`ListaVideojuegos.razor` guarda el juego seleccionado y lo manda con:

```razor
<CascadingValue Value="seleccionado" Name="VideojuegoSeleccionado">
    <PanelDetalle ... />
</CascadingValue>
```

`PanelDetalle.razor` lo recibe con `[CascadingParameter(Name = "VideojuegoSeleccionado")]` y pinta el detalle con sus reseñas. Al seleccionar se vuelve a pedir el juego a la API con `$expand=Resenas`.

### 4. Rutas parametrizadas

`Detalle.razor` tiene `@page "/videojuego/{Id:int}"`. Los datos se cargan en `OnParametersSetAsync` y no en `OnInitializedAsync`, porque al saltar de un juego a otro Blazor reutiliza el componente y solo cambia el `Id`. La ida lleva `?desde=` con la pestaña de origen, y el botón volver (y la miga de pan) regresan a esa pestaña.

### 5. Formulario de alta

`NuevoVideojuego.razor` usa `EditForm`, `OnValidSubmit` y `DataAnnotationsValidator`. En `VideojuegoFormDto` cada campo tiene al menos tres validaciones con su mensaje en español, que se muestra debajo del campo:

| Campo | Validaciones |
|---|---|
| Título | obligatorio, entre 2 y 120 caracteres, al menos una letra o número |
| Plataforma | obligatoria, que esté en la lista, máximo 30 caracteres |
| Estado | obligatorio, que esté en la lista, máximo 40 caracteres |
| Valor estimado | obligatorio, entre 0 y 100,000, máximo 2 decimales |
| Metacritic (opcional) | entre 0 y 100, solo enteros de hasta 3 dígitos, mensaje si no es número |
| URL de portada (opcional) | máximo 400 caracteres, URL válida, que empiece con `https://` |

Las dos casillas (wishlist y terminado) son booleanas y no llevan validación.

## Diseño SAP Fiori

Todo el color, el espaciado y la tipografía salen de variables CSS en `GameVault/wwwroot/css/fiori.css`. En `app.css` no hay colores sueltos. Lo que se aplicó de las guías:

- **Shell Bar** arriba en todas las pantallas, con logo en SVG y acción "Nuevo juego".
- **Page header** con título, subtítulo, acciones y migas de pan.
- **Lista de objetos** con título, atributos secundarios y chips de estado (colores semánticos).
- **Object Page** en el detalle completo, con secciones Información y Reseñas.
- **MessageStrip** (éxito, error, información y advertencia) para los avisos.
- **Botones** primario, secundario y ghost según la importancia de la acción.
- **SimpleForm**: etiquetas a la izquierda en pantallas anchas, una columna en celular, y asterisco en los obligatorios.
- **Accesibilidad**: se puede usar con teclado, foco visible, etiqueta en el buscador, `aria-current` en la pestaña activa, `role="status"` y `role="alert"` en los avisos, y el spinner respeta "reducir movimiento". Los chips y textos tienen contraste de al menos 4.5:1.
- **Responsive** con breakpoints en 72rem, 54rem y 30rem. Se revisó que nada se desborde a 360px.

El tema nativo de MAUI (colores, splash, ícono) también usa la paleta Fiori (`#0a6ed1`, `#354a5f`, `#f5f6f7`).

## Estructura

```
GameVault.Api/        servicio OData (controladores, modelos, base de datos)
GameVault/
  Components/         páginas y componentes Blazor
  Data/               repositorio que guarda en memoria lo que trae la API
  Models/             DTOs, chips de estado, validaciones
  Services/           servicio tipado y ODataResponse<T>
  wwwroot/css/        fiori.css (variables) y app.css (estilos)
```

## Equipo

Dereksit0, OmarGanem y Santi0108.
