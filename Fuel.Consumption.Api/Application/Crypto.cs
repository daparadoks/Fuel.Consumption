using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Fuel.Consumption.Domain;
using Microsoft.IdentityModel.Tokens;

namespace Fuel.Consumption.Api.Application;

public static class Crypto
{
    private const int SaltSize = 16;
        private const int HashSize = 20;

        public static string GeneratePassword(int length = 12)
        {
            using var cryptRng = new RNGCryptoServiceProvider();
            var tokenBuffer = new byte[length];
            cryptRng.GetBytes(tokenBuffer);
            return Convert.ToBase64String(tokenBuffer);
        }

        public static string HashPassword(string password, int iterations = 10000)
        {
            using var rng = new RNGCryptoServiceProvider();
            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            var base64Hash = Convert.ToBase64String(hashBytes);
            return $"$HASH|V1${iterations}${base64Hash}";
        }

        private static bool IsHashSupported(string hashString) => hashString.Contains("HASH|V1$");

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (!IsHashSupported(hashedPassword))
                throw new HashNotSupportedException();

            var hashString = hashedPassword.Replace("$HASH|V1$", "").Split('$');
            var iterations = int.Parse(hashString[0]);
            var base64Hash = hashString[1];
            var hashBytes = Convert.FromBase64String(base64Hash);
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                    return false;
            }

            return true;
        }

        public static string GenerateJwtToken(User user,
            string issuer,
            string audience,
            string secret)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Username),
                    new(CustomClaimTypes.UserRole, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
}