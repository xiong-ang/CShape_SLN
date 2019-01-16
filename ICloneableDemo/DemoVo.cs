using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICloneableDemo
{
    class DemoVo:ICloneable
    {
        private string name;
        private List<SubDemoVo> subDemos = new List<SubDemoVo>();
        private Dictionary<string, SubDemoVo> subDemoDict = new Dictionary<string,SubDemoVo>();

        public DemoVo()
        {
            name = "DemoVo";

            subDemos.Add(new SubDemoVo());
            subDemos.Add(new SubDemoVo());

            subDemoDict.Add("k1", new SubDemoVo());
            subDemoDict.Add("k2", new SubDemoVo());
        }

        public void ChangeDemoVo()
        {
            name = "Changed " + name;

            subDemos.ForEach(x => x.ChangeSubDemoVo());

            foreach (KeyValuePair<string, SubDemoVo> item in subDemoDict)
            {
                item.Value.ChangeSubDemoVo();
            }
        }

        class SubDemoVo:ICloneable
        {
            private string name;
            private List<string> strList = new List<string>();
            private Dictionary<int, string> strDict = new Dictionary<int,string>();

            public SubDemoVo()
            {
                name = "SubDemoVo";

                strList.Add("str1");
                strList.Add("str2");

                strDict.Add(1, "str1");
                strDict.Add(2, "str2");
            }

            public void ChangeSubDemoVo()
            {
                name = "Changed " + name;

                for (int i = 0; i < strList.Count; i++)
                {
                    strList[i] = "Changed " + strList[i];
                }

                foreach (var key in strDict.Keys.ToList())
                {
                    strDict[key] = "Changed " + strDict[key];
                }
            }

            public object Clone()
            {
                var result = (SubDemoVo)MemberwiseClone();
                result.strList = (List<string>)CloneHelper.CloneList(strList);
                result.strDict = (Dictionary<int, string>)CloneHelper.CloneDictionary(strDict);
                return result;
            }
        }

        public object Clone()
        {
            var result = (DemoVo)MemberwiseClone();
            result.subDemos = (List<SubDemoVo>)CloneHelper.CloneList(subDemos);
            result.subDemoDict = (Dictionary<string, SubDemoVo>)CloneHelper.CloneDictionary(subDemoDict);
            return result;
        }
    }
}
