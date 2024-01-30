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
    - .NET Core is an evolutoin of the .NET framework, optimized for cross-platform use and designed to be fully open-source. Microsoft is working to integrate a lot of its frameworks and libraries into one product and under one SDK callend .NET 5.0
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

## Nuget Package
- It is the .NET package manager.
- Se puede visitar la página de Nuget y explorar las librerías. https://www.nuget.org/
- Se pueden descargar librerías por medio del comando que Nuget ofrece para la librería deseada y luego colocarlo en la terminal intergada en el proyecto.

# C# for Beginners
https://www.youtube.com/watch?v=3ST-7TP09qk&list=PLdo4fOcmZ0oULFjxrOagaERVAMbmG20Xe&index=3
- What is IDE?
    - Integrated Development Environment, like Visual Studio or VSC.
- What is the command dotnet?
    - That's the commando line tool used to create projects, run apps.
    - It is the one entrypoint CLI (Command Line Interface)
- What is the Program.cs file?
    - It is the main program. It is the entrypoint. It is wqhere the code runs from.

## Basics of VS Code and C# Dev Kit
- C# Dev Kit aplicará cuando se abre directamente el folder del proyecto en lugar de un folder padre.
    - Esto habilita Solution Explorer, así como poder crear archivos de C# con una plantilla de clase entre otros.
- Solution Explorer presenta un vista lógica del proyecto, los main source files, dependencies.

# Vocabulario
- What i want to call out here is that it is recommended for all new development.
- It is really neat.
- APIs are a common set of collections to be executed agains a given collection of resources.

# Web APIs Beginner's Series
https://learn.microsoft.com/en-us/shows/beginners-series-to-web-apis/
- ASP.NET makes it easy to build services that reach a broad range of clients, including browsers and mobile devices.
- With ASP.NET you use the same framework and patterns to build both web pages and services, side-by-side in the same project.

- ver videos de C#, .NET Core y APS.NET Core. En el video según están en .NET videos
    - dotnet.microsoft.com/learn/videos

- API representa APPLICATION PROGRAMING INTERFACEm y esencialmente es una colección de operaciones que se pueden invocar de forma remota o local.
    - WEB APIs son operaciones que se pueden invocar por medio de HTTP para aprovechar las funcionalidades que esas operaciones ofrecen.
    - Entonces, son un conjunto de operaciones que se desean ejecutar contra una colección de recursos.

## HTTP
- Significa Hypertext Transfer Protocol.
- Permite que dos sistemas se comuniquen a pesar de que no estén en el mismo lugar.
- Es un estandard abieto definido en un documento llamado RFC 2616
- El protocolo confia en el concepto de clientes y el servidor.
    - Cliente: app o entidad que genera solicitudes.
    - Servidor: Recibe las solicitudes, las proces y genera una respuesta HTTP.
- Los headers son key-value pairs que contienen metada asociada con las solicitudes respectivas.
- En el Body se tiene información que opcionalmente se puede enviar junto a la solicitud.
<img src='ASP\FinShark\ImagenesC\HTTPReq.png'></img>
<img src='ASP\FinShark\ImagenesC\HTTPRES.png'></img>
![Alt text](image.png)

## Creación Web API usando ASP.NET Core
https://learn.microsoft.com/en-us/shows/beginners-series-to-web-apis/creating-a-web-api-project-3-of-18--beginners-series-to-web-apis
- En el video se usa Visual Studio, pero la práctica se hace en Visual Studio Code.
- Se presion CTRL + SHIFT + p para abrir las opciones de VS Code.
    - Se selecciona .NET Create Project.
    - Se selecciona la opción del Proyecto WEB API.
- Se debe definir el proyecto en una carpeta que no contenga ya otro proyecto de C# para evitar conflictos.
- Esta opción solo provee del Program.cs en donde ya se encuentra la API de muestra en conjunto con el record de WeatherForecast, los cuales se eliminarán para poder definir las APIs deseadas.

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

- En Visual Studio, Program.cs tiene líneas adicionales:

``` C#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
```

### Controllers
- Se crea la carpeta Controllers en el root del proyecto.
- El nombre del archivo debe terminar con Controller.cs para que ASP.NET Core lo identifique.
- En ASP.NET Core un controlador recibe las solicitudes que coinciden con una path en particular.
    - Se le definine varias operaciones para inspeccionar las solicitudes, procesarlas y retornar información.
        - A estas operaciones se les denomina Action Methods.
- En el ejemplo se declara el controlador: RecipesController.cs
- Un controlador vacío debe lucir de la siguiente forma:

``` C#
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class RecipesController : ControllerBase
    {

    }
}
```

