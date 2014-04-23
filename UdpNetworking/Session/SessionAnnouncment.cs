using System.Collections;
using System.Net;
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
        protected int HostID;
        protected IPAddress RemoteHostIP;

        private enum AnnounceMode { Search, Broadcast, Passive }
        private AnnounceMode _mode;

        /// <summary>
        /// Sets the SessionAnnouncment to recieve. 
        /// </summary>
        public SessionAnnouncment() {
            _mode = AnnounceMode.Search;
        }

        /// <summary>
        /// Populates the host information to announce.
        /// </summary>
        /// <param name="hostID">ID of the host to announce.</param>
        public SessionAnnouncment(int hostID) {
            // Announce that the given host id has started a new session.
            HostID = hostID;
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
            bool searching = _mode == AnnounceMode.Search,
                 recievedAnnouncment = sessionAnnouncment != null;

            if (searching && recievedAnnouncment) {
                SessionFinder.OnFoundSession(sessionAnnouncment.HostID, sessionAnnouncment.RemoteHostIP);
                // Stop recieving new broadcasts till a new finder is initialized.
                _mode = AnnounceMode.Passive;
            }
        }

        new public void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
            info.AddValue("HostID", HostID);
            info.AddValue("HostAddress", Network.Network.LanAddress);
        }

        protected SessionAnnouncment(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            HostID = info.GetInt32("HostID");
            RemoteHostIP = (IPAddress)info.GetValue("HostAddress", typeof(IPAddress));
        }
    }
}