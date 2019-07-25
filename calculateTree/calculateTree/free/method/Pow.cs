using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class Pow : ICalculateMethod
    {
        public Node currentNode { get; set; }

        public ICalculateMethod Clone()
        {
            return new Pow();
        }

        public string ConvertToString()
        {
            return string.Format("{0}({1},{2})",  GetName(), currentNode.GetParamDescription(0), currentNode.GetParamDescription(1));
        }

        public string GetName()
        {
            return this.GetType().Name;
        }

        public int GetParamCount()
        {
            return 2;
        }

        public Node GetUnOpperationCalculateNode(int paramIndex)
        {
            if (paramIndex < 0 || paramIndex >= GetParamCount())
                throw new ArgumentException();
            if (paramIndex == 0)
            {
                Node subNode = currentNode.GetNode(paramIndex);
                List<Node> param = new List<Node>();
                param.Add(currentNode.GetTopNode());
                param.Add(GetReciprocal(currentNode.GetNode(1)));
                Sub sub = new Sub();
                Node res = new Node(subNode.self);
                param.ForEach(p => p.SetParent(res));
                res.SetParams(null, param, sub);
                sub.currentNode = res;
                return res;

            }
            else
            {
                Node subNode = currentNode.GetNode(paramIndex);
                List<Node> param = new List<Node>();
                param.Add(currentNode.GetNode(0));
                param.Add(currentNode.GetTopNode());
                Log sub = new Log();
                Node res = new Node(subNode.self);
                param.ForEach(p => p.SetParent(res));
                res.SetParams(null, param, sub);
                sub.currentNode = res;
                return res;
            }
        }


        Node GetReciprocal(Node node)
        {
            Varible varible = new Varible("1", 1);
            Node leftNode = new Node(varible);
            Node topNode = new Node(new Varible());
            List<Node> param = new List<Node>();
            param.Add(leftNode);
            param.Add(node);
            ICalculateMethod method = new Divide();
            topNode.SetParams(null,param,method);
            param.ForEach(p => p.SetParent(topNode));
            method.currentNode = topNode;
            return topNode;
        }


        public dynamic GetValue(params dynamic[] param)
        {
            if (param == null || param.Count() != GetParamCount())
            {
                throw new ArgumentException(string.Format("{0}需要两个参数", GetName()));
            }
            return Math.Pow(param[0], param[1]);
        }
    }
}
