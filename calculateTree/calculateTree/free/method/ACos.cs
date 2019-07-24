using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class ACos : ICalculateMethod
    {
        public Node currentNode { get; set; }

        public ICalculateMethod Clone()
        {
            return new ACos();
        }

        public string ConvertToString()
        {
            return string.Format("{0}{1}", GetName(), Utils.AddBacket(currentNode.GetParamDescription(0)));
        }

        public string GetName()
        {
            return "acos";
        }

        public int GetParamCount()
        {
            return 1;
        }

        public Node GetUnOpperationCalculateNode(int paramIndex)
        {
            if (paramIndex < 0 || paramIndex >= GetParamCount())
                throw new ArgumentException();
            Node subNode = currentNode.GetNode(paramIndex);
            List<Node> param = new List<Node>();
            param.Add(currentNode.GetTopNode());
            ICalculateMethod sub = new Cos();
            Node res = new Node(subNode.self);
            param.ForEach(p => p.SetParent(res));
            res.SetParams(null, param, sub);
            sub.currentNode = res;
            return res;
        }

        public dynamic GetValue(params dynamic[] param)
        {
            if (param == null || param.Count() != GetParamCount())
            {
                throw new ArgumentException(string.Format("{0}需要两个参数", GetName()));
            }
            return Math.Acos(param[0]);
        }
    }
}
