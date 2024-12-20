using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesService.Api.Models;

public class Note
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("PatId")]
    public int PatId { get; set; }

    [BsonElement("Patient")]
    public string Patient { get; set; } = null!;

    [BsonElement("NoteContent")]
    public string NoteContent { get; set; } = null!;
}
