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