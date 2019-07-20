using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    class Node
    {

        Varible self;

        List<Node> param;

        ICalculateMethod method;

        public Node(Varible varilbe)
        {
            this.self = varilbe;
        }


        public Node(Varible varilbe, List<Node> param, ICalculateMethod method)
        {
            this.self = varilbe;
            this.param = param;
            this.method = method;
        }
        
        public dynamic InvokeMethod()
        {
            if (self.IsDirectGetAble)
            {
                return self.GetValue();
            }
            if (param!=null || method!=null ||param.Count==method.GetParamCount())
            {
               dynamic result = method.GetValue(param.Select(p=>p.self.GetValue()).ToArray());
            }
            throw new CannotCalculate(string.Format("变量{0}为未知变量，不能计算",self.name));
        }


    }
}
