using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api2.Data;
using api2.Models;
using api2.Mappers;
using api2.Dtos.Stock;
using api2.Dtos.Comment;
using Microsoft.EntityFrameworkCore;
using api2.Interfaces;

namespace api2.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // Se crea variable privada para el repositorio de comment
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo) // IStockRepository se agrega para poder usar el método que busca si el Stock existe para poder ligarlo con Comment con un Create.
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(comment => comment.ToCommentDto());
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var Comment = await _commentRepo.GetByIdAsync(id); 

            if(Comment == null)
            {
                return NotFound();
            }

            return Ok(Comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto, [FromRoute] int stockId)
        {
            if(!await _stockRepo.StockExists(stockId)){
                return BadRequest("Stock does not exist");
            }
            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateCommentRequestDto updateDto,[FromRoute] int id)
        {
            // Se busca el valor deseado.
            var commentModel = await _commentRepo.UpdateAsync(id, updateDto);
            if(commentModel == null){
                return NotFound();
            }
            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = _commentRepo.DeleteAsync(id);
            if(commentModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}