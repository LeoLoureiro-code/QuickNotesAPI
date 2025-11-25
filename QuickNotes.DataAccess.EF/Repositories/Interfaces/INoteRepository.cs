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
        Task<IEnumerable<Note>> GetAllNotesByUser();

        Task<User> GetNoteById(int id);

        Task<User> CreateNote(Note note);

        Task<User> UpdateUser(uint id, string noteTitle, string noteContent);

        Task DeleteNote(int id);
    }
}
