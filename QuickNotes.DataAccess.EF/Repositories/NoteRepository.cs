using QuickNotes.DataAccess.EF.Models;
using QuickNotes.DataAccess.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNotes.DataAccess.EF.Repositories
{
    public class NoteRepository : INoteRepository
    {
        public Task<User> CreateNote(Note note)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNote(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Note>> GetAllNotesByUser()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetNoteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(uint id, string noteTitle, string noteContent)
        {
            throw new NotImplementedException();
        }
    }
}
