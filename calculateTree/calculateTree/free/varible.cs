using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    class Varible
    {
        public string name { get; }

        private Func<dynamic> GetValueMethod;

        private List<Node> calculateTree;

        private bool isContant = false;

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

        public bool IsDirectGetAble {

            get {
                return isContant || IsKnown;
            }
        }

        /// <summary>
        /// 表示变量是不是已知的
        /// </summary>
        public bool IsKnown { get; }

        dynamic GetValue()
        {
            if (isContant)
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
            throw new NotImplementedException("TODO 还没有写这里的逻辑");
        }


    }
}
