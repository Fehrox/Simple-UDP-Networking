﻿using System.Net;
using System.Net.Sockets;
using UdpLanViews.Messaging;

namespace UdpLanViews.Network{

    /// <summary>
    /// Author:         Sebastiaan Fehr (Seb@TheBinaryMill.com)
    /// Date:           March 2014
    /// Summary:        Allows easy networking over UDP through Network Views.
    /// Description:    By simply extending a class from View, and adding the 
    ///                 information we wanted synced over the network to the
    ///                 serialization functions this system will sync that 
    ///                 data over the network with other instances of the 
    ///                 class with matching view ids.
    /// Note:           When no server is specified, messages are broadcasted
    ///                 over UDP to the entire LAN.
    /// </summary>
    public static class Network {

        const short LISTEN_PORT = 21044;
        private static UdpClient _client;
        private static IMessageContract _contract;
        private static NetworkRecieve _reciever;
        private static NetworkSend _sender;

        public static void Listen(IPAddress sender = null) {
            _client = new UdpClient(LISTEN_PORT);
            _contract = new BinaryFormatterMessageContract();
            _reciever = new NetworkRecieve(_client, _contract, sender, LISTEN_PORT);
            _sender = new NetworkSend(_client, _contract, LISTEN_PORT, sender);
        }

        public static void StopListening() {
            _reciever.Close();
            _client = null;
            _contract = null;
            _reciever = null;
            _sender = null;
        }

    }

}
