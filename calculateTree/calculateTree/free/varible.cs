using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    internal class Varible
    {
        public string name { get; }

        private Func<dynamic> GetValueMethod;

        private Dictionary<string, Node> calculateTree=new Dictionary<string, Node>();

        private bool IsValueSet = false;

        private dynamic value;

        public Varible(string name)
        {
            this.name = name;
            this.IsKnown = false;
        }

        public Varible(string name, Func<dynamic> getValue)
        {
            this.name = name;
            this.GetValueMethod = getValue;
            this.IsKnown = true;
        }

        public Varible(string name,dynamic val)
        {
            this.name = name;
            this.value = val;
            this.IsKnown = true;
        }

        internal void Execute( string calculateKey )
        {
            if (this.calculateTree.ContainsKey(calculateKey))
            {
                var result = calculateTree[calculateKey].InvokeMethod();
                this.value = result;
                this.IsValueSet = true;
            }
            throw new Exception(string.Format("",calculateKey));
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

        /// <summary>
        /// 表示变量是不是已知的
        /// </summary>
        public bool IsKnown { get; }

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
            throw new Exception(string.Format( "给出条件不足或者其他原因无法计算变量{0}的值",name));
        }


    }
}
