# Instalaciones
https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-spa/9-knowledge-check

https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio
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
## Entity Framework Core
https://learn.microsoft.com/es-es/ef/core/
- Se instala por medio de la galería Nuget
- Entity Framework (EF) Core es una versión ligera, extensible, de código abierto y multiplataforma de la popular tecnología de acceso a datos Entity Framework.
<div style='background: #a8ff78;  /* fallback for old browsers */
background: -webkit-linear-gradient(to right, #78ffd6, #a8ff78);  /* Chrome 10-25, Safari 5.1-6 */
background: linear-gradient(to right, #78ffd6, #a8ff78); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
color: black; border-radius:10px; font-weight:bold; padding:10px;
'>
EF Core puede actuar como asignador relacional de objetos que se encarga de lo siguiente:
<ul>
    <li>Permite a los desarrolladores de .NET trabajar con una base de datos usando objetos .NET.</li>
    <li>Permite prescindir de la mayor parte del código de acceso a datos que normalmente es necesario escribir.</li>
</ul>
</div>

- Con EF Core el acceso a dato se realiza mediante un modelo.
    - Un modelo se compone de clases de entidad y un objeto de contexto que representa una sesión con la base de datos. Este bjeto de contexto permite consultar y guardar datos.

``` C#
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Intro;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True");
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public int Rating { get; set; }
    public List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
```

## Models
- Los modelos permiten crear los blueprints para los objetos a guardar en la base de datos.
- Pueden verse como un cajón en la filing cabinet.
- EF Core usa un modelo de metadatos para descríbir cómo se asignan los tipos de entidad de la aplicación a la base de datos subyacente.
- El modelo se crea con un conjunto de convenciones:
    - Heurística que busca patrones comunes.
- Después, el modelo se puede personalizar poer medio de atributos de asignación (también conocidos como anotaciones de datos) o llamadas a los métodos ModelBuilder (API fluida) en OnModelCreatin; ambos reemplazarán la configuración que realizan las conveciones.

## Relación One-To-Many
## ORM
- Object Relational Mapper, el cual se conoce como Entity Framerwork en .NET.
- Toma las tablas en bases de datos y las transforma en objetos.
- Se deben instalar las siguientes herramientas:
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.AspNet.Mvc
    - Microsoft.AspNetCore.Mvc.NewtonsoftJson (se necesita para asociarl modelos por medio de foreign key)
        - El anterior es la extensión de MVC, por lo que también se debe instalar Newtonsoft.Json.

<img src='ASP\FinShark\ImagenesC\Newton.png'></img>
- Se abre Nuget Gallery con Ctrl + Shift + p para poder instalar lo necesario.
- En el archivo de api2.csproj vienen las especificaciones del proyecto, en donde al momento de instalar los paquetes se debe seleccionar la casilla con el nombre de ese archivo, ya que corresponde con el del proyecto que se trabaja.

### Newton
- En Program.cs se debe colocar lo siguiente:
``` C#
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
```

- Esta librería previene los Objects Cycles

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

## UPDATE
- Modifica a todo aunque solo se desea modificar un campo.
- Se tienen los siguientges pasos:
    - Se busca por el valor.
        - var stock = firstOrDefault(1)
    - Cuando se encuentra el valor se modifica el objeto:
        - stock.companyName = "Microsoft"
    - NOTA: Con los dos pasos anteriores Entity Framework está teniendo un registro, es decir, lleva registro de los Updates que se hacen.
    - Se guarda los cambios en base de datos con saveChanges()
<img src='ASP\FinShark\ImagenesC\UPDATE.png'></img>

### Controlador StockController.cs
- Se usa HttpPut.
- Se usa tanto FromRoute como FromBody.
    - Con FromRoute solo se especifica que se espera la variable id, la cual es un entero.
    - Con FromBody se debe especificar el DTO para Update, por lo que primero se crea.
- En la lógica de Update, se deben asginar a todos los campos con el cuerpo del Update.

``` C#
        // Controlador para UPDATE
        [HttpPut]
        [Route("{id}")]
        // FromRoute es equivalente a req.params
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            // Se busca el valor deseado.
            var stockModel = _context.Stock.FirstOrDefault(x => x.Id == id);
            if(stockModel == null){
                return NotFound();
            }
            // Se actualizan todos los campos por medio del body
            stockModel.String = updateDto.String;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;

            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }
```

#### DTO para Stock, UpdateStockRequestDto.cs
- Tiene el mismo contenido que CreateStockRequestDto.
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        public string String { get; set; } = string.Empty; // Para evitar null reference errors
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
    }
}
```

## DELETE
- Los pasos para DELETE son:
    - Hallar el valor deseado:
        - var stock = firstOrDefault(1);
    - Eliminar el archivo usando Remove.
        - _context.Stock.Remove(stock);
    - NOTA: Entity Framework también lleva registro hasta el momento.
    - Se guardan los cambios con saveChanges.

### StockController.cs
``` C#
        // Controlador para DELETE
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockModel = _context.Stock.FirstOrDefault(x => x.Id == id);
            if(stockModel == null)
            {
                return NotFound();
            } 

            _context.Stock.Remove(stockModel);
            _context.SaveChanges();

            return NoContent();
        }