- Namespaces 
    - Microsoft.AspNetCore.Http: Proporciona tipos para trabajar con objetos HTTP, como solicitudes y respuestas.
    - Microsoft.AspNetCore.Mvc: Contiene clases e interfaces para trabajar con el patrón Modelo-Vista-Controlador (MVC) en ASP.NET Core.
- Declaración del Namespace y Clase:
    - namespace API.Controllers: Indica que la clase RecipesController está dentro del espacio de nombres API.Controllers.

``` C#
namespace API.Controllers
{
    //...
}
```

- Atributos de Ruta y Controlador
    - [Route("api/[controller]")]: Define la ruta base para las acciones en este controlador. El [controller] se reemplazará con el nombre del controlador sin el sufijo "Controller". Por ejemplo, si el nombre de la clase es RecipesController, la ruta base será /api/Recipes.
    - [ApiController]: Indica que esta clase es un controlador de API, lo que proporciona ciertas convenciones y características específicas de API.

``` C#
[Route("api/[controller]")]
[ApiController]
```

- Declaración de la Clase del Controlador
    - public class RecipesController: Declara la clase del controlador llamada RecipesController.
    - : ControllerBase: Indica que esta clase hereda de ControllerBase, que es una clase base para controladores en ASP.NET Core.

``` C#
public class RecipesController : ControllerBase
{
    //...
}
```

- Para este controlador, se coloca un Action Method usando el verbo GET de HTTP.
    - Un Action Method se conforma del verbo HTTP, y el método C# para procesar la solicitud y retornar información al cliente.

``` C#
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class RecipesController : ControllerBase
    {
        [HttpGet]
        public string[] Dishes()
        {
            string[] dishes = {"Pizza", "Dumplings", "Rice"};
            return dishes;
        }
    }
}
```
### CRUD Conventions

<img src="ASP\FinShark\ImagenesC\HTTPMethods.png"></img>

- Siguiendo las conveciones el controlador de Recipes luce así:

``` C#
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class RecipesController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetRecipes()
        {
            string[] recipes = {"Pizza", "Dumplings", "Rice"};

            if(recipes.Any())
                return NotFound();
            return Ok(recipes);
        }

        [HttpDelete]
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return NoContent();
        }
    }
}
```
- GetRecipes()
    - [HttpGet]: Es un atributo que indica que este método manejará las solicitudes HTTP GET. En otras palabras, este método se invocará cuando se realice una solicitud GET a la ruta /api/recipes.
    - public ActionResult GetRecipes(): Declara un método público llamado GetRecipes que devuelve un objeto ActionResult. ActionResult es una clase base para varios tipos de resultados de acción.
    - if(recipes.Any()): Verifica si hay elementos en el array recipes. Any() es un método de extensión que devuelve true si la secuencia tiene al menos un elemento.
    - return NotFound();: Si no hay elementos en recipes, devuelve un resultado HTTP 404 (NotFound). Esto indica que no se encontraron recursos.
    - return Ok(recipes);: Si hay elementos en recipes, devuelve un resultado HTTP 200 (Ok) con el array recipes como cuerpo de la respuesta. Esto indica que la solicitud fue exitosa y devuelve los datos solicitados.

- Los métodos retornan el tipo ActionResult.
- Se response a la solicitud por medio de métodos HTTP.
    - OK()
    - NoContent()
    - BadRequest()
    - NoFound()

### ActionResult
- Es una clase base para varios tipos de resultados de acción que un controlador puede devolver. ActionResult permite a un controlador devolver diferentes tipos de resultados de acción según el escenario. Puede devolver tipos específicos como OkResult, NotFoundResult, BadRequestResult, o incluso tipos complejos como ObjectResult para devolver datos junto con el código de estado HTTP.
- ActionResult es versátil y puede adaptarse a diferentes situaciones. Puedes personalizar el resultado según las necesidades de tu aplicación, ya sea devolviendo datos, redirigiendo a otra acción, devolviendo un código de estado específico, o incluso devolviendo un resultado de tipo diferente.
- Además de los métodos específicos mencionados anteriormente, ActionResult tiene varios tipos derivados que puedes usar directamente, como OkResult, NotFoundResult, BadRequestResult, ObjectResult, entre otros. Cada uno de estos tipos representa un tipo específico de resultado de acción.
- ActionResult es compatible con la negociación de contenido, lo que significa que ASP.NET Core puede seleccionar automáticamente el formato de respuesta basándose en las preferencias del cliente (por ejemplo, JSON o XML). Esto permite construir APIs que pueden responder en diferentes formatos según las preferencias del consumidor.
- Viene de ControllerBase.
- Son útiles también para generar la metadata usada en Swagger UI.
### Routing
https://learn.microsoft.com/en-us/shows/beginners-series-to-web-apis/understanding-web-api-routes-6-of-18--beginners-series-to-web-apis
- URLs are essentially the address of the API.
- Using combinations of URLs, HTTP methods, and some other information inside the requests, ASP.NET Core is able to invoke the right action with it inside of the controller and a processor requests (this is called routing.)

