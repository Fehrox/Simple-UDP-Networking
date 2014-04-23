using UdpNetworking.Views;
using System.Runtime.Serialization;
using UnityEngine;
using Object = System.Object;

[System.Serializable]
public class RemoteInvoke : View, ISerializable {

    private static RemoteInvoke _instance;
    private string _methodName;
    private Object _value;

    public static void SendMessage(string methodName, Object value = null) {
        _instance._methodName = methodName;
        _instance._value = value;
        _instance.Sync();
    }

    public RemoteInvoke() {}
    static RemoteInvoke() {
        Debug.Log("RemoteInvoke()");
        if (_instance == null)
            _instance = new RemoteInvoke();
    }

    /// <summary>
    /// Unpack object on receipt.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public RemoteInvoke(SerializationInfo info, StreamingContext context)
        : base(info, context) {
        var i = 0;
        _methodName = info.GetString(i++.ToString());
        try {
            _value = info.GetValue(i++.ToString(), typeof(Object));
        } catch (System.InvalidCastException e) {
            // No object was sent.
            _value = null;
        }
    } 

    /// <summary>
    /// Pack object for sending.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    new public void GetObjectData(SerializationInfo info, StreamingContext context) {
        base.GetObjectData(info, context);
        var i = 0;
        info.AddValue(i++.ToString(), _methodName);
        info.AddValue(i++.ToString(), _value);
    }

    /// <summary>
    /// Handle local instnace of object.
    /// </summary>
    /// <param name="recievedView"></param>
    public override void RecieveData(View recievedView) {
        //Debug.Log("RecieveData(View recievedView)");
        var restored = recievedView as RemoteInvoke;
        var recievers = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
        foreach(var reciever in recievers)
            (reciever as GameObject).SendMessage(
                restored._methodName, 
                restored._value, 
                SendMessageOptions.DontRequireReceiver
            );
    }
}
