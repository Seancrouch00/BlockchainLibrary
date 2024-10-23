using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary
{
    public class DecentralizedDAO
    {
        private Dictionary<string, int> Votes { get; set; } = new Dictionary<string, int>();
        public List<string> Proposals { get; set; } = new List<string>();

        public void SubmitProposal(string proposal)
        {
            Proposals.Add(proposal);
        }

        public void VoteOnProposal(string proposal, int stake)
        {
            if (Votes.ContainsKey(proposal))
            {
                Votes[proposal] += stake;
            }
            else
            {
                Votes.Add(proposal, stake);
            }
        }

        public string GetWinningProposal()
        {
            return Votes.OrderByDescending(v => v.Value).FirstOrDefault().Key;
        }
    }
}
