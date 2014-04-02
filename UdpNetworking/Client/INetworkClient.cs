using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UdpNetworking.Network
{
    interface INetworkClient {
        void BeginReceive(AsyncCallback requestCallback);
        byte[] EndReceive(IAsyncResult result, ref IPEndPoint sender);
        void Send(byte[] data, int dataLength, IPEndPoint reciever);
        void Close();
    }
}
