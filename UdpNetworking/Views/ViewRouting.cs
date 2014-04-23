﻿using System.Collections.Generic;
using System.Linq;

namespace UdpNetworking.Views
{
    /// <summary>
    /// Keeps track of views in project, and routes messages 
    /// set from networked instances of views, to local ones.
    /// </summary>
    internal static class ViewRouting
    {
        private static readonly List<View> Views = new List<View>();
        
        /// <summary>
        /// Sets a local view to recieve routing of network messages to its instance.
        /// </summary>
        /// <param name="view"></param>
        public static void RegisterView(View view) { 
            //if(!Views.Any(v => v.ViewID == view.ViewID)) 
                Views.Add(view); 
            UnityEngine.Debug.Log(Views.Count);
        }

        /// <summary>
        /// Stops a local view from listening for network messages.
        /// </summary>
        /// <param name="view"></param>
        public static void UnRegisterView(View view) { Views.Remove(view); }

        /// <summary>
        /// Directs incoming messages to the local intance of the view that sent them.
        /// </summary>
        /// <param name="recieved"></param>
        public static void Route(View recieved) {
            for (var i = 0; i < Views.Count; i++) {
                var targetTypeMatch = Views[i].GetType() == recieved.GetType();
                var viewIdMatch = Views[i].ViewID == recieved.ViewID;
                if (targetTypeMatch && viewIdMatch)
                    Views[i].RecieveData(recieved);
            }
        }
    }
}
