using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UdpNetworking.Network
{
    public delegate void AsyncByteCallback(byte[] resultBytes);
    interface INetworkClient {
        void BeginRecieve(AsyncByteCallback requestCallback);
        void Send(byte[] data, int dataLength, IPEndPoint reciever);
        void Close();
    }
}
