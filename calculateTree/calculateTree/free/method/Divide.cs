using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class Divide : ICalculateMethod
    {

        public Node currentNode { get; set; }

        public string GetName()
        {
            return "/";
        }

        public int GetParamCount()
        {
            return 2;
        }

        public Node GetUnOpperationCalculateNode(int paramIndex)
        {
            if (paramIndex < 0 || paramIndex > 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            Node subNode = currentNode.GetNode(paramIndex);
            List<Node> param = new List<Node>();
            //乘法
            if (paramIndex == 0)
            {
                param.Add(currentNode.GetTopNode());
                param.Add(currentNode.GetNode(paramIndex == 0 ? 1 : 0));
                Mutiply sub = new Mutiply();
                Node res = new Node(null, subNode.self, param, sub);
                sub.currentNode = res;
                return res;
            }
            //除法
            else
            {
                param.Add(currentNode.GetNode(0));
                param.Add(currentNode.GetTopNode());
                Divide sub = new Divide();
                Node res = new Node(null, subNode.self, param, sub);
                sub.currentNode = res;
                return res;
            }
        }

        public dynamic GetValue(params dynamic[] param)
        {
            if (param == null || param.Count() != 2)
            {
                throw new ArgumentException("除法需要两个参数");
            }
            return param[0] / param[1];
        }
    }
}
