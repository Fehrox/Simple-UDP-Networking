﻿using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using UdpNetworking.Network;

namespace UdpNetworking.Views
{
    /// <summary>
    /// Classes derived from View can be syncronised with other 
    /// listening instances on the local network with ServerAssert().
    /// </summary>
    public abstract class View : ISerializable
    {
        /// <summary>
        /// When many instances of a given derived class exist, 
        /// the view id determines which a routed view instance
        /// should be applied to.
        /// </summary>
        public ushort ViewID = 0;
        public IPAddress SenderAddress;

        protected View() {
            RegesterView(0);
        }

        protected View(ushort viewId) {
            RegesterView(viewId);
        }

        /// <summary>
        /// Syncronise this instance of the parent of this derived 
        /// View with other view instances on the network.
        /// </summary>
        public void Sync() {
            Network.Network.Send(this);
        }

        /// <summary>
        /// Subscribes this, and derived classes to information
        /// routed over the network.
        /// </summary>
        /// <param name="viewId"></param>
        private void RegesterView(ushort viewId) {
            // Only sync derived types.
            if (GetType().IsSubclassOf(typeof(View))) {
                ViewRouting.RegisterView(this);
                ViewID = viewId;
            }
        }

        ~View() {
            ViewRouting.UnRegisterView(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("ViewID", ViewID);
            info.AddValue("Address", Network.Network.LanAddress);
        }

        public abstract void RecieveData(View recievedView);

        protected View(SerializationInfo info, StreamingContext context) {
            ViewID = info.GetUInt16("ViewID");
            SenderAddress = (IPAddress)info.GetValue("Address", typeof (IPAddress));
        }

        public override string ToString() {
            return ViewID.ToString();
        }
    }

}
