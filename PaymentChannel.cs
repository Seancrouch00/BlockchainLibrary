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

namespace BlockchainLibrary
{
    public class PaymentChannel
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposit { get; set; }

        public PaymentChannel(string fromAddress, string toAddress, decimal deposit)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Deposit = deposit;
            Balance = deposit;
        }

        public void AddPayment(decimal amount)
        {
            Balance -= amount;
        }

        public void CloseChannel()
        {
            // Settle the final balance on the blockchain
            Console.WriteLine($"Payment channel closed. Final balance: {Balance}");
        }
    }
}
