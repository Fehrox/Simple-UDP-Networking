using System;
using System.Net;
using System.Threading;

namespace UdpNetworking.Session
{
    public static class Session {

        /// <summary>
        /// The id that uniquely identifies this computer in the session.
        /// </summary>
        public static int UniqueID { get; set; }

        /// <summary>
        /// Gateway through which announcments are sent and recieved.
        /// </summary>
        static SessionAnnouncment _announcment;

        #region Find Sessions

        /// <summary>
        /// Action to perform when a session is returned from a Find call.
        /// </summary>
        static Action<int, IPAddress> _onSessionFound;

        /// <summary>
        /// Finds a match being announced on UDP.
        /// </summary>
        /// <param name="onSessionFound"></param>
        public static void Find(Action<int, IPAddress> onSessionFound) {
            if(_announcing)
                StopAnnouncing();
            Network.Network.BroadcastConnect();
            _onSessionFound = onSessionFound;
            _announcment = new SessionAnnouncment();
        }

        /// <summary>
        /// Called when SessionAnnouncment returns with the id 
        /// for a hoast advertising a session.
        /// </summary>
        /// <param name="hostID"></param>
        /// <param name="ip"> </param>
        internal static void OnFoundSession(int hostID, IPAddress ip) {
            _onSessionFound.Invoke(hostID, ip);
            _announcment = null;
            UnityEngine.Debug.Log("OnFoundSession("+hostID+"," +ip+")");
        }

        #endregion

        #region Announce Sessions

        // How Often we should broadcase the session.
        private const int BROADCAST_FREQUENCY = 500;
        private static bool _announcing = true;
        private static int _hostID;

        /// <summary>
        /// Broadcast that a given hostID has a session available to join.
        /// </summary>
        /// <param name="hostID">ID of the host who's session is available.</param>
        public static void Announce(int hostID) {
            
            // Close existing connections.
            if (Network.Network.Connected)
                Network.Network.Disconnect();
            Network.Network.BroadcastConnect();
            StopAnnouncing();

            // Run an announcment in the background.
            _hostID = hostID;
            var broadcast = new Thread(Broadcast);
            _announcment = new SessionAnnouncment(_hostID);
            broadcast.Start();

            UnityEngine.Debug.Log("Announce");
        }

        /// <summary>
        /// Regularly sends a message to all networked SessionAnnouncments.
        /// </summary>
        private static void Broadcast() {
            _announcing = true;
            while (_announcing) {
               _announcment.Announce();
                // Wait before announcing again.
                Thread.Sleep(BROADCAST_FREQUENCY);
            }
        }

        /// <summary>
        /// Stop boradcasts and receipt of messages.
        /// </summary>
        public static void StopAnnouncing() {
            _announcing = false;
        }

        #endregion

    }
}
