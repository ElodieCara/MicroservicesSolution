using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotesService.Api.Models;

namespace NotesService.Api.Services;

public class NotesManagementService
{
    private readonly IMongoCollection<Note> _notesCollection;

    public NotesManagementService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _notesCollection = database.GetCollection<Note>(settings.Value.CollectionName);
    }

    public async Task<List<Note>> GetAllNotesAsync() =>
        await _notesCollection.Find(_ => true).ToListAsync();
    public async Task<List<Note>> GetNotesByPatientAsync(int patId) =>
    await _notesCollection.Find(n => n.PatId == patId).ToListAsync();


    public async Task<Note?> GetNoteByIdAsync(string id) =>
        await _notesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task AddNoteAsync(Note note) =>
        await _notesCollection.InsertOneAsync(note);

    public async Task UpdateNoteAsync(string id, Note note) =>
        await _notesCollection.ReplaceOneAsync(x => x.Id == id, note);

    public async Task DeleteNoteAsync(string id) =>
        await _notesCollection.DeleteOneAsync(x => x.Id == id);
}
