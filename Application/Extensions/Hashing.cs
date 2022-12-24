
using System.Security.Cryptography;
using System.Text;

namespace Sarafi.Application.Extensions
{
    public static class Hashing
    {
        public static string GetHash(this string text)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.Unicode.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        public static string AddSalt(this string text) => $"{text}{GetSalt(text)}";
        public static string GetSalt(this string a)
        {
            byte[] bytes = new byte[16];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
