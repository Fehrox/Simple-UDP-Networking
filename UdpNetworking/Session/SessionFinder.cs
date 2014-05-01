using System.Collections;
using System;

namespace UdpNetworking.Session
{
    class SessionFinder
    {
        SessionAnnouncment _announcmentFinder;

        static Action<int> _onSessionFound;

        public SessionFinder(ref SessionAnnouncment announcment) {
            _announcmentFinder = announcment;
        }

        /// <summary>
        /// Called when SessionAnnouncment returns with the id 
        /// for a hoast advertising a session.
        /// </summary>
        /// <param name="hostID"></param>
        internal static void OnFoundSession(int hostID) {
            _onSessionFound.Invoke(hostID);
        }

        /// <summary>
        /// Creates an SessionAnnouncment to recieve broadcasted session information.
        /// Sets up the callback for when session is found.
        /// </summary>
        /// <param name="onSessionFound"></param>
        internal void Find(Action<int> onSessionFound) {
            _onSessionFound = onSessionFound;
            // Make sure there is an instance of SessionAnnouncment to recieve.
            _announcmentFinder = new SessionAnnouncment();
        }

        internal void StopFinding() {
            //TODO: Impliment a Destroy on the View base calss
            //throw new NotImplementedException();
        }
    }
}