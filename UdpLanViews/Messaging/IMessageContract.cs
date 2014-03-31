using System;

namespace UdpLanViews.Messaging
{
    interface IMessageContract {
        /// <summary>
        /// Convert an object to a byte array.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Object of packed type.</returns>
        byte[] PackForSend(Object obj);
        /// <summary>
        ///  Convert a byte array to an Object.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>Given object as bytes.</returns>
        Object UnpackForRecieve(byte[] bytes);
    }
}
