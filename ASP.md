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
## ORM
- Object Relational Mapper, el cual se conoce como Entity Framerwork en .NET.
- Toma las tablas en bases de datos y las transforma en objetos.
- Se deben instalar las siguientes herramientas:
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.EntityFrameworkCore.Design
- Se abre Nuget Gallery con Ctrl + Shift + p para poder instalar lo necesario.
- En el archivo de api2.csproj vienen las especificaciones del proyecto, en donde al momento de instalar los paquetes se debe seleccionar la casilla con el nombre de ese archivo, ya que corresponde con el del proyecto que se trabaja.

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
- Se crean los archivos para los modelos de Stock y Comment.

## Data
### ApplicationDBContext
- Es una clase general (un objeto grande) que permite buscar tablas individuales.
- Al crear el archivo y llenarlo con el código requerido, se debe establecer la conexión a base de datos en Program.cs

## Creación de tablas en SQL Server
- Se ingresa a SQL Management, se selecciona con click derecho a la sección de Databases y se crea una base de datos, la cual solo hay que agregarle un nombre.
- En appsettings.json se agrega el campo de ConnectionStrings en el primer nivel del json.
    - Se agrega el campo Default, el cual contiene el valor de "DefaultConnection", el cual se puso en Program.cs:

``` C#
Program.cs

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // Va a buscar en appsettings.json
});
```

- En el Json se coloca un string en DefaultConnection, en el cual se debe modificar el nombre de la laptop (usuario con el que se inicia sesión en SQL Management y colocar el nombre de la base de datos).

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=LAPTOP-9335UPHE\\SQLEXPRESS;Initial Catalog=CsharpExercise;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" // En este link se debe colocar el nombra de la laptop y colocar en Catalog el nombre de la base de datos.
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

- Al colocar lo anterior se hace una migración.
 - Crean la base de datos detrás de escenas.

``` bash
dotnet ef migrations add init # Genera el código para build la base de datos.

dotnet ef database update # Efectúa los cambios en la base de datos.
```

- En caso de no tener dotnet ef se instala por medio de la siguiente página: https://learn.microsoft.com/en-us/ef/core/cli/dotnet
    - Solo se ejecutan los comandos:
``` Bash
dotnet tool install --global dotnet-ef

dotnet tool update --global dotnet-ef # Se debe ejecutar después de realizar los cambios en api2.csproj como se explica a continuación
```
- En el proyecto de api2.csproj se debe cambiar a falso la seccion de InvariantGlobalization
    
- Cuando se corre el comando de migrations add se crea automáticamente la carpeta de Migrations,