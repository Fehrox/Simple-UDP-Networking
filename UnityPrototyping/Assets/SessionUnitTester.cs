using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Network = UdpNetworking.Network.Network;
using UdpNetworking.Session;

public class SessionUnitTester : MonoBehaviour {

    void Start() {
        Network.Processor = MainThreadProcessor.Instance;
        Network.Statistics.Log = true;
    }

    void OnGUI() {

        //if (GUILayout.Button("TestAsync")) {
        //    var newClient = new UdpClient(21044);
        //    newClient.BeginReceive(result => Debug.Log("Recieved!"), null);
        //    newClient.Close();
        //}

        if (Network.Statistics.Log) {
            GUILayout.Label(
                "Host: " + Network.HostAddress + "\n" +
                "Connected: " + (Network.Connected ? Network.HostAddress.Address.ToString() : "Disconnected") + "\n" +
                "Connections Made: " + Network.Statistics.Connections + "\n" +
                "Disconnects: " + Network.Statistics.Disconnects + "\n" +
                "Messages\n"+
                "Total Recieved: " + Network.Statistics.TotalMessagesRecieved + "\n" +
                "Valid Recieved: " + Network.Statistics.ValidMessagesRecieved + "\n" +
                "Forign Recieved: " + Network.Statistics.ForignMessagesRecieved + "\n" +
                "Total Sent : " + Network.Statistics.MessagesSent
            );
        }

        if (!Network.Connected) {

            var ip = "192.168.1.10";
            if (GUILayout.Button("Connect (" + ip + ")"))
                Network.Connect(IPAddress.Parse(ip));

            if (GUILayout.Button("Find"))
                Session.Find(MatchFound);

            if (GUILayout.Button("Listen For Broadcast"))
                Network.BroadcastConnect();

            if (GUILayout.Button("Announce"))
                Session.Announce(1);

            if (GUILayout.Button("Stop Announcing"))
                Session.StopAnnouncing();

        } else {

            if (GUILayout.Button("Send some other message!!"))
                RemoteInvoke.SendMessage("RecievedMessage");

            if (GUILayout.Button("Disconnect"))
                Network.Disconnect();

        }

        GUILayout.Label("Recieved message?? " + _recievedMessage);
    }

    private bool _recievedMessage;
    void RecievedMessage() {
        _recievedMessage = !_recievedMessage;
        Debug.Log("Recieved message!");
    }

    void MatchFound(int networkId, IPAddress sender) {
        Debug.Log("Match found!! " + networkId + " " + sender);
        if (Network.Connected) {
            Session.StopAnnouncing();
            Network.Disconnect();
        }

        Network.Connect(sender);
        RemoteInvoke.SendMessage("Connected");
    }

    void Connected() {
        Debug.Log("Player connected!");
    }

    void OnApplicationQuit() {
        if(Network.Connected)
            Network.Disconnect();
    }
}
