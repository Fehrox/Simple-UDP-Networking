using System.Net;
using System.Net.Sockets;
using UdpNetworking.Messaging;
using UdpNetworking.Views;

namespace UdpNetworking.Network
{
    /// <summary>
    /// Handles the sending of messages over the Local Network.
    /// </summary>
    class NetworkSend
    {
        private static IPEndPoint _server;
        private static INetworkClient _client;
        private static IMessageContract _contract;

        /// <summary>
        /// Configures the destination for messages.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="contract"> </param>
        /// <param name="port"></param>
        /// <param name="server"></param>
        public NetworkSend(INetworkClient client, IMessageContract contract, short port, IPAddress server = null) {
            // If no server is provided, assume we are server.
            _server = new IPEndPoint(server ?? IPAddress.Broadcast, port);
            _client = client;
            _contract = contract;
        }

        /// <summary>
        /// Encodes and sends data to pre-configured destination.
        /// </summary>
        /// <param name="view"></param>
        public static void Send(View view) {
            var messageBytes = _contract.PackForSend(view);
            _client.Send(messageBytes, messageBytes.Length, _server);
        }
    }
}
