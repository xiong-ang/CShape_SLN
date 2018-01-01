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
    class Program
    {
        static void Main(string[] args)
        {
            Person me = new Person
            {
                Name = "Barret",
                Age = 25,
                Sex = true
            };
            SerializeHelper.Serialize(typeof(Person), me, "me.xml");
            Person someone = (Person)SerializeHelper.Deserialize(typeof(Person), "me.xml");
            Console.WriteLine(someone);
        }
    }
}