<img src='ASP\FinShark\ImagenesC\Routing.png'></img>

- Se usan routing attributes
    - Se puede ser más granular a nivel del Action Method.
        - En el atributo HTTP se puede especificar el endpoint.

``` C#
        [HttpDelete("all")] // api/recipes/all
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return NoContent();
        }
```

- Además de especificar el endpoint, también se puede especificar un token para poder usarlo como variable.
    - Se usan curly brackets.
    - ASP.NET Core se encarga de hacer coincidir el token con el parámetro del método. En otras palabras, la data pasa hacia el Action Method

``` C#
        [HttpDelete("{id}")] // api/recipes/12
        public ActionResult DeleteRecipes(string id)
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return NoContent();
        }
```

### Swagger UI
- Swagger is a collection of open source tools that make it easier for developers to build the consume web APIs
- Among the tools are:
    - Client generation.
    - API documentation.
    - The ability to debug the APIs lab using the UI.
- Swagger relies on something known as the open API specification. That spec allows to define some of the detailed things that are APIs are able to do.
- Se habilita en el proyecto por medio del package Swashbuckle, el cual se puede ver en API.csproj.

``` csproj
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <InvariantGlobalization>true</InvariantGlobalization>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
  </ItemGroup>

</Project>

```

- Por defecto está disponible en la plantilla WEB Api en .NET 5.

### Binding to HTTP request data
- ASP.NET Core defines a lot of types and properties that we can use to inspect the incoming request.
    - We can even define the types of the information we want to pull out of the request and have it provided to the action methods as parameters (binding).
- Se puede definir el tipo de información que se desea obtener (pull out) de la petición y proveerla como parámetros a los action methods.
- Se puede obtener información de varias partes de la solicitud:
    - Del cuerpo.
    - De query.
    - De Headers.
    - De Form.

<img src='ASP\FinShark\ImagenesC\Binding.png'></img>

- Se definen por medio de binding attributes.
- Por ejemplo, se limita la cantidad de recursos que se retorna de la ruta de GET.
    - Se usa [FromQuery] y Take(), el cual proviene de Linq.

``` C#
    [HttpGet]
    public ActionResult GetRecipes([FromQuery] int count)
    {
        string[] recipes = {"Pizza", "Dumplings", "Rice"};

        if(!recipes.Any())
            return NotFound();
        return Ok(recipes.Take(count));
    }
```

### API Data Models
#### Records
- Se introdujo en C# 9.
- Son similares a las clases con la ventaja de que cuentan con beneficios extra.
    - We get support for thinks like immutability and equality.

<img src='ASP\FinShark\ImagenesC\Record.png'></img>

