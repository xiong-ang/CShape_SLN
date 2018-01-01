using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialize_Deserialize
{
    [Serializable]
    public class Person
    {
        public string Name;
        public int Age;
        public bool Sex;

        public Person(){}
        public Person(string name, int age, bool sex)
        {
            this.Name = name;
            this.Age = age;
            this.Sex = sex;
        }

        public override string ToString()
        {
            return "姓名：" + this.Name +"\t年龄："+this.Age+ "\t性别：" + (this.Sex ? "男" : "女");
        }
    }
}
