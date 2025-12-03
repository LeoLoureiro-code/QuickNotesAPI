using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickNotes.DataAccess.EF.Models;
using QuickNotes.DataAccess.EF.Repositories;
using QuickNotes.DataAccess.EF.Repositories.Interfaces;

namespace QuickNotesAPI.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [Authorize]
        [HttpGet("all-notes")]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes(uint userId)
        {
            try
            {
                var notes = await _noteRepository.GetAllNotesByUser(userId);
                if (notes == null || !notes.Any())
                {
                    return NotFound("No notes Found");
                }

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while fetching notes.",
                    statusCode: StatusCodes.Status500InternalServerError);

            }
        }

        [Authorize]
        [HttpGet("find-by-id/{id}")]
        public async Task<ActionResult<Note>> GetNoteById(uint noteId, uint userId)
        {
            try
            {
                var note = await _noteRepository.GetNoteById(noteId, userId);

                if (note == null)
                {
                    return NotFound(
                        new
                        {
                            Message = $"User with ID {noteId} was not found."

                        });
                }
                return Ok(note);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while fetching note.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Note>>> SearchNote(uint userId, string query)
        {
            try
            {
                var notes = await _noteRepository.SearchNotes(userId, query);
                if (notes == null || !notes.Any())
                {
                    return NotFound("No notes Found");
                }

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while fetching notes.",
                    statusCode: StatusCodes.Status500InternalServerError);

            }
        }
    }
}
