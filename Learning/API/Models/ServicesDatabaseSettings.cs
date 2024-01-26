namespace API.Models
{
    public class RecipesDatabaseSettings : IRecipesDatabaseSettings
    {
        public string RecipesCollectionName { get; set; } = "RecipesCollection";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } = "RecipesDB";

        public RecipesDatabaseSettings(string user, string pwd)
        {
            ConnectionString = $"mongodb+srv://{user}:{pwd}@cluster0.bcp5lex.mongodb.net/RecipesDB?retryWrites=true&w=majority";
        }
    }
    public interface IRecipesDatabaseSettings
    {
        string RecipesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}