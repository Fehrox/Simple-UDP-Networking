using System;
using UnityEngine;
using System.Runtime.Serialization;
using UdpLanViews.Views;

[Serializable]
public class ViewTester : View, ISerializable
{

    public bool TestBool;
    public int TestInt;
    public string TestString;
    public ViewTester(int testInt, string testString, bool testBool) {
        TestInt = testInt;
        TestString = testString;
        TestBool = testBool;
    }

    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
        info.AddValue("TestBool", TestBool);
        info.AddValue("TestInt", TestInt);
        info.AddValue("TestString", TestString);
    }

    public ViewTester(SerializationInfo info, StreamingContext context) {
        TestBool = info.GetBoolean("TestBool");
        TestInt = info.GetInt32("TestInt");
        TestString = info.GetString("TestString");
        Debug.Log(TestBool + " " + TestInt + " " + TestString + " recieved object");
    }
}
