using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class Sub : ICalculateMethod
    {
        public Node currentNode { get; set; }

        public int GetParamCount()
        {
            return 2;
        }


        public string GetName()
        {
            return "-";
        }
        public Node GetUnOpperationCalculateNode(int paramIndex)
        {
            if (paramIndex<0 || paramIndex>1)
            {
                throw new ArgumentOutOfRangeException();
            }
            Node subNode = currentNode.GetNode(paramIndex);
            List<Node> param = new List<Node>();
            //加
            if (paramIndex == 0)
            {
                param.Add(currentNode.GetTopNode());
                param.Add(currentNode.GetNode(paramIndex == 0 ? 1 : 0));
                Add sub = new Add();
                Node res = new Node(subNode.self);
                param.ForEach(p => p.SetParent(res));
                res.SetParams(null, param, sub);
                sub.currentNode = res;
                return res;
            }
            //减
            else
            {
                param.Add(currentNode.GetNode(0));
                param.Add(currentNode.GetTopNode());
                Add sub = new Add();
                Node res = new Node(subNode.self);
                param.ForEach(p => p.SetParent(res));
                res.SetParams(null, param, sub);
                sub.currentNode = res;
                return res;
            }
        }

        public dynamic GetValue(params dynamic[] param)
        {
            if (param == null || param.Count() != 2)
            {
                throw new ArgumentException("减法需要两个参数");
            }
            return param[0] - param[1];
        }
    }
}
