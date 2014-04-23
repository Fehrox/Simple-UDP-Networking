using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Network = UdpNetworking.Network.Network;
using UdpNetworking.Session;

public class SessionUnitTester : MonoBehaviour {

    void Start() {
        Network.Processor = MainThreadProcessor.Instance;
    }

    void OnGUI() {

        if (GUILayout.Button("TestAsync")) {
            var newClient = new UdpClient(21044);
            newClient.BeginReceive(result => Debug.Log("Recieved!"), null);
            newClient.Close();
        }

        if (GUILayout.Button("Announce"))
            Session.Announce();

        if (GUILayout.Button("Stop Announcing"))
            Session.Stop();

        if (GUILayout.Button("Find")) 
            Session.Find(MatchFound);

        if (GUILayout.Button("Listen For Broadcast"))
            Network.BroadcastConnect();


        if (GUILayout.Button("Connect"))
            Network.Connect(IPAddress.Parse("192.168.1.25"));

        if(GUILayout.Button("Disconnect"))
            Network.Disconnect(); 

        if(GUILayout.Button("Send some other message!!"))
            RemoteInvoke.SendMessage("RecievedMessage");

        GUILayout.Label("Recieved message?? " + _recievedMessage);
    }

    private bool _recievedMessage;
    void RecievedMessage() {
        _recievedMessage = !_recievedMessage;
        Debug.Log("Recieved message!");
    }

    void MatchFound(int networkId, IPAddress sender) {
        Debug.Log("Match found!! " + networkId + " " + sender);
        if (!Network.Connected) {
            Session.Stop();
            Network.Connect(sender);
            RemoteInvoke.SendMessage("Connected");
        }
    }

    void Connected() {
        Debug.Log("Player connected!");
    }

    void OnApplicationQuit() {
        Network.Disconnect();
    }
}