- Se crea la carpeta de Models y el archivo Records.cs

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public record Recipe
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public IEnumerable<string> Directions { get; init; }
        public IEnumerable<string> Ingredients { get; init; }
        public DateTime Updated { get; init; }
    }
}
```

- En el controlador se modifica el Action Method GET.
    - En este Action Method ahora se usa el Tipo Recipe (el cual es el modelo creado en Records).
    - Solo se llena la propiedad de Title por simplicidad.
    - De igual manera, se implementa el método Post para crear una nueva receta usando el tipo Recipe.

``` C#
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class RecipesController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetRecipes([FromQuery] int count)
        {
            Recipe[] recipes = {
                new() { Title = "Oxtail" },
                new() { Title = "Curry Chicken" },
                new() { Title = "Dumplongs" } 
            };

            return Ok(recipes.Take(count));
        }

        [HttpPost]
        public ActionResult CreateNewRecipe([FromBody] Recipe newRecipe)
        {
            bool badThingsHappened = false;
            if (badThingsHappened)
                return BadRequest();
            
            return Created("", newRecipe);
        }

        [HttpDelete("{id}")] // api/recipes/a23
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return NoContent();
        }
    }
}
```

### Conección a MongoDB Atlas
https://learn.microsoft.com/en-us/shows/beginners-series-to-web-apis/connecting-to-a-data-store-11-of-18--beginners-series-to-web-apis
- Se conecta a MongoDB.
    - MongoDB guarda la información como documentos JSON.
    - Open-Source Database.
    - Official C# library.
    - Supported Azure Cosmos DB.
- Los pasos seguidos están en la <a href='https://www.mongodb.com/languages/how-to-use-mongodb-with-dotnet'> documentación </a> de MongoDB, así como del <a href='https://github.com/mongodb-developer/mongodb-dotnet-example/blob/main/Services/GamesService.cs'>repositorio</a> que brinda como ejemplo.

#### 1. Creación de Cluster y obtención de String para conección
1. Se crea un cluster nuevo (un nuevo proyecto).
2. Se agrega información propia para poder crear la DB y la Collection.
3. En Overview del cluster creado se selecciona la opción de **Connect**
4. En la ventana emergente, en el apartado **Connect to your application** se selecciona la opción **Drivers**.

<img ref='ASP\FinShark\ImagenesC\MongoDBDrivers.png'></img>

5. Se selecciona el Driver de C# y se copia el URL de conexión.
<img ref='ASP\FinShark\ImagenesC\Connection.png'></img>

#### 2. Instalación de MongoDB.Driver
1. Se abre el gestos de dependencias Nuget usando Ctrl + Shift + p.
2. En el campo emergente se escribe NuGet: Open NuGet Gallery.
3. Se filtran los paquetes escribiendo MongoDB.
4. Se descarga el paquete MongoDB.Driver, en donde se debe seleccionar el square para marcar el archivo API.csproj (en la imagen ya se había instalado el paquete).

<img src='ASP\FinShark\ImagenesC\MongoDBDrivers.png'></img>

#### 3. Creación de archivo ServicesDatabaseSettings para colocar datos relacionados a conexión a DB
- Se crea el archivo en Models.
- Se crea la interfaz en el mismo documento, en donde se deben definir las propiedades:
    - RecipesCollectionName
    - ConnectionString
    - DatabaseName
- Esta clase de utilizará en RecipesService

``` C#
namespace API.Models
{
    public class RecipesDatabaseSettings : IRecipesDatabaseSettings
    {
        public string RecipesCollectionName { get; set; } = "RecipesCollection";
        public string ConnectionString { get; set; } = "mongodb+srv://Houser97:Houser97@cluster0.bcp5lex.mongodb.net/RecipesDB?retryWrites=true&w=majority";
        public string DatabaseName { get; set; } = "RecipesDB";
    }

    public interface IRecipesDatabaseSettings
    {
        string RecipesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
```

#### 4. Creación de archivo RecipesService para lógica de DB
- En el archivo se define la conección a la base de datos así como las operaciones que se desean efectuar en contra de la DB.
- Se crea la Carpeta de Services.

``` C#
using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class RecipesService
    {
        private readonly IMongoCollection<Recipe> _recipes;

        public RecipesService(IRecipesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _recipes = database.GetCollection<Recipe>(settings.RecipesCollectionName);
        }

        public Recipe Get(string id) => _recipes.Find(recipe => recipe.Id == id).FirstOrDefault();

        public Recipe Create(Recipe recipe)
        {
            _recipes.InsertOne(recipe);
            return recipe;
        }
    }
}
```

1. Declaraciones.
    - Se utiliza using para importar espacios de nombres, en donde MongoDB.Driver.
2. Definición de campo privado _recipes.
    - El campo almacena una instancia de **IMongoCollection<Recipe>**, el cual representa una colección de Recipes en la base de datos de MongoDB. 
    - La interfaz **IMongoCollection** es parte del controlador de MongoDB.
        - Es una interfaz en el contorlador oficial de MongoDB para .NET que representa una colección en una base de datos MongoDB.
        - Proporciona métodos y propiedades que permiten interactuar con los documentos dentro de una colección.
        - En el contexto de este código, **_recipes** se refiere a la colección de documentos MongoDB, y cada documento en esta colección se espera tenga una estructura de la clase **Recipe**.
        - En esencia, IMongoCollection es una parte clave del controlador de MongoDB para .NET y proporciona una interfaz para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en documentos almacenados en una colección MongoDB.
    - **Recipe** es el modelo creado en Models.

``` C#
private readonly IMongoCollection<Recipe> _recipes;
```

3. Constructor **RecipesService**
    - Recibe un objeto **IRecipesDatabaseSettings**, el cual proviene de RecipesDatabaseSettings definido en el paso anterior.
    - Se define un cliente MongoDB **MongoClient** usando la cadena de conexión proporcionada.
    - Se obtiene la base de datos **GetDatabase**.
    - Se obtiene la colección **GetCollection<Recipe>**

``` C#
public RecipesService(IRecipesDatabaseSettings settings)
{
    var client = new MongoClient(settings.ConnectionString);
    var database = client.GetDatabase(settings.DatabaseName);

    _recipes = database.GetCollection<Recipe>(settings.RecipesCollectionName);
}
```

4. Métodos.
    - Después del constructor se definen los métodos que interactuan con la DB para realizar operaciones CRUD.

``` C#
        public Recipe Get(string id) => _recipes.Find(recipe => recipe.Id == id).FirstOrDefault();

