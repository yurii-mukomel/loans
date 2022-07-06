using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace BLL.Helpers
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            //using (var rgnCsp = new RNGCryptoServiceProvider())
            //{
            //    rgnCsp.GetNonZeroBytes(salt);
            //}

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

            return hashed;
        }
    }
}
