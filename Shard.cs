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
    public class Shard
    {
        public int ShardId { get; set; }
        public List<Block> Blocks { get; set; } = new List<Block>();
        public TransactionPool TransactionPool { get; set; } = new TransactionPool();

        public Shard(int shardId)
        {
            ShardId = shardId;
        }

        public void AddBlock(Block block)
        {
            Blocks.Add(block);
        }

        public Block GetLatestBlock()
        {
            return Blocks.Count > 0 ? Blocks[^1] : null;
        }
    }

    public class BeaconChain
    {
        public List<Shard> Shards { get; set; } = new List<Shard>();

        public BeaconChain(int shardCount)
        {
            for (int i = 0; i < shardCount; i++)
            {
                Shards.Add(new Shard(i));
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
    }
}
