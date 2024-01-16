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

       public async Task<Comment> CreateAsync(Comment commentModel)
       {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
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

       public async Task<Comment?> GetByIdAsync(int id)
       {
            return await _context.Comments.FindAsync(id);
       }

       public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto)
       {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(commentModel == null) 
            {
                return null;
            }

            commentModel.Title = commentDto.Title;
            commentModel.Content = commentDto.Content;

            await _context.SaveChangesAsync();

            return commentModel;

       }
    }
}
