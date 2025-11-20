using QuickNotesAPI.Services.Interfaces;

namespace QuickNotesAPI.Services
{
    public class AuthService : IAuthService
    {
        public Task<(string AccessToken, string RefreshToken, int userId)> LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task RevokeAndRefreshTokenAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
