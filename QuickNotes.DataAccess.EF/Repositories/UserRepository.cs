using QuickNotes.DataAccess.EF.Models;
using QuickNotes.DataAccess.EF.Repositories.Interfaces;
using QuickNotesAPI.DataAccess.EF.Context;
using QuickNotesAPI.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNotes.DataAccess.EF.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly QuickNotesContext _context;
        private readonly IUserRepository _userRepository;

        public UserRepository(QuickNotesContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public Task<User> CreateUser(UserDTO Email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByName(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(int id, string email, string passwordhashed)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
