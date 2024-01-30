using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Models;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public class RecipesController : ControllerBase
    {
        private readonly RecipesService _recipeService;

        public RecipesController(RecipesService recipesService)
        {
            _recipeService = recipesService;
        }

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
    
        [HttpGet("{id:length(24)}", Name = "GetRecipe")]
        public ActionResult<Recipe> Get(string id)
        {
            var recipe = _recipeService.Get(id);

            if(recipe == null)
            {
                return NotFound();
            }
            
            return recipe;
        }

        [HttpPost]
        public ActionResult<Recipe> CreateNewRecipe([FromBody] Recipe newRecipe)
        {
            bool badThingsHappened = false;
            if (badThingsHappened)
                return BadRequest();
            
            //_recipeService.Create(newRecipe);
            


            Recipe newRecipeTest = new Recipe()
            {
               Title="RecipeTestFull4"
            };

            _recipeService.Create(newRecipeTest);
            
            return CreatedAtRoute("GetRecipe", new { id = newRecipeTest.Id.ToString() }, newRecipe);
        }

        [HttpDelete("{id}")] // api/recipes/a23
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappened = false;

            if (badThingsHappened)
                return BadRequest();
            return NoContent();
        }

        // Métodos Update con JsonPatch
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateRecipe(string id, JsonPatchDocument<Recipe> recipeUpdates)
        {
            var recipe = await _recipeService.GetRecipeById(id);

            if(recipe == null)
                return NotFound();
            
            recipeUpdates.ApplyTo(recipe);
            await _recipeService.UpdateRecipe(recipe);

            return NoContent();
        }
    }
}