        public Recipe Create(Recipe recipe)
        {
            _recipes.InsertOne(recipe);
            return recipe;
        }
```

#### 5. Ajuste de modelo Recipe, ubicado en Records.cs

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        
        [BsonElement("Title")]
        public string Title { get; init; }
        public string Description { get; init; }
        public IEnumerable<string> Directions { get; init; }
        public IEnumerable<string> Ingredients { get; init; }
        public DateTime Updated { get; init; }
    }
}
```

1. Declaraciones.
    - Estas declaraciones using importan los espacios de nombres necesarios para la definición de la clase Recipe. 
        - System contiene clases fundamentales de .NET.
        - System.Collections.Generic y System.Linq para manipulación de colecciones.
        - System.Threading.Tasks para trabajar con tareas asincrónicas. 
        - MongoDB.Bson y MongoDB.Bson.Serialization.Attributes para trabajar con objetos BSON en MongoDB.

2. Atributos de serialización BSON
    - Estos atributos son de la biblioteca BSON de MongoDB y se utilizan para indicar cómo serializar y deserializar la clase en BSON.
        - [BsonId] indica que la propiedad Id es la clave primaria del documento.
        - [BsonRepresentation(BsonType.ObjectId)] especifica que la representación en BSON de la propiedad Id debe ser un ObjectId.

``` C#
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
```

3. [BsonElement("Title")]
    - [BsonElement("Title")] es un atributo de la biblioteca BSON de MongoDB que se utiliza para especificar el nombre del campo cuando se serializa/deserializa un objeto a/desde BSON.
    - Cuando un objeto de la clase Recipe se serializa a BSON (por ejemplo, antes de almacenarlo en MongoDB) o se deserializa desde BSON (por ejemplo, al recuperar datos de MongoDB), la anotación [BsonElement("Title")] le dice a la biblioteca BSON que el nombre del campo BSON asociado a la propiedad Title debe ser "Title" en lugar del nombre de la propiedad en la clase.

    - Esto puede ser útil en situaciones donde el nombre del campo en la base de datos MongoDB es diferente al nombre de la propiedad en la clase. Por ejemplo, si en la base de datos se utiliza "RecipeTitle" pero en la clase se quiere mantener simplemente "Title", puedes usar [BsonElement("RecipeTitle")] para mapear correctamente entre la propiedad de la clase y el campo de la base de datos.

