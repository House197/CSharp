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

    }
}