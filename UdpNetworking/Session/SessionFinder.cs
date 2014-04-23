using System.Collections;
using System;
using System.Net;

namespace UdpNetworking.Session
{
    class SessionFinder
    {
        static SessionAnnouncment _announcmentFinder;

        static Action<int, IPAddress> _onSessionFound;

        public SessionFinder(ref SessionAnnouncment announcment) {
            _announcmentFinder = announcment;
        }

        /// <summary>
        /// Called when SessionAnnouncment returns with the id 
        /// for a hoast advertising a session.
        /// </summary>
        /// <param name="hostID"></param>
        /// <param name="ip"> </param>
        internal static void OnFoundSession(int hostID, IPAddress ip) {
            StopFinding();
            _onSessionFound.Invoke(hostID, ip);
        }

        /// <summary>
        /// Creates an SessionAnnouncment to recieve broadcasted session information.
        /// Sets up the callback for when session is found.
        /// </summary>
        /// <param name="onSessionFound"></param>
        internal void Find(Action<int, IPAddress> onSessionFound) {
            _onSessionFound = onSessionFound;
            // Make sure there is an instance of SessionAnnouncment to recieve.
            _announcmentFinder = new SessionAnnouncment();
        }

        internal static void StopFinding() {
            //TODO: Impliment a Destroy on the View base calss
            _announcmentFinder = null;
        }
    }
}