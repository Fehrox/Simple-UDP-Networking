using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace UdpNetworking.Messaging
{
    class BinaryFormatterMessageContract : IMessageContract
    {

        public byte[] PackForSend(object obj) 
        {
            if (obj == null) return null;
            using (var stream = new MemoryStream()) {
                var formatter = new BinaryFormatter {
                    AssemblyFormat = FormatterAssemblyStyle.Simple
                };
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public object UnpackForRecieve(byte[] bytes) 
        {
            using (var stream = new MemoryStream()) {
                stream.Write(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                var formatter = new BinaryFormatter();
                    //Binder = new LocalAssemblyBinder()};
                return formatter.Deserialize(stream);
            }
        }

        //sealed class LocalAssemblyBinder : SerializationBinder
        //{
        //    public override Type BindToType(string assemblyName, string typeName) {
        //        var assemblyStr = Assembly.GetEntryAssembly().FullName;
        //        var typeStr = String.Format("{0}, {1}", typeName, assemblyStr);
        //        UnityEngine.Debug.Log(assemblyName + " " + typeStr);
        //        return Type.GetType(typeStr);
        //    }
        //}
    }
}
