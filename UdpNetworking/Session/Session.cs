using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public static void Find(Action<int> onMatchFound) {
            if (_sessionAnnouncer != null)
                _sessionAnnouncer.StopBroadcast();
            if (_sessionFinder == null)
                _sessionFinder = new SessionFinder(ref _announcment);
            _sessionFinder.Find(onMatchFound);
        }

        /// <summary>
        /// Announces a match over UDP.
        /// </summary>
        /// <param name="hostId"></param>
        public static void Announce(int hostId) {
            if (_sessionFinder != null)
                _sessionFinder.StopFinding();
            if (_sessionAnnouncer == null)
                _sessionAnnouncer = new SessionAnnouncer(ref _announcment);
            _sessionAnnouncer.Announce(hostId);
        }

    }
}
