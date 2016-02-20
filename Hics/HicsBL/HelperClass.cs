using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HicsBL
{
    public class HelperClass
    {
        public static string GetHash(string text)
        {
            string hash = "";
            SHA512 alg = SHA512.Create();
            byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(text));
            hash = Encoding.UTF8.GetString(result);
            return hash;
        }
    }
}
