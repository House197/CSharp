namespace API.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string RecipesCollectionName { get; set; } = "RecipesCollection";
        public string IngredientsCollectionName {get; set;} = "IngredientsCollection";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } = "RecipesDB";

        public DatabaseSettings(string user, string pwd)
        {
            ConnectionString = $"mongodb+srv://{user}:{pwd}@cluster0.bcp5lex.mongodb.net/RecipesDB?retryWrites=true&w=majority";
        }
    }
    public interface IDatabaseSettings
    {
        string RecipesCollectionName { get; set; }
        string IngredientsCollectionName{ get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}