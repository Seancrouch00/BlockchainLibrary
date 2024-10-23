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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary
{
    public class P2PNode
    {
        private TcpListener _server;
        private List<TcpClient> _clients = new List<TcpClient>();

        public P2PNode(string ipAddress, int port)
        {
            _server = new TcpListener(IPAddress.Parse(ipAddress), port);
        }

        public void Start()
        {
            _server.Start();
            while (true)
            {
                var client = _server.AcceptTcpClient();
                _clients.Add(client);
                Task.Run(() => HandleClient(client));
            }
        }

        private void HandleClient(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received data: " + receivedData);
            }
        }

        public void BroadcastBlock(Block block)
        {
            foreach (var client in _clients)
            {
                var stream = client.GetStream();
                var blockBytes = Encoding.UTF8.GetBytes(block.ToString());
                stream.Write(blockBytes, 0, blockBytes.Length);
            }
        }

        public void BroadcastTransaction(Transaction transaction)
        {
            foreach (var client in _clients)
            {
                var stream = client.GetStream();
                var txBytes = Encoding.UTF8.GetBytes(transaction.ToString());
                stream.Write(txBytes, 0, txBytes.Length);
            }
        }
    }
}
