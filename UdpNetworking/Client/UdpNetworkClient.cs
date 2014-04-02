using System;
using System.Net;
using System.Net.Sockets;
using UdpNetworking.Network;

namespace UdpNetworking.Client
{
    class UdpNetworkClient : INetworkClient
    {
        protected UdpClient Client;
        private readonly int _port;

        public UdpNetworkClient(int port) {
            Client = new UdpClient(port);
            _port = port;
        }

        /// <summary>
        /// Send data through client over the network.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLength"></param>
        /// <param name="reciever"></param>
        public void Send(byte[] data, int dataLength, IPEndPoint reciever) {
            Client.Send(data, dataLength, reciever);
        }

        /// <summary>
        /// Recieve data, and direct response to given callback.
        /// </summary>
        /// <param name="requestCallback">Method to invoked when message recieved.</param>
        public virtual void BeginRecieve(AsyncByteCallback requestCallback) {
            Client.BeginReceive(result => ProcessRecieve(requestCallback, result), null);
        }

        /// <summary>
        /// Extracts the bytes from the UDP response and invokes the callback.
        /// </summary>
        /// <param name="requestCallback"></param>
        /// <param name="result"></param>
        public void ProcessRecieve(AsyncByteCallback requestCallback, IAsyncResult result) {
            var sender = new IPEndPoint(IPAddress.Any, _port);
            var recievedBytes = Client.EndReceive(result, ref sender);
            requestCallback.Invoke(recievedBytes);
        }

        /// <summary>
        /// Close the client connection.
        /// </summary>
        public void Close() {
            Client.Close();
        }

        ~UdpNetworkClient() {
            Client.Close();
        }

        // Explicitly define this class in order to extract Endpoint from sender.
        /// <summary>
        /// Represents the UdpState at time of transmission.
        /// </summary>
        public class UdpState {
            public IPEndPoint EndPoint;
            public UdpClient Client;
        }

    }
}
