using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class CalculateFactory
    {

        public static ICalculateMethod GetMethod(string method)
        {
            if (method == "+")
                return new Add();
            if (method == "-")
                return new Sub();
            if (method == "*")
                return new Mutiply();
            if (method == "/")
                return new Divide();
            if (method == "sin")
                return new Sin();
            throw new NotImplementedException();
        }

    }
}
