using System;
using System.IO;
using System.Xml.Serialization;

namespace Serialize_extra
{
    public class XmlSerializeHelper : ISerializer
    {
        public object Deserialize(Type type, string fileName)
        {
            using (StreamReader fStream = File.OpenText(fileName))
            {
                XmlSerializer format = new XmlSerializer(type);

                return format.Deserialize(fStream);
            }
        }

        public bool Serialize(Type type, object obj, string fileName)
        {
            using (StreamWriter fStream = File.CreateText(fileName))
            {
                XmlSerializer format = new XmlSerializer(type);

                format.Serialize(fStream, obj);
                return true;
            }
        }
    }
}