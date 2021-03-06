﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Threading;
using UdpNetworking.Client;
using UdpNetworking.Messaging;
using UdpNetworking.Views;

namespace UdpNetworking.Network{

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

        public static bool Connected { get {return _client != null;} }
        public const short PORT = 21043;
        public static IPEndPoint HostAddress { get; set; }
        public static IPAddress LanAddress {
            get {
                return Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList
                    .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            }
        }
        public static IThreadProcessor Processor {
            get {
                if (_processor == null)
                    throw new NoThreadProcessorException("Network.Processor not yet set.");
                return _processor;
            } set { _processor = value.InterfaceInstance; }
        }

        public static NetworkStatistics Statistics { get; set; }

        private static IThreadProcessor _processor;
        private static INetworkClient _client;
        private static IMessageContract _contract;

        static Network() {
            _contract = new BinaryFormatterMessageContract();
            Statistics = new NetworkStatistics();
        }

        public static void BroadcastConnect() {
            Connect(IPAddress.Broadcast);
        }

        public static void Connect(IPAddress host) {
            if (Connected) 
                Disconnect();

            _client = new ThreadlessUdpNetworkClient(PORT, Processor);
            HostAddress = new IPEndPoint(host, PORT);
            _client.BeginRecieve(Recieve);

            if(Statistics.Log) Statistics.Connections++;
        }

        /// <summary>
        /// Network consume loop. 
        /// Reads in network data as it becomes available.
        /// </summary>
        /// <param name="result"></param>
        private static void Recieve(byte[] viewBytes) {
            //UnityEngine.Debug.Log("Network.Recieve");
            // Recieve next transmitted data.
            _client.BeginRecieve(Recieve);

            if (Statistics.Log) Statistics.TotalMessagesRecieved++;

            // Restore and process the sent message.
            var restoredView = _contract.UnpackForRecieve(viewBytes) as View;
            if (restoredView == null) return;

            if (Statistics.Log) Statistics.ValidMessagesRecieved++;

            // Don't route our own messages. 
            var loopBack = Equals(restoredView.SenderAddress, LanAddress);
            if (loopBack) return;

            ViewRouting.Route(restoredView);

            if (Statistics.Log) Statistics.ForignMessagesRecieved++;
        }

        /// <summary>
        /// Encodes and sends data to pre-configured destination.
        /// </summary>
        /// <param name="view"></param>
        public static void Send(View view) {
            if (_contract == null || _client == null)
                throw new NoConnectionException("Connect before sending.");

            var messageBytes = _contract.PackForSend(view);
            _client.Send(messageBytes, messageBytes.Length, HostAddress);

            if (Statistics.Log) Statistics.MessagesSent++;
        }

        public static void Disconnect() {
            if(_client == null)
                throw new NoConnectionException("No existing connection to close.");

            if (Statistics.Log) Statistics.Disconnects++;

            _client.Close();
            _client = null;
            HostAddress = null;

#if DEBUG_LOG
            UnityEngine.Debug.Log("Disconnected from " + HostAddress);
#endif
        }

    }

    [Serializable]
    public class NoThreadProcessorException : Exception {

        public NoThreadProcessorException() {}
        public NoThreadProcessorException(string message) : base(message) {}
        public NoThreadProcessorException(string message, Exception inner) : base(message, inner) {}

        protected NoThreadProcessorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }

}
