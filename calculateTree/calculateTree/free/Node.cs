using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    class Node
    {

        private Node parent;

        Varible self;

        List<Node> param;

        ICalculateMethod method;

        public Node(Varible varilbe)
        {
            this.self = varilbe;
        }


        public Node( Node parent, Varible varilbe, List<Node> param, ICalculateMethod method)
        {
            this.parent = parent;
            this.self = varilbe;
            this.param = param;
            this.method = method;
        }

        List<string> GetAllVarible()
        {
            List<string> res = new List<string>();
            if (!self.IsContant)
                res.Add(self.name);
            if (param!=null && param.Count>0)
            {
                param.ForEach(p=> res.AddRange(p.GetAllVarible()) );
            }
            return res;
        }


        Node GetNodeFromParam(string varible)
        {
            return null;
        }

        private void ConditionAction(Func<Node ,bool> prediction, Action<Node> action)
        {
            if (prediction == null || action == null)
                throw new ArgumentNullException();
            if (prediction(this))
            {
                action(this);
            }
            if (param!=null && param.Count>0)
            {
                param.ForEach(p=>
                { if (prediction(p))
                        action(p);
                });
            }
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
