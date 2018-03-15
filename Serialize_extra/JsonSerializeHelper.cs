using System;
using System.IO;
using Newtonsoft.Json;

namespace Serialize_extra
{
    public class JsonSerializeHelper : ISerializer
    {
        public object Deserialize(Type type, string fileName)
        {
            using (StreamReader fStream = File.OpenText(fileName))
            {
                JsonSerializer format = new JsonSerializer();

                return format.Deserialize(fStream, type);
            }
        }

        public bool Serialize(Type type, object obj, string fileName)
        {
            using (StreamWriter fStream = File.CreateText(fileName))
            {
                JsonSerializer format = new JsonSerializer();

                format.Serialize(fStream, obj);
                return true;
            }
        }
    }
}