```

## async/await
- Se debe especificar que se retorna algo aún cuando la función no lo haga.
    - Esto se hace usando la palabra reservada Task, la cual envuelve con <> el tipo de dato.
``` C#
public async Task<Stock> GetStock {
    Console.WriteLine("Running");
    var stock = await getStockFromDB();
    Console.WriteLine("Running");
    return stock;
}
```

- Se convierte async el código que se ha escrito.
- StockController.cs
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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stock.ToListAsync();
            
            var stockDto = stocks.Select(stock1 => stock1.ToStockDto());
            // Stock se definió en Data, en ApplicationDBContext

            return Ok(stockDto);
        }

        // Por medio de model binding .NET va a extraer el stirng {id}, lo convierte a int y lo pasa al código
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stock.FindAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        // Controlador para POST
        [HttpPost]
        // FromBody es lo mismo que req.body
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        // Controlador para UPDATE
        [HttpPut]
        [Route("{id}")]
        // FromRoute es equivalente a req.params
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            // Se busca el valor deseado.
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null){
                return NotFound();
            }
            // Se actualizan todos los campos por medio del body
            stockModel.String = updateDto.String;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }

        // Controlador para DELETE
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return NotFound();
            } 

            _context.Stock.Remove(stockModel); // No es una operación asíncrona.
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
```

## Repository Pattern + Dependency Injection
- Se evita tener llamadas a la base de datos en los controladores.
    - Código como FirstOrDefault, el cual se ocupa en el controlador de GET, se reemplaza por FindStock.
        - FindStock es una función que contiene FirstOrDefault. Este función se encuentra en REPOSITORY.

<img src='ASP\FinShark\ImagenesC\Repository.png'></img>

- Repository Patterns y Dependency Injection ayudan a code to an abstraction.
    - FindStock es un abstracción. Se está escondiendo todo el código dentro de otro método
    - La idea principal es poder modificar ese código y afectar a otros.
- El 99% de la inyección de dependencia es constructor based.
    - En este caso, el objeto va a ser:

``` C#
constructor(DBContext context){´
    context=_context;
}
```

- La razón por la que se necesita Dependency Injection es porque se necesita preheat the metaphorical code oven. Se necesita tener objetos, se debe tener things lined up para que cuando se usen estos nuevos métodos, estas nuevas abstracciones que se van a crear, se tienen objetos a la mano. En este caso, el objeto que se tiene a la mano es the application DB context.
    - Se necesita a la base de datos antes de que se pueda hacer algo más.
    - En el constructor se va a pasar la base de datos así como se hizo con los controladores, solo que ahora va a ser en otra clase. 

### Interfaces
- Las interfaces permiten plug in el código en otros lugares y permitir abstraer el código.
- Se les conoce como Contracts.
- El archivo IStockRepository.cs es:

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Models;
using api2.Dtos.Stock;

