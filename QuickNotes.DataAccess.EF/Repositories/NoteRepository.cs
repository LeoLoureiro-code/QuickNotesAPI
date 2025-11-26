using Microsoft.EntityFrameworkCore;
using QuickNotes.DataAccess.EF.Models;
using QuickNotes.DataAccess.EF.Repositories.Interfaces;
using QuickNotesAPI.DataAccess.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNotes.DataAccess.EF.Repositories
{
    public class NoteRepository : INoteRepository
    {

        private readonly QuickNotesContext _context;

        public NoteRepository( QuickNotesContext context)
        {
            context = _context;
        }

        public async Task<Note> CreateNote(Note note)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(u => u.NoteId == note.NoteId);
            if (existingNote != null)
            {
                throw new Exception("A user with this email already exists.");
            }


            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<IEnumerable<Note>> GetAllNotesByUser(uint userId)
        {
            return await _context.Notes.
                Where(x => x.UserId == userId).
                ToListAsync();
        }

        public Task<Note> GetNoteById(uint userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Note>> SearchNotes(uint userId, string query)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(uint id, string noteTitle, string noteContent)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNote(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> UpdateNote(uint id, string noteTitle, string noteContent)
        {
            throw new NotImplementedException();
        }
    }
}
