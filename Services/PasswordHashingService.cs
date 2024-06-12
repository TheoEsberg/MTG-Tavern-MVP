namespace MTG_Tavern_MVP.Services
{
    public class PasswordHashingService
    {
        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }
    }
}
