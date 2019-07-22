using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
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
            this.value = val;
            this.IsKnown = true;
            this.IsContant = true;
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


        internal void AddCalculateTree(string key,Node node)
        {
            this.calculateTree[key] = node;
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
            throw new Exception(string.Format( "给出条件不足或者其他原因无法计算变量{0}的值",name));
        }


    }
}
