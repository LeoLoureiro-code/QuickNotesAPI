using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickNotesAPI.Services.Interfaces;
using QuickNotes.DataAccess.EF.Repositories.Interfaces;

namespace QuickNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public UserController(IUserRepository userRepository, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }
    }
}
