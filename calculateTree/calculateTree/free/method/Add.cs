using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class Add : ICalculateMethod
    {

        public Node currentNode { get; set ; }

        public int GetParamCount()
        {
            return 2;
        }

        public Node GetUnOpperationCalculateNode(int paramIndex)
        {
            if (paramIndex < 0 || paramIndex >= GetParamCount())
                throw new ArgumentException();
            Node subNode = currentNode.GetNode(paramIndex);
            List<Node> param = new List<Node>();
            param.Add(currentNode.GetTopNode());
            param.Add(currentNode.GetNode(paramIndex == 0 ? 1 : 0));
            Sub sub = new Sub();
            Node res = new Node(null,subNode.self,param,sub);
            sub.currentNode = res;
            return res;
        }

        public dynamic GetValue(params dynamic[] param)
        {
            if (param==null || param.Count()!=2)
            {
                throw new ArgumentException("加法需要两个参数");
            }
            return param[0] + param[1];
        }
    }
}
