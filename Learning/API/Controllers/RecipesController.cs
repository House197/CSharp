using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class RecipesController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetRecipes()
        {
            string[] recipes = {"Pizza", "Dumplings", "Rice"};

            if(recipes.Any())
                return NotFound();
            return Ok(recipes);
        }

        [HttpDelete]
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return NoContent();
        }
    }
}