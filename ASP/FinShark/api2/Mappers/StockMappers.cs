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