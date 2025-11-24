using Microsoft.EntityFrameworkCore;
using QuickNotes.DataAccess.EF.Models;
using QuickNotes.DataAccess.EF.Repositories.Interfaces;
using QuickNotesAPI.DataAccess.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuickNotes.DataAccess.EF.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly QuickNotesContext _context;


        public UserRepository(QuickNotesContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);
            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }


            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;


        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<User> GetUserById(int id)
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("Book not found");
            }

            return user;
        }

        public async Task<User> GetUserByName(string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail== userEmail);


            if (user == null)
                throw new Exception("User not found.");

            return user;
        }

        public async Task<User> UpdateUser(uint id, string email, string password, string role)
        {
            User? existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
                throw new Exception("User not found");

            existingUser.UserEmail = email;
            existingUser.UserPassword = password;
            existingUser.UserRole = role;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task DeleteUser(int id)
        {
            User? existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
        }
    }
}
