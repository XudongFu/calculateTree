using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class Mutiply : ICalculateMethod
    {
        public Node currentNode { get; set; }

        public string GetName()
        {
            return "*";
        }

        public int GetParamCount()
        {
            return 2;
        }

        public Node GetUnOpperationCalculateNode(int paramIndex)
        {
            throw new NotImplementedException();
        }

        public dynamic GetValue(params dynamic[] param)
        {
            if (param == null || param.Count() != 2)
            {
                throw new ArgumentException("乘法需要两个参数");
            }
            return param[0] * param[1];
        }
    }
}
