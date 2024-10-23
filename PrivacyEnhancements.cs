using System;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainLibrary
{
    public class PrivacyEnhancements
    {
        public static string GenerateStealthAddress(string publicKey, int nonce)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string data = publicKey + nonce.ToString();
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public static bool VerifyZKP(string proof, string originalData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalData));
                string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return proof == hashString;
            }
        }
    }
}
