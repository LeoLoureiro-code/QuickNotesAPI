using QuickNotes.DataAccess.EF.Models;
using QuickNotesAPI.DTO.UserDTO;
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

        Task<User> GetUserByName(string username);

        Task<User> CreateUser(UserDTO Email);

        Task<User> UpdateUser(int id, string email, string password, string role);

        Task DeleteUser(int id);
    }
}
