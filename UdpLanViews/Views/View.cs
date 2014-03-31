using System.Net;
using System.Runtime.Serialization;
using UdpLanViews.Network;

namespace UdpLanViews.Views
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

        /// <summary>
        /// Syncronise this instance of the parent of this derived 
        /// View with other view instances on the network.
        /// </summary>
        public void ServerAssert() {
            NetworkSend.Send(this);
        }

        protected View() {
            ViewRouting.RegisterView(this);
        }

        ~View() {
            ViewRouting.UnRegisterView(this);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("ViewID", ViewID, typeof(ushort));
        }

        protected View(SerializationInfo info, StreamingContext context) {
            ViewID = info.GetUInt16("ViewID");
        }
    }

}
