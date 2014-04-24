using System.Threading;

namespace UdpNetworking.Session
{
    /// <summary>
    /// Notifies other players that there is a session available to join on a given host.
    /// </summary>
    class SessionAnnouncer
    {

        SessionAnnouncment _announcer;

        private bool _broadcast = true;
        private int _hostID;

        // How Often we should broadcase the session.
        private const int BROADCAST_FREQUENCY = 500;

        public SessionAnnouncer(ref SessionAnnouncment _announcment) {
            _announcer = _announcment;
        }

        /// <summary>
        /// Broadcast that a given hostID has a session available to join.
        /// </summary>
        /// <param name="hostID">ID of the host who's session is available.</param>
        public void Announce(int hostID) {
            _hostID = hostID;
            // Run an announcment in the background.
            var broadcast = new Thread(Broadcast);
            broadcast.Start();
        }

        /// <summary>
        /// Stop threads when object is destroyed.
        /// </summary>
        ~SessionAnnouncer() {
            StopBroadcast();
        }

        /// <summary>
        /// Regularly sends a message to all networked SessionAnnouncments.
        /// </summary>
        private void Broadcast() {
            _broadcast = true;
            while (_broadcast ) {
                _announcer = new SessionAnnouncment(_hostID);
                _announcer.Announce();
                // Wait .35 seconds before announcing again.
                Thread.Sleep(BROADCAST_FREQUENCY);
            }
        }

        /// <summary>
        /// Ceace broadcasting threads.
        /// </summary>
        public void StopBroadcast() {
            _broadcast = false;
        }

    }
}