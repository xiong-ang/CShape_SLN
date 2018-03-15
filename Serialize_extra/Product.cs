using System;
using System.Collections.Generic;
namespace Serialize_extra
{
    [Serializable]
    public class Product
    {
        public string Name { get; set; }
        public DateTime Expiry { get; set; }
        public string[] Sizes { get; set; }
        //public IData Data { get; set; }
        public MyData Data { get; set; }
        public Dictionary<string, int> MyProperty { get; set; }
        public void show()
        {
            Console.WriteLine("Name: \n"+Name);
            Console.WriteLine("Expiry: \n"+Expiry.ToShortDateString());
            Console.WriteLine("Sizes: \n"+Sizes.ToString());
            Console.WriteLine("Data: \n"+Data.Data);
            Console.WriteLine("MyProperty: \n"+MyProperty.ToString());
        }
        static void Main(string[] args)
        {
            Product product = new Product();
            product.Name = "Apple";
            product.Expiry = new DateTime(2008, 12, 28);
            product.Sizes = new string[] { "Small" };
            product.Data=new MyData("data");
            product.MyProperty=new Dictionary<string, int>{
                ["a"]=1,
                ["b"]=2
            };

            product.show();

            ISerializer serializeHelper=new XmlSerializeHelper();
            //serializeHelper.Serialize(typeof(Product), product, "tem.xml");
            //((Product)serializeHelper.Deserialize(typeof(Product),"tem.xml")).show();

            serializeHelper=new BinSerializeHelper();
            //serializeHelper.Serialize(typeof(Product), product, "tem.bin");
            //((Product)serializeHelper.Deserialize(typeof(Product),"tem.bin")).show();

            serializeHelper=new JsonSerializeHelper();
            serializeHelper.Serialize(typeof(Product), product, "tem.json");
            ((Product)serializeHelper.Deserialize(typeof(Product),"tem.json")).show();
        }
    }
    public interface IData
    {
        string Data { get; set; }
    }
    [Serializable]
    public class MyData:IData
    {
        public string Data { get; set; }
        public MyData(string data)
        {
            this.Data=data;
        }
         
        public MyData()
        {   
        }
    }
}