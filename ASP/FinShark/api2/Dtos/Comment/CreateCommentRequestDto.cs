using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Para definir data validation

namespace api2.Dtos.Comment
{
    public class CreateCommentRequestDto 
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must have more than 5 characters.")]
        [MaxLength(280, ErrorMessage = "Content must be less than 280 characters.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Title must have more than 5 characters.")]
        [MaxLength(280, ErrorMessage = "Content must be less than 280 characters.")]
        public string Content { get; set; } = string.Empty;
        //public int? StockId { get; set; } El Stock Id va a venir como parámetro, no en el body
    }
}