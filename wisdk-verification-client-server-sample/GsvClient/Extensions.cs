using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GSV_Client_Framework
{
    /// <summary>
    /// Extension Methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts an object to byte array
        /// </summary>
        /// <param name="obj">An Object.</param>
        /// <returns>Byte array.</returns>
        public static byte[] ToByteArray(this object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();   
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
