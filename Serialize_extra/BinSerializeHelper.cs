using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace Serialize_extra
{
    public class BinSerializeHelper : ISerializer
    {
        public object Deserialize(Type type, string fileName)
        {
            using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter format = new BinaryFormatter();

                return format.Deserialize(fStream);
            }
        }

        public bool Serialize(Type type, object obj, string fileName)
        {
            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter format = new BinaryFormatter();

                format.Serialize(fStream, obj);
                return true;
            }
        }
    }
}