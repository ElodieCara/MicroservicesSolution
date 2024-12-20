using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotesService.Api.Models;

namespace NotesService.Api.Data
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Note> _notesCollection;

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _notesCollection = database.GetCollection<Note>(settings.Value.CollectionName);
        }

        public async Task<List<Note>> GetNotesAsync() =>
            await _notesCollection.Find(_ => true).ToListAsync();

        public async Task<Note> GetNoteAsync(string id) =>
            await _notesCollection.Find(note => note.Id == id).FirstOrDefaultAsync();

        public async Task CreateNoteAsync(Note note) =>
            await _notesCollection.InsertOneAsync(note);

        public async Task UpdateNoteAsync(string id, Note note) =>
            await _notesCollection.ReplaceOneAsync(n => n.Id == id, note);

        public async Task DeleteNoteAsync(string id) =>
            await _notesCollection.DeleteOneAsync(n => n.Id == id);
    }
}