namespace api2.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        // Se coloca ? ya que se usa FirstOrDefault, el cual puede ser null
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
    }
}
```

- Es una interfaz que define los métodos que se deben implementar para las llamadas de la base de datos.
    - La interfazse implementa en StockRepository, en donde cada método debe implementarse.

### Repository
- El propósito de la interface es ingresar IStockRepository e implementar la interface en StockRepository.

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Interfaces;
using api2.Models;
using Microsoft.EntityFrameworkCore;
using api2.Data;

// Con CTRL + . sobre el nombre de IStockRepository debería mostrar una lista para poder implementar la interface.
namespace api2.Repository
{
    public class StockRepository : IStockRepository
    {

        // Dependency Injection. Constructor.
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

       public Task<List<Stock>> GetAllAsync() 
       {
            return _context.Stock.ToListAsync();
       }
    }
}
```

- Para poder usar las interfaces y el repositorio se debe agregar en Program.cs lo siguiente antes del build de la app.

``` C#
using api2.Interfaces;
using api2.Repository;

...

builder.Services.AddScoped<IStockRepository, StockRepository>();

var app = builder.Build();
```

### Refactor to repository pattern
- Consiste en tomar el código de la base de datos y colocarlo en funciones ubicadas en el repository.
    - El repository está para alojar el código para las llamadas a la base de datos.

<img src='ASP\FinShark\ImagenesC\Repository2.png'></img>

- StockController luce así hasta el momento:

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
using api2.Interfaces;

namespace api2.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // ctor para colocar constructor
        // En el parámetro se coloca la DB por medio de DBContext
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            
            var stockDto = stocks.Select(stock1 => stock1.ToStockDto());
            // Stock se definió en Data, en ApplicationDBContext

            return Ok(stockDto);
        }

        // Por medio de model binding .NET va a extraer el stirng {id}, lo convierte a int y lo pasa al código
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stock.FindAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        // Controlador para POST
        [HttpPost]
        // FromBody es lo mismo que req.body
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        // Controlador para UPDATE
        [HttpPut]
        [Route("{id}")]
        // FromRoute es equivalente a req.params
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            // Se busca el valor deseado.
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null){
                return NotFound();
            }
            // Se actualizan todos los campos por medio del body
            stockModel.String = updateDto.String;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }

        // Controlador para DELETE
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return NotFound();
            } 

            _context.Stock.Remove(stockModel); // No es una operación asíncrona.
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
```

- Al reemplazar todo por repository se tiene lo siguiente:

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
using api2.Interfaces;

namespace api2.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // ctor para colocar constructor
        // En el parámetro se coloca la DB por medio de DBContext
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            
            var stockDto = stocks.Select(stock1 => stock1.ToStockDto());
            // Stock se definió en Data, en ApplicationDBContext

            return Ok(stockDto);
        }

        // Por medio de model binding .NET va a extraer el stirng {id}, lo convierte a int y lo pasa al código
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        // Controlador para POST
        [HttpPost]
        // FromBody es lo mismo que req.body
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        // Controlador para UPDATE
        [HttpPut]
        [Route("{id}")]
        // FromRoute es equivalente a req.params
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            // Se busca el valor deseado.
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if(stockModel == null){
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }

        // Controlador para DELETE
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            } 

            return NoContent();
        }

    }
}
```

- En StockRepository.cs se define la lógica para llamar a la base de datos, y en StockController se usan los métodos de Ok(), NotFount(), ToStockDto() para retornar al cliente el resultado de la operación. Por otro lado, en StockRepository solo que se retorna el resultado RAW de la base de datos o null.
- StockRepository.css

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Interfaces;
using api2.Models;
using Microsoft.EntityFrameworkCore;
using api2.Data;
using api2.Dtos.Stock;

// Con CTRL + . sobre el nombre de IStockRepository debería mostrar una lista para poder implementar la interface.
namespace api2.Repository
{
    public class StockRepository : IStockRepository
    {

        // Dependency Injection. Constructor.
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

       public async Task<List<Stock>> GetAllAsync() 
       {
            return await _context.Stock.ToListAsync();
       }

       public async Task<Stock> CreateAsync(Stock stockModel)
       {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
       }

        public async Task<Stock?> DeleteAsync(int id)
       {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return null;
            }

            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
       }

