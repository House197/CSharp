using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Dtos.Comment
{
    public class CreateCommentRequestDto 
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        //public int? StockId { get; set; } El Stock Id va a venir como parámetro, no en el body
    }
}