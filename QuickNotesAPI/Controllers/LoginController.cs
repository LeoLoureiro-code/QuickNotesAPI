using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuickNotes.DataAccess.EF.Models;
using QuickNotes.DataAccess.EF.Repositories.Interfaces;
using QuickNotesAPI.DTO.Auth;
using QuickNotesAPI.DTO.UserDTO;
using QuickNotesAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuickNotesAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _config;

        public LoginController(IUserRepository userRepository, IPasswordService passwordService, IJWTService jWTService, IConfiguration config)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jWTService;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            var user = new User
            {
                UserEmail = dto.Email,
                UserPassword = _passwordService.HashPassword(dto.Password),
                UserRole = "User"
            };

            await _userRepository.CreateUser(user);

            return Ok(new { message = "User created" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _userRepository.GetUserByEmail(dto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            if (!_passwordService.VerifyPassword(dto.Password, user.UserPassword))
            {
                return Unauthorized("Invalid email or password");
            }
                
            var accessToken = _jwtService.GenerateToken(user.UserId, user.UserEmail, user.UserRole ?? "User");
            var refreshToken = _jwtService.GenerateRefreshToken(user.UserId, user.UserEmail, user.UserRole ?? "User");

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshDTO dto)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var jwt = _config.GetSection("Jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

                handler.ValidateToken(dto.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = key
                }, out SecurityToken validatedToken);

                var token = validatedToken as JwtSecurityToken;

                var userId = uint.Parse(token!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var email = token.Claims.First(c => c.Type == ClaimTypes.Email).Value;
                var role = token.Claims.First(c => c.Type == ClaimTypes.Role).Value;

                var newAccess = _jwtService.GenerateToken(userId, email, role);

                return Ok(new { Token = newAccess });
            }
            catch
            {
                return Unauthorized("Invalid refresh token");
            }
        }

    }
}
