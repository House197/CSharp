using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class RecipesService
    {
        private readonly IMongoCollection<Recipe> _recipes;
        private readonly IMongoCollection<Ingredient> _ingredients;

        public RecipesService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _recipes = database.GetCollection<Recipe>(settings.RecipesCollectionName);
            _ingredients = database.GetCollection<Ingredient>(settings.IngredientsCollectionName);
        }

        public Recipe Get(string id) => _recipes.Find(recipe => recipe.Id == id).FirstOrDefault();

        public Recipe Create(Recipe recipe)
        {
            _recipes.InsertOne(recipe);
            return recipe;
        }
    }
}