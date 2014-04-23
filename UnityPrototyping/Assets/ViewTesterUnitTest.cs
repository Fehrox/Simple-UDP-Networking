using UnityEngine;
using UdpNetworking.Network;
using Network = UdpNetworking.Network.Network;

public class ViewTesterUnitTest : MonoBehaviour {

    //ViewTester viewTesterOne = new ViewTester(1, 1, "DesktopOne");
    //ViewTester viewTesterTwo = new ViewTester(2, 1, "DesktopTwo");

    void Start() {
        //Network.Connect();
        //viewTesterOne.TestInt = 1;
        //viewTesterOne.TestString = "DesktopOne";
        //viewTesterTwo.TestInt = 2;
        //viewTesterTwo.TestString = "DesktopTwo";
    }

    void OnGUI() {
        //GUILayout.Label("viewTesterOne " + viewTesterOne.TestString + " " + viewTesterOne.TestInt);
        //GUILayout.Label("viewTesterTwo " + viewTesterTwo.TestString + " " + viewTesterTwo.TestInt);

        if (GUILayout.Button("Send a message!")) {
            RemoteInvoke.SendMessage("RecieveMessage", "Yuppers!");
            //viewTesterOne.TestInt++;
            //viewTesterTwo.TestInt++;
            //viewTesterOne.Sync();
            //viewTesterTwo.Sync();
        }
    }

    public void RecieveMessage(string message) {
        Debug.Log("Message recieved! " + message);
    }

}
