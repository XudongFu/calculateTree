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

        List<Node> param;

        ICalculateMethod method;

        public int Index { get; private set; }

        

        public Node GetTopNode()
        {
            if (parent!=null)
            {
                return parent.GetNodeFromParam(Index);
            }
            return this;
        }

        public Node GetNode(int index)
        {
            if (param!=null && index<param.Count)
            {
                return param[index];
            }
            throw new ArgumentOutOfRangeException();
        }



        public Node(Varible varilbe)
        {
            this.self = varilbe;
        }


        public Node( Node parent, Varible varilbe, List<Node> param, ICalculateMethod method)
        {
            if (varilbe==null || param==null || method==null)
            {
                throw new ArgumentNullException();
            }
            this.parent = parent;
            if (this.parent==null)
            {
                Index = -1;
            }
            this.self = varilbe;
            this.param = param;
            this.method = method;
            int inde = 0;
            param.ForEach(p=>p.Index=inde++);
            
        }

        private int locateVaribleIndex(string varible)
        {
            if (param!=null && param.Count>0)
            {
                Dictionary<string,int> dic = new Dictionary<string,int >();
                int i = 0;
                try
                {
                    param.ForEach(p => {
                        dic.Add(p.self.name, i++);
                    });
                }
                //增加的时候报重复键错误
                catch (ArgumentException e)
                {
                    throw new Exception( string.Format( "数据状态构建错误，{0}节点出现相同的名称的子节点",self.name));
                }
                if (dic.ContainsKey(varible))
                {
                    return dic[varible];
                }
                else
                {
                    throw new ArgumentException(string.Format("直接下级没有名为{0}的节点",varible));
                }
            }
            throw new Exception(string.Format("no lower node exists",varible));
        }



       public List<string> GetAllVarible()
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


        public Node GetNodeFromParam(string varible)
        {
            try
            {
                int index = locateVaribleIndex(varible);                
                return method.GetUnOpperationCalculateNode(index);
            }
            catch (Exception e)
            {
                throw new Exception("");
            }
         
        }


        public Node GetNodeFromParam(int index)
        {
            return method.GetUnOpperationCalculateNode(index);
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
