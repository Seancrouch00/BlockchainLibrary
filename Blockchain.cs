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
using System.Collections.Generic;

namespace BlockchainLibrary
{
    public class Blockchain
    {
        public List<Block> Chain { get; set; }
        public int Difficulty { get; set; }
        public TransactionPool TransactionPool { get; set; }
        public DPoS ConsensusMechanism { get; set; }
        public DAO Governance { get; set; }

        public Blockchain()
        {
            Chain = new List<Block> { CreateGenesisBlock() };
            Difficulty = 2;
            TransactionPool = new TransactionPool();
            ConsensusMechanism = new DPoS();
            Governance = new DAO();
        }

        private Block CreateGenesisBlock()
        {
            return new Block(0, DateTime.Now, new List<Transaction>(), "0");
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block newBlock)
        {
            newBlock.PreviousHash = GetLatestBlock().Hash;
            newBlock.MineBlock(Difficulty);
            Chain.Add(newBlock);
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction.IsValid())
            {
                TransactionPool.AddTransaction(transaction);
            }
        }

        public void ProcessTransactions(string validatorAddress)
        {
            if (!ConsensusMechanism.IsDelegate(validatorAddress))
            {
                throw new Exception("Validator is not an elected delegate.");
            }

            var transactions = TransactionPool.GetUnconfirmedTransactions();
            Block newBlock = new Block(Chain.Count, DateTime.Now, transactions, GetLatestBlock().Hash);
            AddBlock(newBlock);
            TransactionPool.Clear();
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash() || currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
