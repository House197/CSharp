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