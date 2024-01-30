using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace API.Services
{
    public class IngredientsService
    {
        private readonly IMongoCollection<Ingredient> _ingredients;

        public IngredientsService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _ingredients = database.GetCollection<Ingredient>(settings.IngredientsCollectionName);
        }

        public Ingredient Get(string id) => _ingredients.Find(ingredient => ingredient.Id == id).FirstOrDefault();

        public Ingredient Create(Ingredient ingredient)
        {
            _ingredients.InsertOne(ingredient);
            return ingredient;
        }
    }
}