    - En resumen, [BsonElement("Title")] personaliza el mapeo entre el nombre de la propiedad de la clase y el nombre del campo BSON cuando se interactúa con MongoDB. En este caso específico, se asegura de que el campo BSON asociado a la propiedad Title tenga el nombre "Title".

#### 6. Ajuste de controlador RecipesController

- Se realiza la inyección de independencia de _recipeService

``` C#
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Models;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class RecipesController : ControllerBase
    {
        private readonly RecipesService _recipeService;

        public RecipesController(RecipesService recipesService)
        {
            _recipeService = recipesService;
        }

        [HttpGet]
        public ActionResult GetRecipes([FromQuery] int count)
        {
            Recipe[] recipes = {
                new() { Title = "Oxtail" },
                new() { Title = "Curry Chicken" },
                new() { Title = "Dumplongs" } 
            };

            return Ok(recipes.Take(count));
        }
    
        [HttpGet("{id:length(24)}", Name = "GetRecipe")]
        public ActionResult<Recipe> Get(string id)
        {
            var recipe = _recipeService.Get(id);

            if(recipe == null)
            {
                return NotFound();
            }
            
            return recipe;
        }

        [HttpPost]
        public ActionResult<Recipe> CreateNewRecipe([FromBody] Recipe newRecipe)
        {
            bool badThingsHappened = false;
            if (badThingsHappened)
                return BadRequest();
            
            //_recipeService.Create(newRecipe);
            


            Recipe newRecipeTest = new Recipe()
            {
               Title="RecipeTestFull4"
            };

            _recipeService.Create(newRecipeTest);
            
            return CreatedAtRoute("GetRecipe", new { id = newRecipeTest.Id.ToString() }, newRecipe);
        }

        [HttpDelete("{id}")] // api/recipes/a23
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return NoContent();
        }
    }
}
```

- Pasos:
1. Definición del campo privador **_recipeService**
    - Se decalara este campo privado, el cual es de tipo RecipesService.
    - Se marca como solo lectura, por lo que solo se puede inicializar en el constructor y no se puede modificar posteriormente.
``` C#
private readonly RecipesService _recipeService;
```

2. Constructor de la clase **RecipesController**.
    - Toma un parámetros de tipo **RecipesService** y lo asigna al campo **_recipeService**.
    - Esto se le conoce como inyección de dependencias, donde la dependencia **RecipesService** se pasa al componente dependiente **RecipesController** a través del constructor.
    - Inyectar la dependencia de esta manera otorga la fexibilidad para cmabiar la implementación de RecipesService sin modificar la clase RecipesController, lo cual facilita las pruebas unitarias y la gestión de dependencias.
    - Este enfoque sigue el principio de inverión de dependencias (DIP), donde las dependencias d euna clase se deben invertir, es decir, proporcionadas desde el exterior en lugar de ser creadas internamente por la clase.

``` C#
public RecipesController(RecipesService recipesService)
{
    _recipeService = recipesService;
}
```

3. Uso de _recipeService
    - En los métodos de la clase, se utiliza **_recipeService** para interactuar con la lógica de negocio relacionada con las Recipes. Por ejemplo, en el método **Get** se llama al método **Get** de **_recipeService** para obtener una receta por su ID

``` C#
var recipe = _recipeService.Get(id);
```

- En resumen, la inyección de dependencias mediante la inicialización de un campo privado en el constructor (_recipeService) permite que la clase RecipesController utilice los servicios proporcionados por RecipesService de manera flexible y facilita la gestión de dependencias y las pruebas unitarias.

``` C#
[HttpGet("{id:length(24)}", Name = "GetRecipe")]
```

- id:length(24) especifica que el parámetro de ruta id debe ser exactamente de longitud 24 caracteres. Esto es común en escenarios donde se espera un identificador BSON ObjectId de MongoDB, que suele tener una longitud de 24 caracteres.
- Name = "GetRecipe" Asigna un nombre a la ruta del URI. En este caso, el nombre de la ruta se establece en "GetRecipe". Este nombre de ruta puede ser útil al generar enlaces o URL en la aplicación.
    - Entonces, este atributo [HttpGet] junto con la ruta definida se encarga de asociar la solicitud HTTP GET a este método de acción específico dentro del controlador. El id es un parámetro de ruta que se espera que tenga una longitud específica, y el nombre de la ruta se establece en "GetRecipe". Este nombre de ruta puede ser referenciado en otros lugares del código para generar URLs en función del nombre de la ruta. Por ejemplo, en el método CreateNewRecipe, se utiliza CreatedAtRoute("GetRecipe", ...) para generar la URL de la receta creada.

#### 7. Creación de archivo Startup
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using API.Models;
using API.Services;

namespace mongodb_dotnet_example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RecipesDatabaseSettings>(Configuration.GetSection(nameof(RecipesDatabaseSettings)));

            services.AddSingleton<IRecipesDatabaseSettings>(sp => sp.GetRequiredService<IOptions<RecipesDatabaseSettings>>().Value);

            services.AddSingleton<RecipesService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
```

- Es responsable de configurar y definir la estrutura de la aplicación. Contiene métodos que se llaman durante la inicialización de la aplicación para configurar servicios, middleware y el pipeline de procesamiento de solicitudes HTTP.

#####  1. Constructor
``` C#
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}
```

- Recibe una instancia de IConfiguration que proporciona acceso a las configuraciones de la aplicación. Se almacena en la propiedad Configuration para su uso posterior.

##### 2. Propiedad Configuration
- Proporciona acceso a la configuración de la aplicación. La configuración proviene de varias fuentes como archivos de configuración, variables de entorno, etc.

``` C#
public IConfiguration Configuration { get; }
```

##### 3. ConfigureServices - Método de configuración de servicios

``` C#
public void ConfigureServices(IServiceCollection services)
{
    // Configura el servicio RecipesDatabaseSettings usando la sección del archivo de configuración
    services.Configure<RecipesDatabaseSettings>(Configuration.GetSection(nameof(RecipesDatabaseSettings)));

    // Registra el servicio IRecipesDatabaseSettings con su implementación como RecipesDatabaseSettings
    services.AddSingleton<IRecipesDatabaseSettings>(sp => sp.GetRequiredService<IOptions<RecipesDatabaseSettings>>().Value);

    // Registra el servicio RecipesService como Singleton
    services.AddSingleton<RecipesService>();

    // Añade controladores de API
    services.AddControllers();

    // Configura Swagger para la generación de documentación de la API
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
    });
}
```

- En este método se configurar los servicios que se usan en la app. Esto incluye la configuración de la base de datos, la configuración de Swagger para documentación de la API y la configuración de servicios como **RecipeService**.

##### 4. Configure - Método de configuración de la canalización de solicitud HTTP.
``` C#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Configura la página de excepciones en el entorno de desarrollo
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    // Configura Swagger y UI para la documentación de la API
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));

    // Configura la redirección HTTPS
    app.UseHttpsRedirection();

    // Configura el enrutamiento de solicitudes
    app.UseRouting();

    // Configura la autorización
    app.UseAuthorization();

    // Configura el manejo de solicitudes HTTP
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

