using UdpNetworking.Client;
using UnityEngine;
using System.Collections;

public class ThreadlessUdpNetworkClient_Test : ThreadlessUdpNetworkClient, IUnitTestable
{
    public ThreadlessUdpNetworkClient_Test() {}

    public bool RunTest() {
        var processor = MainThreadProcessor.Instance;
        var testThreadlessClient = new ThreadlessUdpNetworkClient(21044, processor);
        testThreadlessClient.BeginRecieve(result => Debug.Log("Response"));
        GameObject.DestroyImmediate(processor.gameObject);
        return false;
    }

}
