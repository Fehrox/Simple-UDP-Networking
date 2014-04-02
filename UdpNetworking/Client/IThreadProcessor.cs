using System.Collections.Generic;
using UdpNetworking.Network;

namespace UdpNetworking.Client
{
    public interface IThreadProcessor {
        Queue<AsyncThreadCallback> Queue { get; set; }
        void Enqueue(AsyncThreadCallback response);
    }

    public struct AsyncThreadCallback{
        public AsyncByteCallback RequestCallback;
        public byte[] RecievedBytes;
        public AsyncThreadCallback(byte[] recievedBytes, AsyncByteCallback requestCallback) {
            RequestCallback = requestCallback;
            RecievedBytes = recievedBytes;
        }
    }
}
