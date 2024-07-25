using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArganaWeedClietApi
{
    public class ServiceSerializer<T> where T : class
    {
        public static T Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static T DeserializeFromBytes(byte[] data)
        {
            return Deserialize(Encoding.UTF8.GetString(data, 0, data.Length));
        }

        public static string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static byte[] SerializeToBytes(T obj)
        {
            return Encoding.UTF8.GetBytes(Serialize(obj));
        }

        public static byte[] ToBytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
