using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api2.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        public string String { get; set; } = string.Empty; // Para evitar null reference errors
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 10000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
    }
}