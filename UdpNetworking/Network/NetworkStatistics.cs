using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UdpNetworking.Network
{
    public class NetworkStatistics
    {
        public bool Log { get; set; }

        public int Connections { get; set; }

        public int TotalMessagesRecieved { get; set; }

        public int ValidMessagesRecieved { get; set; }

        public int ForignMessagesRecieved { get; set; }

        public int Disconnects { get; set; }

        public int MessagesSent { get; set; }
    }
}
