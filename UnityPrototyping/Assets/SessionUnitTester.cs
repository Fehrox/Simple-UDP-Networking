using UnityEngine;
using Network = UdpNetworking.Network.Network;
using UdpNetworking.Session;

public class SessionUnitTester : MonoBehaviour {

    void Start() {
        Network.Start();
    }

    void OnGUI() {

        if (GUILayout.Button("Broadcast")) {
            Session.Announce(1);
        }

        if (GUILayout.Button("Recieve")) {
            Session.Find(MatchFound);
        }

    }

    void MatchFound(int networkId) {
        Debug.Log("Match found!! " + networkId);
    }

    void OnApplicationQuit() {
        Network.Stop();
    }
}
