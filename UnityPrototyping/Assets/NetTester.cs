using UnityEngine;
using UdpLanViews.Network;
using Network = UdpLanViews.Network.Network;

public class NetTester : MonoBehaviour {

    ViewTester viewTesterOne = new ViewTester(1, "One", true);

    void Start() {
        Network.Listen();
    }

    void OnGUI() {
        if (GUILayout.Button("RUN THAT BIATCH!!!")) {
            viewTesterOne.ServerAssert();
        }
    }

}
