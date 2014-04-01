using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UdpLanViews.Messaging;
using UdpLanViews.Views;

namespace UdpLanViews.Network
{
    /// <summary>
    /// Handles the recieving and of UDP messages sent 
    /// to this computer over the local network.
    /// </summary>
    class NetworkRecieve
    {
 
        // Address from which to recieve data.
        private IPEndPoint _sender;
        private UdpClient _client;
        private IMessageContract _contract;

        private readonly IPAddress _localAddress;

        /// <summary>
        /// Begins listening for Network communications.
        /// </summary>
        public NetworkRecieve(UdpClient client,  IMessageContract contract, IPAddress sender, short port) {
            _sender = new IPEndPoint(sender ?? IPAddress.Broadcast, port);
            _client = client;
            _localAddress = GetLocalIp();
            _contract = contract;

            //TODO: Investigate UDPState.
            _client.BeginReceive(DataRecieved, null);
        }

        /// <summary>
        /// Close underlying udp connection.
        /// </summary>
        public void Close() {
            _client.Close();
        }

        // Unbind from port.
        ~NetworkRecieve() {
            Close();
        }

        /// <summary>
        /// Network consume loop. 
        /// Reads in network data as it becomes available.
        /// </summary>
        /// <param name="result"></param>
        private void DataRecieved(IAsyncResult result) {

            // Recieve any pending network messages.
            var viewBytes = _client.EndReceive(result, ref _sender);

            // Recieve next transmitted data.
            _client.BeginReceive(DataRecieved, null);

            // Don't route our own messages. 
            var loopBack = _sender.Address.Equals(_localAddress);
            if (loopBack) return;

            // Restore and process the sent message.
            var restoredView = _contract.UnpackForRecieve(viewBytes) as View;
            if (restoredView != null)
                ViewRouting.Route(restoredView);
        }

        private IPAddress GetLocalIp() {
            return Dns.GetHostEntry(Dns.GetHostName())
                      .AddressList
                      .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
