using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UdpNetworking.Network;

namespace UdpNetworking.Client
{
    class UdpNetworkClient : INetworkClient
    {
        private UdpClient _client;

        public UdpNetworkClient(int port) {
            _client = new UdpClient(port);
        }

        public void Send(byte[] data, int dataLength, System.Net.IPEndPoint reciever) {
            _client.Send(data, dataLength, reciever);
        }

        public void BeginReceive(AsyncCallback requestCallback) {
            _client.BeginReceive(requestCallback, null);
        }

        public void Close() {
            _client.Close();
        }

        public byte[] EndReceive(IAsyncResult result, ref System.Net.IPEndPoint sender) {
            return _client.EndReceive(result, ref sender);
        }
    }
}
