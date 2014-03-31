using UnityEngine;
using UdpLanViews.Network;
using Network = UdpLanViews.Network.Network;

public class NetTester : MonoBehaviour {

    ViewTester viewTesterOne = new ViewTester(1, 1, "DesktopOne");
    ViewTester viewTesterTwo = new ViewTester(2, 1, "DesktopTwo");

    void Start() {
        Network.Listen();
        viewTesterOne.TestInt = 1;
        viewTesterOne.TestString = "DesktopOne";
        viewTesterTwo.TestInt = 2;
        viewTesterTwo.TestString = "DesktopTwo";
    }

    void OnGUI() {
        GUILayout.Label("viewTesterOne " + viewTesterOne.TestString + " " + viewTesterOne.TestInt);
        GUILayout.Label("viewTesterTwo " + viewTesterTwo.TestString + " " + viewTesterTwo.TestInt);

        if (GUILayout.Button("RUN THAT BIATCH!!!")) {
            viewTesterOne.TestInt++;
            viewTesterTwo.TestInt++;
            viewTesterOne.ServerAssert();
            viewTesterTwo.ServerAssert();
        }
    }

}
