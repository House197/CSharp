# Instalaciones
## Visual Studio
- Es posible trabajar con Visual Studio Code, sin embargo se requiere instalar Visual Studio para tener todas las dependencias.
- Se dirige a la página <a href='https://visualstudio.microsoft.com/es/'>Visual Studio</a> y se descarga **Community 2022**.
- Al instalar el programa solo se deben seleccionar las siguientes dependencies:
    - ASP.NET and web development.
    - Azure development.
## SQL Server
- En la página de <a href='https://www.microsoft.com/es-MX/sql-server/sql-server-downloads'>Microsoft</a> y se instala la versión Express.
    - Si en un futuro se desean agregar las features que brinda el paquete de desarrollador, Microsoft permite instalarlas después.
    - En el instalador de SQL Server se selecciona la versión BASIC.
    - Al finalizar la instalación se presiona el botón <a href='https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16&redirectedfrom=MSDN'>Install SSMS</a>.
- Para poder trabajar en Visual Studio Code hace falta instalar el SDK de .NET Core.
https://dotnet.microsoft.com/es-es/download

# Teoría
## Models
- Los modelos permiten crear los blueprints para los objetos a guardar en la base de datos.
- Pueden verse como un cajón en la filing cabinet.

## Relación One-To-Many


## Extensiones
- Se recomienda instalar las siguentes extensiones en Visual Studio Code.
    - C#
    - C# Dev Kit (Instala la mayoría de las extensiones en la lista)
    - .NET Extension Pack
    - .NET Install Tool
    - Nuget Gallery
    - Prettier
    - Extension Pack JosKreative (más opcional)

# Proyecto FinShark (Stock App) 
## Creación del proyecto ASP
- Se crea el proyecto con el siguiente comando:

``` Bash
dotnet new webapi -o api
```

- Se verifica que el proyecto corre bien con el siguiente comando, el cual se ejecuta en la carpeta creada del proyecto.

``` Bash
dotnet watch run
```

- El comando abrirá el navegador predeterminado con el puerto del localhost requerido para ver la aplicación.

## Program.cs
- Es similar al archivo index.js de NodeJS.
- Se limpia el boilerplate y se dejan las líneas de código necesarias.
- Se controlan las inyecciones de dependencia, proveer servicios, gestionar peticiones HTTP, gestión de Middlewares.

``` C#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
```

## Modelos
- Se crea la carpeta Models.