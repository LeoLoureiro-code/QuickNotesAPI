using QuickNotes.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNotes.DataAccess.EF.Repositories.Interfaces
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotesByUser(uint userId);

        Task<Note> GetNoteById(uint noteId, uint userId);

        Task<IEnumerable<Note>> SearchNotes(uint userId, string query);

        Task<Note> CreateNote(Note note);

        Task<Note> UpdateNote(uint id, string noteTitle, string noteContent);

        Task DeleteNote(int id);
    }
}