- En este método, se configura la canalización de procesamiento de solicitudes HTTP. Se establece el manejo de excepciones en el entorno de desarrollo, se configura Swagger y su interfaz de usuario para la documentación de la API, se habilita la redirección HTTPS, se configura el enrutamiento de solicitudes, se establece la autorización y se mapean los controladores de la API.
En resumen, el archivo Startup.cs juega un papel crucial en la configuración y estructura de una aplicación ASP.NET Core, definiendo cómo se deben configurar los servicios, cómo se debe procesar cada solicitud HTTP y cómo se deben gestionar las excepciones.

#### 8. Program.cs
-  El archivo Program.cs es el punto de entrada de una aplicación ASP.NET Core. 

##### 1. Método Main
``` C#
public static void Main(string[] args)
{
    CreateHostBuilder(args).Build().Run();
}

```

- El método Main es el punto de entrada de la aplicación. Se encarga de configurar y construir el host de la aplicación utilizando el método CreateHostBuilder, y luego ejecuta el host con el método Run.

##### 2. Método CreateHostBuilder
``` C#
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
```

- El método CreateHostBuilder es responsable de configurar el host de la aplicación. Utiliza Host.CreateDefaultBuilder(args) para crear un host predeterminado con una configuración básica y común. Luego, utiliza .ConfigureWebHostDefaults para configurar el host web.

##### 3. Configuración del HostWeb
``` C#
webBuilder.UseStartup<Startup>();
```
- En esta línea, se especifica que la clase Startup se utilizará para configurar y construir la aplicación web. La clase Startup contiene métodos como ConfigureServices y Configure que configuran servicios y middleware para la aplicación.
- En resumen, el archivo Program.cs establece el punto de entrada de la aplicación y configura el host de la aplicación utilizando CreateHostBuilder. El host se configura para utilizar Startup para la configuración del host web, lo que significa que la configuración específica de la aplicación web se definirá en la clase Startup. Esto sigue la convención de configuración por defecto de ASP.NET Core.

#### 9. DotNevEnv
1. Se instala el paquete desde la terminal ubicada en el root del proyecto.

``` bash
dotnet add package DotNetEnv
```

2. Se crea el archivo .env en el root del proyecto.
3. Se carga DotNetEnv en Program.cs

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace mongodb_dotnet_example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            DotNetEnv.Env.Load();
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
```

4. Se realiza la inyección de dependencia en Startup para poder pasar las variables de entorno a RecipesDatabaseSettings.

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using API.Models;
using API.Services;

namespace mongodb_dotnet_example
{
    public class Startup
    {

        private readonly string USER = string.Empty;
        private readonly string PWD = string.Empty;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            USER = configuration["DB_USER"] ?? string.Empty;
            PWD = configuration["DB_PASSWORD"] ?? string.Empty;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.Configure<RecipesDatabaseSettings>(settings =>
            {
                settings.RecipesCollectionName = Configuration["RecipesDatabaseSettings:RecipesCollectionName"] ?? string.Empty;
                settings.DatabaseName = Configuration["RecipesDatabaseSettings:DatabaseName"] ?? string.Empty;
            });

            services.AddSingleton<IRecipesDatabaseSettings>(sp =>
                new RecipesDatabaseSettings(USER, PWD)
            );

            services.AddSingleton<RecipesService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
```

- Se colocan kas variables USER y PWD a nivel de clase, haciéndolas campos privados de la clase Startup para que estén disponibles en todo ámbito de la clase.
##### Explicación
``` C#
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<RecipesDatabaseSettings>(settings =>
    {
        settings.RecipesCollectionName = Configuration["RecipesDatabaseSettings:RecipesCollectionName"] ?? string.Empty;
        settings.DatabaseName = Configuration["RecipesDatabaseSettings:DatabaseName"] ?? string.Empty;
    });

    services.AddSingleton<IRecipesDatabaseSettings>(sp =>
        new RecipesDatabaseSettings(USER, PWD)
    );
}

```
- Configuración de RecipesDatabaseSettings.
    - Se usa el método Configure<TOptions> para configurar la clase de RecipesDatabaseSettings.
    - Establece los valores de las propiedades RecipesCollectionName y DatabaseName de la clase según los valores presentes en la configuración de la aplicaicón **Configuration**. Si los valores no están presentes en la configuración, se utilizan valores predeterminados.
