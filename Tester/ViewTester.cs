using System;
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

    public ViewTester(SerializationInfo info, StreamingContext context) {
        TestInt = info.GetInt32("TestInt");
        TestString = info.GetString("TestString");
        TestBool = info.GetBoolean("TestBool");
        Console.WriteLine("Synced " + TestInt + " "  + TestString + " " + TestBool);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
        info.AddValue("TestInt", TestInt);
        info.AddValue("TestString", TestString);
        info.AddValue("TestBool", TestBool);
    }
}