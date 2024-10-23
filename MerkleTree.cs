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

using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainLibrary
{
    public static class MerkleTree
    {
        public static string ComputeMerkleRoot(List<string> transactionHashes)
        {
            if (transactionHashes.Count == 0)
                return string.Empty;

            while (transactionHashes.Count > 1)
            {
                if (transactionHashes.Count % 2 != 0)
                    transactionHashes.Add(transactionHashes[transactionHashes.Count - 1]);

                List<string> newLevel = new List<string>();
                for (int i = 0; i < transactionHashes.Count; i += 2)
                {
                    newLevel.Add(HashPair(transactionHashes[i], transactionHashes[i + 1]));
                }

                transactionHashes = newLevel;
            }

            return transactionHashes[0];
        }

        private static string HashPair(string left, string right)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string combinedHash = left + right;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedHash));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
