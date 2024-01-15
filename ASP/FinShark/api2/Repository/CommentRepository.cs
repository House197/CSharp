using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Interfaces;
using api2.Models;
using Microsoft.EntityFrameworkCore;
using api2.Data;
using api2.Dtos.Stock;
using api2.Dtos.Comment;

namespace api2.Repository
{
    public class CommentRepository : ICommentRepository 
    {
                // Dependency Injection. Constructor.
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

       public async Task<List<Comment>> GetAllAsync() 
       {
            return await _context.Comments.ToListAsync();
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
