﻿using calculateTree.free;
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

            //DateTime time = DateTime.Now;
            //long sum = 0;
            //for (long i=0;i<10616832;i++)
            //{
            //    sum++;
            //}

            //DateTime endtime = DateTime.Now;
            //var span = endtime - time;
            //Double tt = span.TotalMilliseconds;
            //Console.ReadLine();

            //string res = "";
            //string exp = "(23+34*45/(5+6+7))";
            //string exp3 = "1+2*pow(4,(6+7.65*var)*2)=6";
            //string exp4 = "0.56";

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


            //List<string> test = new List<string>();
            //test.Add("(a+1)*(9+c)=b*5");
            //test.Add("a");
            //test.Add("b");
            //test.Add("c");
            //test.Add("b=5");
            //test.Add("c=1");
            //test.Add("a");
            //test.ForEach(p=> {
            //    Console.WriteLine(">:"+p);
            //    Console.WriteLine(engine.Parse(p));
            //});
            Begin:
            string helpText ="CalculateTree:" +"变量解析程序，计算变量与变量的关系，计算变量的值";
            Console.WriteLine(helpText);

            while (true)
            {
                Console.WriteLine("");
                Console.Write(">:");
                string readText = Console.ReadLine();
                if (readText == "clear")
                {
                    engine.Clear();
                    Console.Clear();
                    goto Begin;
                }
                else
                {
                    Console.Write(engine.Parse(readText));
                }
            }
        }
    }
}
