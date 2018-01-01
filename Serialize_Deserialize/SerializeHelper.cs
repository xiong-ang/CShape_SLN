using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serialize_Deserialize
{
    class SerializeHelper
    {
        public static bool Serialize(Type type, Object obj, string fileName)
        {
            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                //BinaryFormatter format = new BinaryFormatter();
                //IFormatter format = new SoapFormatter();
                XmlSerializer format = new XmlSerializer(type);

                format.Serialize(fStream, obj);
                return true;
            }
            return false;
        }
        public static Object Deserialize(Type type, string fileName)
        {
            using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                //BinaryFormatter format = new BinaryFormatter();
                //IFormatter format = new SoapFormatter();
                XmlSerializer format = new XmlSerializer(type);

                Object obj = format.Deserialize(fStream);
                if (obj != null)
                    return obj;
            }
            return null;
        }
    }
}
