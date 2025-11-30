namespace QuickNotesAPI.Services.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(uint userId, string email, string role);

        string GenerateRefreshToken(uint userId, string email, string role);
    }
}
