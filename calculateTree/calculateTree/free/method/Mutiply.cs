﻿using System;
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
            if (paramIndex < 0 || paramIndex >= GetParamCount())
                throw new ArgumentException();
            Node subNode = currentNode.GetNode(paramIndex);
            List<Node> param = new List<Node>();
            param.Add(currentNode.GetTopNode());
            param.Add(currentNode.GetNode(paramIndex == 0 ? 1 : 0));
            Divide sub = new Divide();
            Node res = new Node( subNode.self);
            param.ForEach(p => p.SetParent(res));
            res.SetParams(null,param,sub);
            sub.currentNode = res;
            return res;
        }

        public dynamic GetValue(params dynamic[] param)
        {
            if (param == null || param.Count() != 2)
            {
                throw new ArgumentException("乘法需要两个参数");
            }
            return param[0] * param[1];
        }

        public ICalculateMethod Clone()
        {
            return new Mutiply();
        }

        public string ConvertToString()
        {
            return string.Format("{0}{1}{2}", currentNode.GetParamDescription(0), GetName(), currentNode.GetParamDescription(1));
        }
    }
}