- Registro de IRecipesDatabaseSettings como servicio Singleton.
    - Se registra la interfaz IRecipesDatabaseSettings junto con us implementación RecipesDatabaseSettings como un servicio singleton en el contenedor de servicios. Esto significa que habrá una única instancia de RecipesDatabaseSettings que será compartida por todos los componentes que la soliciten durante el tiempo de vida de la aplicación.
    - SP dentor de la expresión lambda es el proveedor de servicios que ASP.NET Core proporciona automáticamente. Se utiliza para reoslver dependencias al crear la instancia de RecipesDatabaseSettings.
- AddSingleton
    - AddSingleton es un método de la clase IServiceCollection en ASP.NET Core que se utiliza para registrar servicios en el contenedor de servicios con un tiempo de vida de singleton. Singleton significa que solo habrá una instancia del servicio durante toda la vida de la aplicación y esta instancia será compartida por todos los componentes que lo soliciten.
    - La signature es:
``` C#
public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, TService implementationInstance)
```

##### Ejemplo Singleton
``` C#
services.AddSingleton<IMyService, MyService>();

```

En este ejemplo, se está registrando MyService como un servicio de tipo IMyService con un tiempo de vida de singleton. Cuando se solicita IMyService, se obtendrá siempre la misma instancia de MyService durante toda la duración de la aplicación.

En tu código específico:

``` C#
services.AddSingleton<IRecipesDatabaseSettings>(sp =>
    new RecipesDatabaseSettings(USER, PWD)
);
```

- Se está registrando RecipesDatabaseSettings como un servicio de tipo IRecipesDatabaseSettings. La instancia de RecipesDatabaseSettings se crea utilizando una expresión lambda que toma el proveedor de servicios (sp) como argumento. Esto permite realizar alguna lógica personalizada durante la creación de la instancia, como en este caso, proporcionar los valores USER y PWD durante la construcción de la instancia.


5. Se modifica la clase RecipesDatabaseSettings, en donde se define un constructor para que tome USER y PWD como parámetros.

``` C#
namespace API.Models
{
    public class RecipesDatabaseSettings : IRecipesDatabaseSettings
    {
        public string RecipesCollectionName { get; set; } = "RecipesCollection";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } = "RecipesDB";

        public RecipesDatabaseSettings(string user, string pwd)
        {
            ConnectionString = $"mongodb+srv://{user}:{pwd}@cluster0.bcp5lex.mongodb.net/RecipesDB?retryWrites=true&w=majority";
        }
    }
    public interface IRecipesDatabaseSettings
    {
        string RecipesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
```

### Validating HTTP Request Payloads
- Se tienen built-in validation attributes dentro de .NET.

<img src='ASP\FinShark\ImagenesC\AttValidation.png'></img>

- Se tiene Problem Details for HTTP APIs, en RFC 7807.
    - Define una forma uniforme para reportar errores de vuelta al cliente.
- Para su uso se debe usar:

``` C#
using System.ComponentModel.DataAnnotations
```

- Los modelos para Recipes e Ingredients lucen así ahora:

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public record Ingredient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        [Required]
        public string Name { get; init; }
        public string Amount { get; init; }
        public string Unit { get; init }
    }
}
```

``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public record Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        
        [Required]
        [BsonElement("Title")]
        public string Title { get; init; }
        public string Description { get; init; }
        public IEnumerable<string> Directions { get; init; }
        public IEnumerable<string> Tags { get; init; }

        [Required]
        public IEnumerable<string> Ingredients { get; init; }
        public DateTime Updated { get; init; }
    }
}
```

### Updating data with JSON Patch
- Se tienen dos métodos HTTP para actualizar recursos:
    - Put
        - Updates are issued by replacing entire resources
        - En estas peticione usualmente se tiene en el body todo el recurso.
    - Patch
        - Permite actualizar propiedades individuales en los recursos.
        - En el Body se contienen las propiedades individuales y la operación update asociada.
        - En RFC 6902 se define how to issue updates to resources inside of our web APIs.
- JSON Patch define operaciones como:   
    - add, remove, replace, move, copy y test.

<img src='ASP\FinShark\ImagenesC\JSONPatch.png'></img>

- Se deben isntalar las siguientes NuGet Packages.
    - Microsoft.AspNetCore.JsonPatch
    - Microsoft.AspNetCore.Mvc.NewtonsoftJson.
    - Swashbuckle.AspNetCore.Newtonsoft
        - Se necesita para que JSON Patch support works inside of Swagger UI.
- En Startup.cs, en la parte de ConfigureService se agrega la siguiente parte:

``` C#
services.AddControllers().AddNewtonsoftJson();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "mongodb_dotnet_example", Version = "v1" });
}).AddSwaggerGenNewtonsoftSupport();
```

- Finalmente, en el controlador de Recipes se definen los métodos de Update

https://code-maze.com/getting-started-aspnetcore-mongodb/