       public async Task<Stock?> GetByIdAsync(int id)
       {
            return await _context.Stock.FindAsync(id);
       }

       public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
       {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null) 
            {
                return null;
            }

            stockModel.String = stockDto.String;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;

            await _context.SaveChangesAsync();

            return stockModel;

       }
    }

}
```

## Comment System
- Los pasos enlistados a continuación no pueden completarse hasta hacer el siguiente, por lo que se recomienda primero crear los DTO para comment.
### Interface
- Se crea el archivo ICommentRepository.cs
- Se hereda ControllerBase a la clase antes de colocar las Routes

### Repository

### Dependency Injection en Program.cs
``` C#
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
```

### Controller
#### HttpPost
- Se crea la relación de comentario y stock por medio de la Foreign Key en Comment (StockId).
- El Id del Stock se va a obtener de la Route.
- Se verifiva que el ID exista creando un nuevo método en Stock
#### IStockRepository
- Se implementa en la interfaz StockExists
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Models;
using api2.Dtos.Stock;

namespace api2.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        // Se coloca ? ya que se usa FirstOrDefault, el cual puede ser null
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
```

#### StockRepository
- Se implementa el método, en donde se usa AnyAsync
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Interfaces;
using api2.Models;
using Microsoft.EntityFrameworkCore;
using api2.Data;
using api2.Dtos.Stock;

// Con CTRL + . sobre el nombre de IStockRepository debería mostrar una lista para poder implementar la interface.
namespace api2.Repository
{
    public class StockRepository : IStockRepository
    {

        // Dependency Injection. Constructor.
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

       public async Task<List<Stock>> GetAllAsync() 
       {
            return await _context.Stock.ToListAsync();
       }

       public async Task<Stock> CreateAsync(Stock stockModel)
       {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
       }

        public async Task<Stock?> DeleteAsync(int id)
       {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return null;
            }

            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
       }

       public async Task<Stock?> GetByIdAsync(int id)
       {
            return await _context.Stock.FindAsync(id);
       }

       public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
       {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null) 
            {
                return null;
            }

            stockModel.String = stockDto.String;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;

            await _context.SaveChangesAsync();

            return stockModel;

       }

       public async Task<bool> StockExists(int id)
       {
          return await _context.Stock.AnyAsync(stock => stock.Id == id)
       }
    }

}
```

### CommentDto
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Dtos.Comment
{
    public class CommentDto
    {
        public int Id {get; set;}
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        // No se desea la propiedad de Navigation, ya que va a inyectar otro objeto dentro de Comment.
    }
}
```

### CommentMappers.cs
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Dtos.Comment;
using api2.Models;

namespace api2.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }


        // Método de extensión que retorna Comment. En el argumento el tipo de dato que se usa es CreateCommentRequestDto, y el parámetro tiene el nombre de commentDto.
        // El método retorna un Comment (definido en Model), pero solo se asignan los campos necesarios para la creación.
        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto commentDto, int stockId)
        {
            return new Comment {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId,
            };
        }
    }
}
```

### Referencias Comments en 
https://www.youtube.com/watch?v=J1VuY2owXo4&list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc&index=13
- Se debe instalar la dependencia de Newton, la cual ya se explicó en al apartado de instalación de dependencias.
    - Se igual manera, se debe instalar la exntesión de Newton para MVC.
- Se debe agregar Comments en el StockDto

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Dtos.Comment;

namespace api2.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        // Acá no era String, era Symbol
        public string String { get; set; } = string.Empty; // Para evitar null reference errors
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        // No se retornan los Comments
        public List<CommentDto> Comments { get; set; }
    }
}
```

- Se debe agregar COMMENT en el ToStockDTO en DTO
``` C#
        public static StockDto ToStockDto(this Stock stockModel)
        {   // Los campos corresponden con el nombre colocado en el modelo, por eso empiezan con mayúscula pero en la db empiezan con minúscula.
            return new StockDto
            {
                Id = stockModel.Id,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                String = stockModel.String,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }
```

- En StockRepository se debe usar include para buscar los comentarios.
    - Include no sirve en conjunto con Find, por lo que se debe usar FirstOrDefault.

