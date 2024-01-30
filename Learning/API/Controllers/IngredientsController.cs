using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Models;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ingredientService;

        public IngredientsController(IngredientsService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public ActionResult GetIngredients([FromQuery] int count)
        {
            Ingredient[] ingredients = {
                new() { Name = "IngredientGet1" },
                new() { Name = "Cheese" },
                new() { Name = "Potato" } 
            };

            return Ok(ingredients.Take(count));
        }
    
        [HttpGet("{id:length(24)}", Name = "GetIngredient")]
        public ActionResult<Ingredient> Get(string id)
        {
            var ingredient = _ingredientService.Get(id);

            if(ingredient == null)
            {
                return NotFound();
            }
            
            return ingredient;
        }

        [HttpPost]
        public ActionResult<Ingredient> CreateNewRecipe([FromBody] Ingredient newIngredient)
        {
            bool badThingsHappened = false;
            if (badThingsHappened)
                return BadRequest();
            
            //_recipeService.Create(newRecipe);
            


            Ingredient newIngredientTest = new Ingredient()
            {
               Name="Ingredient1"
            };

            _ingredientService.Create(newIngredientTest);
            
            return CreatedAtRoute("GetRecipe", new { id = newIngredientTest.Id.ToString() }, newIngredient);
        }
    }
}