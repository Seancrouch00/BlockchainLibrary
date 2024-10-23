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
    public class DPoS
    {
        private Dictionary<string, int> _votes = new Dictionary<string, int>();
        private List<string> _delegates = new List<string>();

        public void Vote(string delegateAddress, int stake)
        {
            if (_votes.ContainsKey(delegateAddress))
            {
                _votes[delegateAddress] += stake;
            }
            else
            {
                _votes.Add(delegateAddress, stake);
            }
        }

        public List<string> ElectDelegates(int numberOfDelegates)
        {
            _delegates = _votes.OrderByDescending(v => v.Value).Take(numberOfDelegates).Select(v => v.Key).ToList();
            return _delegates;
        }

        public bool IsDelegate(string address)
        {
            return _delegates.Contains(address);
        }

        public void Slashing(string delegateAddress, int penalty)
        {
            if (_votes.ContainsKey(delegateAddress))
            {
                _votes[delegateAddress] = Math.Max(0, _votes[delegateAddress] - penalty);
            }
        }
    }
}