``` C#
       {
            return await _context.Stock.Include(c => c.Comments).ToListAsync();
       }
```

## Update Comments
- Es igual que con Stock, ya que las relaciones con Stock y Comment ya se han creado, por lo que solo se debe actualizar Content y Title del Comment.
- Queda pendiente saber por qué en Comment en el video se pasa el modelo del Commet y se crea un nuevo Mapper, en lugar de hacerlo como con el Stock, en donde se pasó commentDTO.
https://www.youtube.com/watch?v=wpBTiISt6UE&list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc&index=19

## Delete Comments

## Data Validation
### Simple Types
#### URL constraints or Route constraints
- Se define el tipo de dato del parámetro usando : seguido del tipo de dato.
``` C#
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var Comment = await _commentRepo.GetByIdAsync(id); 

            if(Comment == null)
            {
                return NotFound();
            }

            return Ok(Comment.ToCommentDto());
        }
```

#### Validation annotations
- Se usa Data annotations para definir las validaciones en los DTO.
- Se sugiere no hacerlo en el modelo para no impactar de forma global.
- Se usa la librería using System.ComponentModel.DataAnnotations;

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Para definir data validation

namespace api2.Dtos.Comment
{
    public class UpdateCommentRequestDto 
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must have more than 5 characters.")]
        [MaxLength(280, ErrorMessage = "Content must be less than 280 characters.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Title must have more than 5 characters.")]
        [MaxLength(280, ErrorMessage = "Content must be less than 280 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}
```

``` C#
        [Required]
        [Range(1, 10000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
```

- Luego, en los controladores se debe agregar ModelState para poder aplicar validaciones.
    - Model State viene de Controller Base.

``` C#
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateCommentRequestDto updateDto,[FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            // Se busca el valor deseado.
            var commentModel = await _commentRepo.UpdateAsync(id, updateDto);
            if(commentModel == null){
                return NotFound();
            }
            return Ok(commentModel.ToCommentDto());
        }
```

## Filtering
- WEn el códio generado se utiliza .ToList() para genera y ejecutar el SQL.
``` C#
var stocks = _context.Stocks.ToList();
```

- Por medio de .AsQueryable() se realiza un delay a ToList para poder implementar features de filtrado.

``` C#
var stocks = _context.Stocks.AsQueryable();
stocks.Where(s => s.Symbol == symbol);
stocks.Limit(2);
stocks.ToList();
```

- Se crea un objeto para que se pueda insertar el objeto en el argumento del controlador deseado. 
    - Se aplica Filtering a los Stocks, en donde se va a filtar por nombre de compañia y por Symbol (el cual se colocó con el nombre de 'String' accidentalmente).
    - El tipo de dato que se ocupa el QueryObject (es el tipo del objeto creado).
    - Se utiliza además [FromQuery] en el argumento de la función.
- Se crea la carpeta de Helpers, en donde se va a crear el archivo QueryObject.cs

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers
{
    public class QueryObject
    {
        public string? String { get; set; } = null; // Deberia ser Symbol, pero se colocó String como nombre por accidente.
        public string? CompanyName { get; set; } = null;
    }
}
```

- En el controlador de Stock GetAll se define la query como argumento para poder pasarla a _stockRepo.GetAllAsync. Esto significa que más adelante se debe modificar la interfaz para que ese método acepte ese parámetro.

``` C#
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
    
            var stocks = await _stockRepo.GetAllAsync(query);
            
            var stockDto = stocks.Select(stock1 => stock1.ToStockDto());
            // Stock se definió en Data, en ApplicationDBContext

            return Ok(stockDto);
        }
```

- En la interfaz de IStockRepository se modifica GetAllAsync para que acepte QueryObject.
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Models;
using api2.Dtos.Stock;
using api2.Helpers;

namespace api2.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        // Se coloca ? ya que se usa FirstOrDefault, el cual puede ser null
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
```

- Luego, se debe ajustar el repositorio de Stock para que tomer QueryObject e implementar el filtrado.

``` C#
       public async Task<List<Stock>> GetAllAsync(QueryObject query) 
       {
            //return await _context.Stock.Include(c => c.Comments).ToListAsync();
            var stocks = _context.Stock.Include(c => c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.String))
            {
                stocks = stocks.Where(stock => stock.String.Contains(query.String));
            }

            return await stocks.ToListAsync();
       }

```

## Sorting
- Se requiere el uso de AsQueryble.
- En la clase de QueryObject se define la propiedad SortBy, la cual puede ser nula.
- Se define la propiedad IsDescending.

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Helpers
{
    public class QueryObject
    {
        public string? String { get; set; } = null; // Deberia ser Symbol, pero se colocó String como nombre por accidente.
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; }
    }
}
```

- En StockRepository se revise que el parámetro no sea nulo o tenga espacios para poder aplicarlo después de las demás querys hechas.

``` C#
       public async Task<List<Stock>> GetAllAsync(QueryObject query) 
       {
            //return await _context.Stock.Include(c => c.Comments).ToListAsync();
            var stocks = _context.Stock.Include(c => c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.String))
            {
                stocks = stocks.Where(stock => stock.String.Contains(query.String));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                // Se compara que sea igual a la columna deseada, en donde acá String debería llamarse Symbol
                if(query.SortBy.Equals("String", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(stock => stock.String) : stocks.OrderBy(stock => stock.String);
                }
            }

            return await stocks.ToListAsync();
       }
```

## Pagination
- Permite no retornar todos los resultados de inmediato.
    - Los separa en diferentes 'páginas'.
- Se combinan las funciones .Skip() y .Take()

<a src='ASP\FinShark\ImagenesC\Pagination.png'></a>

- Se colocan las propiedades PageNumber y PageSize en QueryObject
    - PageSize indica la cantidad de elementos a mostrar por página.

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Helpers
{
    public class QueryObject
    {
        public string? String { get; set; } = null; // Deberia ser Symbol, pero se colocó String como nombre por accidente.
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
```

- En StockRepository se agrega la lógica en GetAllAsync.

``` C#
       public async Task<List<Stock>> GetAllAsync(QueryObject query) 
       {
            //return await _context.Stock.Include(c => c.Comments).ToListAsync();
            var stocks = _context.Stock.Include(c => c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.String))
            {
                stocks = stocks.Where(stock => stock.String.Contains(query.String));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                // Se compara que sea igual a la columna deseada, en donde acá String debería llamarse Symbol
                if(query.SortBy.Equals("String", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(stock => stock.String) : stocks.OrderBy(stock => stock.String);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
       }
```

# .NET
https://dotnet.microsoft.com/en-us/learn/videos?WT.mc_id=beginwebapis-c9-cephilli
https://www.youtube.com/watch?v=uytNCXw9dME&list=PLdo4fOcmZ0oUwBEC2bnwPtHqbU8Vmh_tj&index=3
- .NET is a free, open-source, & cross-platform development platform.
    - Cross-platform = Works & runs on Linux, macOS, and Windows.
    - Development platform = it means it has programming languages and libraries, so when put together it's a free development environment where developers can build applications for their customers.
- .NET supports C#, F# and more.
- Tools like Visual Studio and Visual Studio Code can be used.
- What can be built with .NET?
    - WEB
        - Build web apps and services for macOS, Windows, Linux and Docker.
    - Mobile and Desktop
        - Use single codebase to build native apps for Windows, macOS, iOS, and Android.
    - Cloud
        - Build scalable and resilient cloud native apps that run on all major cloud providers.
    - Microservices
        - Create independently deployable microservices that run on Docker containers.
- Whats the difference between .NET and .NET Framework?
    - .NET is the modernized version while .NET Framwork is the first rendition of our product.
<img src='ASP\FinShark\ImagenesC\NETvsNETFramework.png'></img>

- Versions to identify each one:
<img src='ASP\FinShark\ImagenesC\Versions.png'></img>
<img src='ASP\FinShark\ImagenesC\Learning.png'></img>

## Instalación de SDK de .NET en Windows
- Se dirige a la página de Microsoft para descagar el SDK de .NET, en donde la página ofrece la opción de descarga según el sistema operativo que se usa.
- Se escoge la instalación scripted para Linux, en donde al momento de descargar .dotnet NO se debe agregar --version, simplemente la primera parte del comando. https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#scripted-install
- Luego, se deben ajustar las variables de entorno según la siguiente página, la cual se da el link al momento de descargar .dotnet. https://learn.microsoft.com/en-us/dotnet/core/install/linux-scripted-manual#set-environment-variables-system-wide
- En Visual Studio Code solo se debe instalar la extensión de C# DEV KIT.
- Al finalizar, se presion CTRL + SHIFT + P para poder seleccionar la opción .NET CREATE NEW.
    - Esta opción ofrece una lista de posibles proyectos que se pueden crear.
- Para correr el programa se abre la terminal integrada (Se puede hacer click derecho sobre la carpeta del proyecto y abrirla), y luego correr el comando:

``` bash
dotnet run
```

# Vocabulario
- What i want to call out here is that it is recommended for all new development.

# Web APIs Beginner's Series
https://learn.microsoft.com/en-us/shows/beginners-series-to-web-apis/

## Creación de Proyecto en Visual Studio
- Se selecciona la opción: Create a new Project.
- Se 

<img src=''></img>



``` python
import pylightxl as xl 	
def ReadXLS(FilePath,colTable,sheetName,DateFormat):
	try:
		pathSplited = FilePath.split('/')
		fileName = pathSplited[len(pathSplited) - 1]
		readedFile = xl.readxl(fn=str(FilePath))	
		wsList =  readedFile.ws_names
		if sheetName in wsList:
			datosfil=[]
			table =[]
			countRows = 0 
			
			MyIter = iter(readedFile.ws(ws=sheetName).rows)
			rowFechas = next(MyIter)
			Fechas = []
			for item in rowFechas:
				if item != '':
					date = system.date.parse(item, DateFormat)
					Fechas.append(date)
			DataDemand = []
			for row in readedFile.ws(ws=sheetName).rows:
				countRows += 1
				if countRows > 2:	
					partnumber = str(row[1])
					description = str(row[2])
					query = 'SELECT * FROM partnumber where partnumber = ?'
					DataPartnumber = system.db.runPrepQuery(query,[partnumber],"")
					try:
						id_partnumber = DataPartnumber.getValueAt(0,"id_partnumber")
						id_line = DataPartnumber.getValueAt(0,"id_line")
					except:
						id_partnumber = 0
						id_line = 0
					DemandList = row[3:47]
					Turnos = []
					CountTurno = 0
					IndexFecha = 0
					DemandPerDay =[DemandList[i:i + 3] for i in range(0, len(DemandList), 3)]
					for demand in DemandPerDay:
						demand = [0 if value == '' else int(value) for value in demand]
						fecha = Fechas[IndexFecha]
						IndexFecha += 1
						rowDemand = [id_line, id_partnumber, partnumber, description, fecha] + demand
						if rowDemand[0] != 0:
							DataDemand.append(rowDemand)
			table = system.dataset.toDataSet(colTable, DataDemand)
			event.source.parent.getComponent('Power Table').data = table
			
		else:
				system.gui.messageBox('Sheet ' +sheetName+ ' not found in Excel Workbook: \n' + str(fileName),'File format error')
				table = []
		
		event.source.parent.getComponent('ProcessRunning').visible= False 
		
	except:
		system.gui.messageBox('This file does not correspond to pull .xlsx file','Canceled')
		event.source.parent.getComponent('ProcessRunning').visible = False 



selectedFilePath = system.file.openFile('xlsx')
sheetName = 'Pull'
colTable = ['id_line', 'id_partnumber', 'partnumber', 'description', 'Date', 'Turno1', 'Turno2', 'Turno3']
database = ""
DateFormat = event.source.parent.getComponent('DateFormat').selectedStringValue

try:
	if selectedFilePath != None:
		event.source.parent.getComponent('ProcessRunning').visible = True
		system.util.invokeAsynchronous(ReadXLS,[selectedFilePath,colTable,sheetName, DateFormat],'File read threat')
	else:
		system.gui.messageBox('No file selected','Canceled')
except:
	system.gui.messageBox('This file does not correspond to demand .xlsx file','Canceled')
```