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

        public Varible self { get; }

        internal List<Node> Param { get;set; }

        internal ICalculateMethod Method { get; set; }

        public int Index { get; private set; }



        public Node GetTopNode()
        {
            if (parent != null)
            {
                return parent.GetNodeFromParam(Index);
            }
            return this.Clone(false);
        }

        public Node GetNode(int index)
        {
            if (Param != null && index < Param.Count)
            {
                return Param[index].Clone();
            }
            throw new ArgumentOutOfRangeException();
        }

        internal string GetParamDescription(int index)
        {
            if (Param != null && index < Param.Count)
            {
                return Param[index].ToString();
            }
            throw new ArgumentOutOfRangeException();
        }

        public Node(Varible varilbe)
        {
            this.self = varilbe;
        }


        private Node Clone(bool includeparam = true)
        {
            Node copy = new Node(self);
            if (includeparam)
                copy.SetParams(parent, Param, Method);
            else
                copy.SetParams(parent, null, Method);
            return copy;
        }

        internal void SetParent(Node parent)
        {
            this.parent = parent;
            if (this.parent == null)
            {
                Index = -1;
            }
        }

        public void SetParams(Node parent, List<Node> param, ICalculateMethod method)
        {
            this.parent = parent;
            if (this.parent == null)
            {
                Index = -1;
            }
            if (param != null)
            {
                this.Param = param;
                int inde = 0;
                param.ForEach(p => p.Index = inde++);
            }
            if (method != null)
            {
                this.Method = method.Clone();
                this.Method.currentNode = this;
            }
        }

        private int locateVaribleIndex(string varible)
        {
            if (Param != null && Param.Count > 0)
            {
                Dictionary<string, int> dic = new Dictionary<string, int>();
                int i = 0;
                try
                {
                    Param.ForEach(p =>
                    {
                        dic.Add(p.self.name, i++);
                    });
                }
                //增加的时候报重复键错误
                catch (ArgumentException e)
                {
                    throw new Exception(string.Format("数据状态构建错误，{0}节点出现相同的名称的子节点", self.name));
                }
                if (dic.ContainsKey(varible))
                {
                    return dic[varible];
                }
                else
                {
                    throw new ArgumentException(string.Format("直接下级没有名为{0}的节点", varible));
                }
            }
            throw new Exception(string.Format("no lower node exists", varible));
        }



        public List<string> GetAllRequiredVarible()
        {
            var res = GetAllVaribles();
            res.Remove(self.name);
            return res;
        }



        private List<string> GetAllVaribles()
        {
            List<string> res = new List<string>();
            if (!self.IsContant && !self.IsTemp)
                res.Add(self.name);
            if (Param != null && Param.Count > 0)
            {
                Param.ForEach(p => res.AddRange(p.GetAllVaribles()));
            }
            return res;
        }
        
        public Node GetNodeFromParam(string varible)
        {
            Node node = null;
            this.ConditionAction(p =>
            {
                if (p.Param != null && p.Param.Any(t => t.self.name.Equals(varible)))
                    return true;
                else
                    return false;
            },
            h =>
            {
                int index = h.locateVaribleIndex(varible);
                node = h.Method.GetUnOpperationCalculateNode(index);
            });
            if (node != null)
                return node;
            else
                throw new Exception(string.Format("没有找到名为{0}的变量", varible));
        }


        public Node GetNodeFromParam(int index)
        {
            return Method.GetUnOpperationCalculateNode(index);
        }


        internal void ConditionAction(Func<Node, bool> prediction, Action<Node> action)
        {
            if (prediction == null || action == null)
                throw new ArgumentNullException();
            if (prediction(this))
            {
                action(this);
            }
            if (Param != null && Param.Count > 0)
            {
                Param.ForEach(p => p.ConditionAction(prediction, action));
            }
        }


        public dynamic InvokeMethod()
        {
            if (self.IsDirectGetAble)
            {
                return self.GetValue();
            }
            if (Param != null || Method != null || Param.Count == Method.GetParamCount())
            {
                List<dynamic> pp = new List<dynamic>();
                Param.ForEach(p => pp.Add(p.InvokeMethod()));
                dynamic result = Method.GetValue(pp.ToArray());
                return result;
            }
            throw new CannotCalculate(string.Format("变量{0}为未知变量，不能计算", self.name));
        }


        public override string ToString()
        {
            if (Method == null ||Param == null)
            {
                return self.ToString();
            }
            return Method.ConvertToString();
        }


    }
}
