# Instalaciones
https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-spa/9-knowledge-check
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
    - Microsoft.AspNet.Mvc
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
- El tipo está en singular, seguido por el plural. (El plural debe coincidir con el nombre en base de datos, en este caso es Stock)

``` C#
        public DbSet<Stock> Stock { get; set; }// Se utiliza para recuperar los datos en base de datos.
        // DbSet Va a retornar los datos en la forma que se desee. Se habla de Deffered execution luego.

        public DbSet<Comment> Comments { get; set; }
```

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

## Controllers
- Por medio de ApplicationDBContext (definido en Data), se accede a los métodos Stocks y Comments definidos para poder efectuar las operaciones deseadas en la base de datos.
- Los controladores se deben agregar a Program.cs por medio de builder.Services.AddControllers() (se coloca al inicio), y app.MapControllers() (Se coloca justo antes de la última línea, la cual es app.Run()).

### Deferred 
- En el controlador de StockControllers, en el controlador para recuperar de base de datos se agrega toList:

``` C#
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.toList();
        }
```
- Eso va a retornar una lista como objeto.

### Prueba
- Para poder ejecutar la prueba se tuvo que instalar el paquete: Microsoft.AspNet.Mvc, y colocar lo siguiente en el archivode StockController.cs
  - using Microsoft.AspNetCore.Mvc;
- Se ejecuta el comando dotnet watch run.


## DTOs (Data Transfer Object)
- El 90% de los DTO caen en la categoría response request format.
- It spruces up the data (aclara los datos.)
- Response
  - No siempre se desea retornar todo un objeto llenos de información al usuario, sino que se desea retornar únicamente un objeto con los campos que el cliente va a ocupar.
    - Por ejemplo, con un usuario solo se desea retornar el usuario y no la contraseña.
- Request  
  - Tiene que ver con data validation.

### Response
- Se crea la carpeta de Dto en el route de la aplicación.
- Se crea una subcarpeta por cada modelo que se tiene.
 
#### StockDto.cs
- Se pegan del modelo Stock los campos que se desean retornar al cliente, tomando por ejemplo que solo no se desean retornar los comentarios.
  - Se pueden quitar las data annotations que se definen en algunos campos del modelo de Stock.

### Mappers
- Se crea la carpeta al nivel del root.
#### StockMappers.cs

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Dtos.Stock;
using api2.Models;

namespace api2.Mappers
{
    // Van a ser extension methods, por eso se coloca static.
    public static class StockMappers
    {   // Se va a crear un nuevo objeto y se colocan los campos que se desean devolver.
        public static StockDto ToStockDto(this Stock stockModel)
        {   // Los campos corresponden con el nombre colocado en el modelo, por eso empiezan con mayúscula pero en la db empiezan con minúscula.
            return new StockDto
            {
                Id = stockModel.Id,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                String = stockModel.String,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry
            };
        }
    }
}
```

- Se define una extensión de método que va a retornar un nuevo objeto con los campos deseados.
- Luego, en el cotrolador StockController.cs
  - En el controlador de HttpGet, GetAll se encadena Select.
    - Select es un Mapper, en donde al igual que en JavaScript se ocupa una arrow function.
    - Select es la versión .NET de MAP.
      - Entonces, va a mapear cada elemento para aplicar ToStockDto().
      - Va a retornar una lista inmutable de ToStockDto.

``` C#
       public IActionResult GetAll()
        {
            var stocks = _context.Stock.ToList()
            .Select(stock => stock.ToStockDto());
            // Stock se definió en Data, en ApplicationDBContext

            return Ok(stocks);
        }
```

- Para el caso de la API que solicita un ID se coloca directo ToStockDTO, ya que retorna un solo objeto.

### Request
#### CreateStockRequestDto.cs
- Se crea para limitar la cantidad de información que el usuario puede enviar a la API, de modo que se bloquea el envío el ID por ejemplo.

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Dtos.Dtock
{
    public class CreateStockRequestDto
    {
        public string String { get; set; } = string.Empty; // Para evitar null reference errors
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
    }
}
```

##### Mapper para CreateStockRequestDto
- Se crea en el archivo de StockMappers.cs ubicado en Mappers.
  - Se crea el Mapper ToStockFromCreateDTO para definir qué campos tiene que mandar el cliene al momento de hacer post.
  - El Mapper es un método de extensión.
  - Recibe al DTO CreateStockRequestDto y pasa el objeto stockModel.
  - Se crea un nuevo objeto para poder pasarlo al método _context.Stock.Add()

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Dtos.Stock;
using api2.Models;

namespace api2.Mappers
{
    // Van a ser extension methods, por eso se coloca static.
    public static class StockMappers
    {   // Se va a crear un nuevo objeto y se colocan los campos que se desean devolver.
        public static StockDto ToStockDto(this Stock stockModel)
        {   // Los campos corresponden con el nombre colocado en el modelo, por eso empiezan con mayúscula pero en la db empiezan con minúscula.
            return new StockDto
            {
                Id = stockModel.Id,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                String = stockModel.String,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry
            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockModel)
        {
            return new Stock
            {
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                String = stockModel.String,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry
            };
        }
    }
}
```

## POST
- En la mayoría de los casos se usa Entity Framework.

``` C#
_context.Stocks.Add(json); // Va a empezar tener un registro de la data pero no va a realizar el campo en base de datos.
_context.SaveChanges();
```

- En el controlador StockController se agrega el método [HttpPost]
  - Se define el controlador.

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api2.Data;
using api2.Models;
using api2.Mappers;
using api2.Dtos.Stock;
using Microsoft.EntityFrameworkCore;


namespace api2.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // ctor para colocar constructor
        // En el parámetro se coloca la DB por medio de DBContext
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stock.ToList().Select(stock1 => stock1.ToStockDto());
            // Stock se definió en Data, en ApplicationDBContext

            return Ok(stocks);
        }

        // Por medio de model binding .NET va a extraer el stirng {id}, lo convierte a int y lo pasa al código
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stock.Find(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        // Controlador para POST
        [HttpPost]
        // FromBody es lo mismo que req.body
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stock.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

    }
}
```