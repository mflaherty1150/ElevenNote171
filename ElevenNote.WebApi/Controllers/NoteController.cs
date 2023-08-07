using ElevenNote.Models.Note;
using ElevenNote.Models.Responses;
using ElevenNote.Services.Note;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElevenNote.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    // POST api/Note
    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] NoteCreate request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _noteService.CreateNoteAsync(request);
        if (response is not null)
            return Ok(response);

        return BadRequest(new TextResponse("Could not create note."));
    }

    // GET api/Note
    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        var notes = await _noteService.GetAllNotesAsync();
        return Ok(notes);
    }
}

