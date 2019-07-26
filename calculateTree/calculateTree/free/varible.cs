using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    /// <summary>
    /// 变量要么是字母那种变量，要么就是临时变量，所有的固定的数字都是临时变量
    /// </summary>
    internal class Varible
    {
        private static int key = 1;

        public string name { get; }

        private Func<dynamic> GetValueMethod;

        private Dictionary<string, Node> calculateTree=new Dictionary<string, Node>();

        private bool IsValueSet = false;

        public bool IsContant { get; private set; }

        /// <summary>
        /// 表示变量是不是已知的
        /// </summary>
        public bool IsKnown { get; private set; }

        public bool IsTemp { get; private set; }


        private dynamic value;

        string GetName()
        {
            string res = "temp_" + key;
            unchecked
            {
                key += 1;
            }
            return res;
        }

        public Varible()
        {
            this.name = GetName();
            IsTemp = true;
            IsContant = false;
            IsValueSet = false;
        }
        
        public Varible(string name)
        {
            this.name = name;
            IsTemp = false;
            IsContant = false;
            IsValueSet = false;
        }

        public void SetGetValueMethod(Func<dynamic> GetValueMethod)
        {
            this.GetValueMethod = GetValueMethod;
            this.IsKnown = true;
            this.IsContant = false;
        }

        public void SetDefaultValue(dynamic value)
        {
            this.value = value;
            this.IsKnown = true;
            this.IsContant = true;
            this.IsValueSet = true;
        }



        public Varible(string name, Func<dynamic> getValue)
        {
            IsTemp = false;
            this.name = name;
            this.GetValueMethod = getValue;
            this.IsKnown = true;
            this.IsContant = false;
        }

        public Varible(string name,dynamic val)
        {
            this.name = name;
            this.IsTemp = false;
            this.value = val;
            this.IsKnown = true;
            this.IsContant = true;
            this.IsValueSet = true;
        }

        internal void Execute()
        {
            if (this.calculateTree.ContainsKey(ExecuteKey))
            {
                var result = calculateTree[ExecuteKey].InvokeMethod();
                this.value = result;
                this.IsValueSet = true;
            }
            else
                throw new Exception(string.Format("没有名为：{0}的计算键", ExecuteKey));
        }

        
        
        internal void Clear()
        {
            value = null;
            IsValueSet = false;
        }


        internal bool IsDirectGetAble {

            get {
                return IsValueSet || IsKnown;
            }
        }

        public string ExecuteKey { get; set; } = "";

        internal void AddCalculateTree(string key,Node node)
        {
            this.calculateTree[key] = node;
        }


        internal Dictionary<string, List<string>> GetCalculateInfo()
        {
            Dictionary<string, List<string>> res = new Dictionary<string, List<string>>();
            foreach (var node in calculateTree.ToList())
            {
                res.Add(node.Key,node.Value.GetAllRequiredVarible());
            }
            return res;
        }

        public dynamic GetValue()
        {
            if (IsValueSet)
                return value;
            if (IsKnown)
            {
                if (GetValueMethod != null)
                {
                    return GetValueMethod();
                }
                else
                {
                    throw new Exception(String.Format("varible {0} is Known,but GetValueMethod equal null", name));
                }
            }

            if (calculateTree.ContainsKey(ExecuteKey))
            {
                return calculateTree[ExecuteKey].InvokeMethod();
            }

            if (calculateTree.Values.Count!=0)
            {
                dynamic res = null;
                var enu= calculateTree.Values.GetEnumerator();
                Node cal = null;
                do
                {
                    cal = enu.Current;

                    if (cal.GetAllRequiredVarible().Count==0)
                    {
                        if (res == null)
                        {
                            res = cal.InvokeMethod();
                        }
                        else
                        {
                            dynamic temp = cal.InvokeMethod();
                            if (temp!=res)
                            {
                                throw new Exception(string.Format("变量{0}获得多个不同的值",name));
                            }
                        }
                    }
                } while (enu.MoveNext());
                if (res!=null)
                {
                    return res;
                }
            }
            throw new Exception(string.Format("给出条件不足或者其他原因无法计算变量{0}的值", name));
        }


        internal void RepireNode(Func<string, Node> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException();
            }
            if (!calculateTree.ContainsKey(ExecuteKey))
            {
                throw new Exception("没有设定执行键或计算节点不包含执行键");
            }
            var executeNode = calculateTree[ExecuteKey];
            var vari = executeNode.GetAllRequiredVarible();
            vari.ForEach(p =>
            {
                executeNode.ConditionAction(
                f =>
                {
                    if (f.self.name == p)
                        return true;
                    else
                        return false;

                }, h =>
                {
                    Node node = func(p);
                    h.Param = node.Param;
                    h.Method = node.Method;

                });
            });
        }


        internal Node GetExecute()
        {
            if (!calculateTree.ContainsKey(ExecuteKey))
            {
                throw new Exception("unexpected condition");
            }
            return calculateTree[ExecuteKey];
        }



        public override string ToString()
        {
            if (IsDirectGetAble)
            {
                return Convert.ToString(GetValue());
            }
            //不是临时变量
            if (!IsTemp)
            {
                return name;
            }
            throw new Exception("unexpected condition");
        }

        public string GetAllDescription()
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(ExecuteKey))
            {
                if (IsValueSet)
                {
                    builder.AppendLine(string.Format(" {0} | {1} = {2} = {3}",value, name, calculateTree[ExecuteKey].ToString(),value));
                }
                else
                    throw new Exception("unexpected condition");
            }
            else
            {
                foreach (var node in calculateTree.Values)
                {
                    builder.AppendLine(string.Format(" {0} = {1}", name, node.ToString()));
                }
            }
            return builder.ToString();
        }


    }
}
