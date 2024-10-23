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
    public class ProofOfStake
    {
        private Dictionary<string, int> _stakeholders = new Dictionary<string, int>();

        public void RegisterStakeholder(string address, int stake)
        {
            if (_stakeholders.ContainsKey(address))
            {
                _stakeholders[address] += stake;
            }
            else
            {
                _stakeholders.Add(address, stake);
            }
        }

        public string SelectValidator()
        {
            Random random = new Random();
            int totalStake = 0;

            foreach (var stake in _stakeholders.Values)
            {
                totalStake += stake;
            }

            int selection = random.Next(0, totalStake);
            foreach (var stakeholder in _stakeholders)
            {
                if (selection < stakeholder.Value)
                {
                    return stakeholder.Key;
                }
                selection -= stakeholder.Value;
            }

            return null; // This should not happen unless no stakeholders are registered
        }

        public bool ValidateBlock(string validator, Block block)
        {
            // Validator must be a stakeholder with sufficient stake
            return _stakeholders.ContainsKey(validator);
        }
    }
}
