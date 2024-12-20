using Microsoft.AspNetCore.Mvc;
using NotesService.Api.Models;
using NotesService.Api.Services;

namespace NotesService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly NotesManagementService _notesService;

    public NotesController(NotesManagementService notesService)
    {
        _notesService = notesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Note>>> Get() =>
        await _notesService.GetAllNotesAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Note>> Get(string id)
    {
        var note = await _notesService.GetNoteByIdAsync(id);

        if (note is null)
        {
            return NotFound();
        }

        return note;
    }

    [HttpGet("patient/{patId}")]
    public async Task<ActionResult<List<Note>>> GetNotesByPatient(int patId)
    {
        var notes = await _notesService.GetNotesByPatientAsync(patId);

        if (notes is null || !notes.Any())
        {
            return NotFound($"No notes found for patient with ID {patId}.");
        }

        return Ok(notes);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Note newNote)
    {
        await _notesService.AddNoteAsync(newNote);

        return CreatedAtAction(nameof(Get), new { id = newNote.Id }, newNote);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Note updatedNote)
    {
        var existingNote = await _notesService.GetNoteByIdAsync(id);

        if (existingNote is null)
        {
            return NotFound();
        }

        updatedNote.Id = existingNote.Id;

        await _notesService.UpdateNoteAsync(id, updatedNote);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingNote = await _notesService.GetNoteByIdAsync(id);

        if (existingNote is null)
        {
            return NotFound();
        }

        await _notesService.DeleteNoteAsync(id);

        return NoContent();
    }
}
