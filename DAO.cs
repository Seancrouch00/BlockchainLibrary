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

namespace BlockchainLibrary
{
    public class DAO
    {
        private Dictionary<string, int> _votes = new Dictionary<string, int>();
        private List<string> _proposals = new List<string>();

        public void CreateProposal(string proposal)
        {
            _proposals.Add(proposal);
        }

        public void Vote(string proposal, int stake)
        {
            if (_votes.ContainsKey(proposal))
            {
                _votes[proposal] += stake;
            }
            else
            {
                _votes.Add(proposal, stake);
            }
        }

        public string GetWinningProposal()
        {
            return _votes.OrderByDescending(v => v.Value).First().Key;
        }
    }
}
