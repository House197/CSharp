using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string String { get; set; } = string.Empty; // Para evitar null reference errors
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
