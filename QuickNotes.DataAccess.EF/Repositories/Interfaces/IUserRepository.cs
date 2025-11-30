using QuickNotes.DataAccess.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNotes.DataAccess.EF.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> GetUserByEmail(string userEmail);

        Task<User> CreateUser(User Email);

        Task<User> UpdateUser(uint id, string email, string password, string role);

        Task DeleteUser(int id);

    }
}
