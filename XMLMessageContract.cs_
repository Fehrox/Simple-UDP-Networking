using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UdpNetworking.Views;

namespace UdpNetworking.Messaging
{
    class XmlMessageContract : IMessageContract
    {
        /// <summary>
        /// A mechanism for transferring an object with its type.
        /// </summary>
        [DataContract]
        private struct TypeMessage {
            [DataMember]
            public Type T;
            [DataMember]
            public Object Obj;
            public TypeMessage(Type type, Object obj) {
                T = type;
                Obj = obj;
            }
        }

        public byte[] PackForSend(object obj) {
            var typeMessage = new TypeMessage(obj.GetType(), obj);
            using (var stream = new MemoryStream()) {
                var serializer = new DataContractSerializer(typeof(TypeMessage));
                serializer.WriteObject(stream, typeMessage);
                stream.Position = 0;
                return stream.ToArray();
            }
        }

        public object UnpackForRecieve(byte[] bytes) {
            using (Stream stream = new MemoryStream()) {
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                var deserializer = new DataContractSerializer(typeof(TypeMessage));
                var typeObj = (TypeMessage)deserializer.ReadObject(stream);
                return Convert.ChangeType(typeObj.Obj, typeObj.T);
            }
        }

        //private static IEnumerable<Type> GetKnownTypes() {
        //    return  Assembly.GetExecutingAssembly()
        //                    .GetTypes()
        //                    .Where(t => typeof(TypeMessage).IsAssignableFrom(t))
        //                    .ToList();
        //}

    }
}
