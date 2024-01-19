using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Interfaces;
using api2.Models;
using Microsoft.EntityFrameworkCore;
using api2.Data;
using api2.Dtos.Stock;
using api2.Helpers;

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
          // FindAsync no sirve con Include, por eso se usa FirstOrDefaultAsync
            return await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
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
          return await _context.Stock.AnyAsync(stock => stock.Id == id);
       }
    }

}