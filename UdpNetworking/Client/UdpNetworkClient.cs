using System;
using System.Net;
using System.Net.Sockets;
using UdpNetworking.Network;

namespace UdpNetworking.Client
{
    public class UdpNetworkClient : INetworkClient
    {
        private readonly int _port;
        private UdpClient _client;

        public UdpNetworkClient() {}

        public UdpNetworkClient(int port) {
            _client = new UdpClient(port);
            _port = port;
        }

        /// <summary>
        /// Send data through client over the network.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLength"></param>
        /// <param name="reciever"></param>
        public void Send(byte[] data, int dataLength, IPEndPoint reciever) {
            _client.Send(data, dataLength, reciever);
        }

        /// <summary>
        /// Recieve data, and direct response to given callback.
        /// </summary>
        /// <param name="requestCallback">Method to invoked when message recieved.</param>
        public virtual void BeginRecieve(AsyncByteCallback requestCallback) {
            _client.BeginReceive(result => ProcessRecieve(requestCallback, result), _client);
        }

        /// <summary>
        /// Extracts the bytes from the UDP response and invokes the callback.
        /// </summary>
        /// <param name="requestCallback"></param>
        /// <param name="result"></param>
        public void ProcessRecieve(AsyncByteCallback requestCallback, IAsyncResult result) {
            var sender = new IPEndPoint(IPAddress.Any, 0);
            var listner = (UdpClient)result.AsyncState;
            var recievedBytes = listner.EndReceive(result, ref sender);
            requestCallback.Invoke(recievedBytes);
        }

        /// <summary>
        /// Close the client connection.
        /// </summary>
        public void Close() {
            _client.Close();
        }

        ~UdpNetworkClient() {
            _client.Close();
        }

    }

    public class AlreadyConnectedException : Exception {
        public AlreadyConnectedException() {}
        public AlreadyConnectedException(string message) : base(message) {}
        public AlreadyConnectedException(string message, Exception inner) : base(message, inner) {}
    }

    public class NoConnectionException : Exception{
        public NoConnectionException() { }
        public NoConnectionException(string message) : base(message) { }
        public NoConnectionException(string message, Exception inner) : base(message, inner) { }
    }
}
