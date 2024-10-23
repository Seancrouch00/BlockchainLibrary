using System;
using System.Collections.Generic;

namespace BlockchainLibrary
{
    public class MultiSigWallet
    {
        public List<string> PublicKeys { get; set; }
        public int RequiredSignatures { get; set; }

        public MultiSigWallet(List<string> publicKeys, int requiredSignatures)
        {
            PublicKeys = publicKeys;
            RequiredSignatures = requiredSignatures;
        }

        public bool AuthorizeTransaction(List<string> signatures)
        {
            int validSignatures = 0;

            foreach (var signature in signatures)
            {
                if (PublicKeys.Contains(signature))
                {
                    validSignatures++;
                }
            }

            return validSignatures >= RequiredSignatures;
        }
    }
}
