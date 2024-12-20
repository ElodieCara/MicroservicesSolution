namespace NotesService.Api.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!; // Lien de connexion à la base MongoDB
        public string DatabaseName { get; set; } = null!;  // Nom de la base de données
        public string CollectionName { get; set; } = null!;  // Nom de la collection
    }
}
