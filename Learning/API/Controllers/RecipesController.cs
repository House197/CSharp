using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class RecipesController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetRecipes([FromQuery] int count)
        {
            Recipe[] recipes = {
                new() { Title = "Oxtail" },
                new() { Title = "Curry Chicken" },
                new() { Title = "Dumplongs" } 
            };

            return Ok(recipes.Take(count));
        }

        [HttpPost]
        public ActionResult CreateNewRecipe([FromBody] Recipe newRecipe)
        {
            bool badThingsHappened = false;
            if (badThingsHappened)
                return BadRequest();
            
            return Created("", newRecipe);
        }

        [HttpDelete("{id}")] // api/recipes/a23
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return NoContent();
        }
    }
}