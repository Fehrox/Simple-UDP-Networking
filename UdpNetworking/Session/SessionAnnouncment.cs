using System.Collections;
using System.Runtime.Serialization;
using UdpNetworking.Views;

namespace UdpNetworking.Session
{
    /// <summary>
    /// Gateway for network announcments/data. 
    /// </summary>
    [System.Serializable]
    class SessionAnnouncment : View, ISerializable
    {
        // The id for the host announcing a session.
        protected int _hostID;

        private enum AnnounceMode { Search, Broadcast }
        private AnnounceMode _mode;

        /// <summary>
        /// Sets the SessionAnnouncment to recieve. 
        /// </summary>
        public SessionAnnouncment() : base() {
            _mode = AnnounceMode.Search;
        }

        /// <summary>
        /// Populates the host information to announce.
        /// </summary>
        /// <param name="hostID">ID of the host to announce.</param>
        public SessionAnnouncment(int hostID) {
            // Announce that the given host id has started a new session.
            _hostID = hostID;
            _mode = AnnounceMode.Broadcast;
        }

        /// <summary>
        /// Announces session to networked view instances, with HostID.
        /// </summary>
        public void Announce() {
            Sync();
        }

        /// <summary>
        /// We have recieved a session announcment.
        /// </summary>
        /// <param name="recievedView">Announced session.</param>
        public override void RecieveData(View recievedView) {
            var sessionAnnouncment = recievedView as SessionAnnouncment;
            if (_mode == AnnounceMode.Search && sessionAnnouncment != null)
                SessionFinder.OnFoundSession(sessionAnnouncment._hostID);
        }

        new public void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
            info.AddValue("_hostID", _hostID);
        }

        protected SessionAnnouncment(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            _hostID = info.GetInt32("_hostID");
        }
    }
}