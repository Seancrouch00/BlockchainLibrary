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
