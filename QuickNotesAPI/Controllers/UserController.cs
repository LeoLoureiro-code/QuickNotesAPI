using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickNotes.DataAccess.EF.Models;
using QuickNotes.DataAccess.EF.Repositories.Interfaces;
using QuickNotesAPI.DTO.Auth;
using QuickNotesAPI.DTO.UserDTO;
using QuickNotesAPI.Services.Interfaces;

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

        [Authorize]
        [HttpGet("all-users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllusers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                if (users == null || !users.Any())
                {
                    return NotFound("No users Found");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while fetching users.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("find-by-id/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);

                if (user == null)
                {
                    return NotFound(
                        new
                        {
                            Message = $"User with ID {id} was not found."

                        });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while fetching user.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("find-by-credentials")]

        public async Task<ActionResult<User>> GetUserByCredentials([FromBody] LoginDTO login)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(login.Email);

                if (existingUser == null)
                {
                    throw new Exception("User not found.");
                }

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return Problem(
                   detail: ex.Message,
                   title: "An error occurred while fetching user.",
                   statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("create-user")]
        public async Task<ActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Email and password are required.");
            }

            string hashed = _passwordService.HashPassword(userDto.Password!);

            var user = new User
            {
                UserEmail = userDto.Email!,
                UserPassword = hashed,
                UserRole = "User"
            };

            var createdUser = await _userRepository.CreateUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
        }


        [Authorize]
        [HttpPut("update-user/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (user.UserId != 0 && user.UserId != id)
                {
                    return BadRequest("User ID in the body does not match URL.");
                }
                    

                if (string.IsNullOrWhiteSpace(user.UserEmail))
                {
                    return BadRequest("Email cannot be empty.");
                }

                var existingUser = await _userRepository.GetUserById(id);
                if (existingUser == null)
                    return NotFound($"User with ID {id} not found.");


                if (!string.IsNullOrWhiteSpace(user.UserPassword) &&
                    user.UserPassword != existingUser.UserPassword)
                {

                    existingUser.UserPassword = _passwordService.HashPassword(user.UserPassword);
                }

                existingUser.UserEmail = user.UserEmail;

                await _userRepository.UpdateUser(existingUser.UserId, existingUser.UserEmail, existingUser.UserPassword, existingUser.UserRole);

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while updating the user.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                await _userRepository.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    title: "An error occurred while deleting the user.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
