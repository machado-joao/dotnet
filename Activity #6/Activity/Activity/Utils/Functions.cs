using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Activity.Utils
{
    public class Functions
    {
        public static string HashText(string text, string hashType)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create(hashType);
            if (algorithm == null)
            {
                throw new ArgumentException("This hash type doesn\'t exist.", "hashType");
            }
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            return Convert.ToBase64String(hash);
        }
    }
}
