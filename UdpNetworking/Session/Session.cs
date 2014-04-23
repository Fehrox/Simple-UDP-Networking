using System;
using System.Net;
using Networking = UdpNetworking.Network;

namespace UdpNetworking.Session
{
    public static class Session
    {
        /// <summary>
        /// Gateway through which announcments are sent and recieved.
        /// </summary>
        static SessionAnnouncment _announcment;
        static SessionFinder _sessionFinder;
        static SessionAnnouncer _sessionAnnouncer;

        /// <summary>
        /// Finds a match being announced on UDP.
        /// </summary>
        /// <param name="onMatchFound"></param>
        public static void Find(Action<int, IPAddress> onMatchFound) {
            Network.Network.BroadcastConnect();
            Stop();
            if (_sessionFinder == null)
                _sessionFinder = new SessionFinder(ref _announcment);
            _sessionFinder.Find(onMatchFound);
        }

        /// <summary>
        /// Announces a match over UDP.
        /// </summary>
        /// <param name="hostId"></param>
        public static void Announce(int hostId = 1) {
            Network.Network.BroadcastConnect();
            Stop();
            if (_sessionAnnouncer == null)
                _sessionAnnouncer = new SessionAnnouncer(ref _announcment);
            _sessionAnnouncer.Announce(hostId);
        }


        public static void Stop() {
            if (_sessionAnnouncer != null)
                _sessionAnnouncer.StopBroadcast();
            if (_sessionFinder != null)
                SessionFinder.StopFinding();
        }

    }
}
