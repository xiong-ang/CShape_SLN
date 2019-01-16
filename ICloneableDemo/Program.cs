using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICloneableDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoVo dv = new DemoVo();
            DemoVo cloned_dv = (DemoVo)dv.Clone();

            dv.ChangeDemoVo();
        }
    }
}
