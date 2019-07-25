using calculateTree.free;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculateEngine engine = new CalculateEngine();

            string res = "";
            string exp = "(23+34*45/(5+6+7))";
            string exp3 = "1+2*pow(4,(6+7.65*var)*2)=6";
            string exp4 = "0.56";


            //string exp2 = "1+aa*2+(10-2)=0";
            //res = engine.Parse(exp2);
            //Console.Write(res);
            //Console.Write(engine.GetVaribleDescription("aa"));


            //string exp5 = "sin(3.14)";
            //res = engine.Parse(exp5);
            //Console.Write(res);

            //string exp6 = "sin(Radians(a))+5.25*6-c=b*0.05";
            //res = engine.Parse(exp6);
            //Console.Write(res);
            //Console.Write(engine.GetVaribleDescription("a"));
            //Console.Write(engine.GetVaribleDescription("c"));


            List<string> test = new List<string>();
            test.Add("aa+1=b");
            test.Add("b=5");
            test.Add("aa");
            test.ForEach(p=> {
                Console.WriteLine("输入为："+p);
                Console.WriteLine(engine.Parse(p));
            });

            Console.ReadKey();

        }
    }
}
