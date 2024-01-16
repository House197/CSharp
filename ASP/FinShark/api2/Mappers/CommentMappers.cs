using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api2.Dtos.Comment;
using api2.Models;

namespace api2.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }


        // Método de extensión que retorna Comment. En el argumento el tipo de dato que se usa es CreateCommentRequestDto, y el parámetro tiene el nombre de commentDto.
        // El método retorna un Comment (definido en Model), pero solo se asignan los campos necesarios para la creación.
        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto commentDto, int stockId)
        {
            return new Comment {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId,
            };
        }
    }
}