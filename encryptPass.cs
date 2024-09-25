using System.Security.Cryptography;
using System.Text;

namespace StockManagementAPI
{
    public class encryptPass
    {
        // SHA-256 hash from the input string
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

            // Return the final hashed string in hexadecimal format
            return sb.ToString();
        }
    }
}
