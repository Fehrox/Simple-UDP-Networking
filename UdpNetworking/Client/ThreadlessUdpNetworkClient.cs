using UdpNetworking.Network;

namespace UdpNetworking.Client
{

    public class ThreadlessUdpNetworkClient : UdpNetworkClient
    {
        private IThreadProcessor _threadProcessor;

        public ThreadlessUdpNetworkClient() {}

        /// <summary>
        /// Set up Queuing mechanisms.
        /// </summary>
        /// <param name="port">Port for client connection.</param>
        /// <param name="threadProcessor">Mechanism for handling responses.</param>
        public ThreadlessUdpNetworkClient(int port, IThreadProcessor threadProcessor)
            : base(port) {
            _threadProcessor = threadProcessor;
        }

        /// <summary>
        /// Recieve data, and process it in the main thread.
        /// </summary>
        public override void BeginRecieve(AsyncByteCallback requestCallback) {
            base.BeginRecieve(recievedBytes => QueueForProcessing(recievedBytes, requestCallback));
        }

        /// <summary>
        /// Add the async message to the queue to be processed int the main thread.
        /// </summary>
        /// <param name="requestCallback"></param>
        /// <param name="result"></param>
        private void QueueForProcessing(byte[] recievedBytes, AsyncByteCallback requestCallback) {
            _threadProcessor.Enqueue(new AsyncThreadCallback(recievedBytes, requestCallback));
        }
    }
}
