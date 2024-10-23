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
    public class BeaconChain
    {
        public List<ShardChain> Shards { get; set; } = new List<ShardChain>();

        public BeaconChain(int shardCount)
        {
            for (int i = 0; i < shardCount; i++)
            {
                Shards.Add(new ShardChain(i));
            }
        }

        public void ProcessShardTransactions()
        {
            foreach (var shard in Shards)
            {
                var transactions = shard.TransactionPool.GetUnconfirmedTransactions();
                Block newBlock = new Block(shard.Blocks.Count, DateTime.Now, transactions, shard.GetLatestBlock()?.Hash);
                shard.AddBlock(newBlock);
                shard.TransactionPool.Clear();
            }
        }

        public void HandleCrossShardTransactions(Transaction transaction, int fromShard, int toShard)
        {
            if (Shards[fromShard].CrossShardTransaction(fromShard.ToString(), toShard.ToString(), transaction))
            {
                Console.WriteLine($"Transaction {transaction} successfully handled across shards {fromShard} -> {toShard}");
            }
        }
    }
}
