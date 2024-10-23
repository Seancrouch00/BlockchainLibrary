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
