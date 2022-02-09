using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ZeppelinWebsite.Helper
{
    public class CryptoService
    {

        private const int SaltSize = 32;

        public static byte[] GenerateSalt()
        {
            using(var rng = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[SaltSize];
                rng.GetBytes(randomNumber);
                return randomNumber;
            }

        }

        public static byte[] ComputeHMAC256(byte[] data, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {

                return hmac.ComputeHash(data);
            }


        }


    }
}