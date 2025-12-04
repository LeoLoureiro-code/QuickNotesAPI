using Microsoft.EntityFrameworkCore;
using QuickNotes.DataAccess.EF.Context;
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

        private readonly QuickNotesContext _context;

        public NoteRepository(QuickNotesContext context)
        {
            _context = context;
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

        public async Task<Note> GetNoteById(uint noteId, uint userId)
        {
            var note = await _context.Notes
                .SingleOrDefaultAsync(n => n.NoteId == noteId && n.UserId == userId);

            if (note == null)
            {
                throw new KeyNotFoundException("The note does not exist.");
            }

            return note;
        }


        public async Task<IEnumerable<Note>> SearchNotes(uint userId, string query)
        {
            var lowerQuery = query.ToLower();

            return await _context.Notes
                .Where(n => n.UserId == userId &&
                    (n.NoteContent.ToLower().Contains(lowerQuery) ||
                     n.NoteTitle.ToLower().Contains(lowerQuery)))
                .ToListAsync();
        }

        public async Task<Note> UpdateNote(uint id, string noteTitle, string noteContent)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.NoteId == id);

            if (note == null)
            {
                throw new KeyNotFoundException("The note does not exist.");
            }

            note.NoteTitle = noteTitle;
            note.NoteContent = noteContent;

            await _context.SaveChangesAsync();
            return note;
        }


        public async Task<bool> DeleteNote(int noteId, int userId)
        {
            var existingNote = await _context.Notes
                .SingleOrDefaultAsync(n => n.NoteId == noteId && n.UserId == userId);

            if (existingNote == null)
            {
                return false;
            }
               

            _context.Notes.Remove(existingNote);
            await _context.SaveChangesAsync();

            return true;
        }



    }
}
