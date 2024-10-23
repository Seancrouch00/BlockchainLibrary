/*
MIT License

Copyright (c) 2024 Sean Crouch

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainLibrary
{
    public class Transaction
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }  // Added transaction fee
        public List<byte[]> Signatures { get; set; } = new List<byte[]>();  // Multi-signature support

        public Transaction(string fromAddress, string toAddress, decimal amount, decimal fee = 0)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
            Fee = fee;
        }

        public void SignTransaction(RSAParameters privateKey)
        {
            if (FromAddress == null)
            {
                throw new Exception("No from address specified");
            }

            string transactionData = FromAddress + ToAddress + Amount + Fee;
            Signatures.Add(SignData(Encoding.UTF8.GetBytes(transactionData), privateKey));
        }

        public bool IsValid()
        {
            if (FromAddress == null) return true; // For mining rewards, we allow a null from address
            if (Signatures.Count == 0)
            {
                throw new Exception("No signatures in this transaction");
            }

            return VerifySignature();
        }

        private byte[] SignData(byte[] data, RSAParameters privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey);
                return rsa.SignData(data, CryptoConfig.MapNameToOID("SHA256"));
            }
        }

        private bool VerifySignature()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(GetPublicKeyFromAddress(FromAddress));
                string transactionData = FromAddress + ToAddress + Amount + Fee;
                foreach (var signature in Signatures)
                {
                    if (!rsa.VerifyData(Encoding.UTF8.GetBytes(transactionData), CryptoConfig.MapNameToOID("SHA256"), signature))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private RSAParameters GetPublicKeyFromAddress(string address)
        {
            // Mocked for simplicity: Return public key based on the address.
            return new RSAParameters();
        }

        public string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = FromAddress + ToAddress + Amount + Fee;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
