using System;
using System.Runtime.Serialization;
using UdpNetworking.Views;

[Serializable]
public class ViewTester : View, ISerializable
{
    public int TestInt;
    public string TestString;

    public ViewTester(ushort viewId, int testInt, string testString) : base(viewId){
        TestInt = testInt;
        TestString = testString;
    }

    public ViewTester(SerializationInfo info, StreamingContext context)
        : base(info, context) {
        TestInt = info.GetInt32("TestInt");
        TestString = info.GetString("TestString");
    } 

    new public void GetObjectData(SerializationInfo info, StreamingContext context) {
        base.GetObjectData(info, context);
        info.AddValue("TestInt", TestInt);
        info.AddValue("TestString", TestString);
    }

    public override void RecieveData(View recievedView) {
        var resored = recievedView as ViewTester;
        TestInt = resored.TestInt;
        TestString = resored.TestString;
        UnityEngine.Debug.Log("Are we in teh main thread? " + recievedView.ViewID);
    }
}