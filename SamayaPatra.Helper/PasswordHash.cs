using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SamayaPatra.Helper
{
    public static class PasswordHash
    {
        static string SecretKey => "P@w1SXC!@#$";

        public static string ToSHA512(this string password)
        {
            // Create a SHA512   
            using (SHA512 sha512Hash = SHA512.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(SecretKey, password)));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
