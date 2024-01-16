using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Models;
using api2.Dtos.Comment;

namespace api2.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        // Se coloca ? ya que se usa FirstOrDefault, el cual puede ser null
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto);
        Task<Comment?> DeleteAsync(int id);
    }
}