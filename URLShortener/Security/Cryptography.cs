using System.Security.Cryptography;
using System.Text;

namespace URLShortener.Security
{
    public class Cryptography
    {
        public static string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return BitConverter.ToString(salt).Replace("-", "").ToLower();
        }

        public static string HashPassword(string password, string salt)
        {
            using var sha512 = SHA512.Create();
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password + salt);
            byte[] hash = sha512.ComputeHash(passwordBytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public static string GenShortUrl(string longUrl, string urlId){
            return HashPassword(longUrl, urlId).Substring(0, 20);
        }

    }
}