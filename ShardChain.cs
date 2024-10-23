using System;
using System.Collections.Generic;

namespace BlockchainLibrary
{
    public class ShardChain
    {
        public int ShardId { get; set; }
        public List<Block> Blocks { get; set; } = new List<Block>();
        public TransactionPool TransactionPool { get; set; } = new TransactionPool();

        public ShardChain(int shardId)
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

        public bool CrossShardTransaction(string fromShard, string toShard, Transaction transaction)
        {
            // Simplified logic to perform cross-shard transaction
            // In a real implementation, you'd handle inter-shard communication.
            Console.WriteLine($"Cross-shard transaction from Shard {fromShard} to Shard {toShard}");
            return true;
        }
    }
}
