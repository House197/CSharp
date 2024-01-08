using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Models;
using Microsoft.EntityFrameworkCore;

namespace api2.Data 
{
    public class ApplicationDBContext : DbContext // Se hereda de DbContext, el cual proviene de using Microsoft.EntityFrameworkCore;
    {
        // ctor para crear constructores
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        // Con base es como si se escribiera DbContext(), solo que hacerlo directamente en el constructor no es posible.
        {
            
        }

        public DbSet<Stock> Stock { get; set; }// Se utiliza para recuperar los datos en base de datos.
        // DbSet Va a retornar los datos en la forma que se desee. Se habla de Deffered execution luego.
        // El nombre despues de <> debe coincidir con el nombre en la base de datos: dbo.Stock

        public DbSet<Comment> Comments { get; set; }
    }
}