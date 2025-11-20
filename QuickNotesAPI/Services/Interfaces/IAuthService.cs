namespace QuickNotesAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(string AccessToken, string RefreshToken, int userId)> LoginAsync(string username, string password);


        Task RevokeAndRefreshTokenAsync(string email, string password);

    }
}
