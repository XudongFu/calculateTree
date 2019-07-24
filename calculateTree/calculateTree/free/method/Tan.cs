using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class Tan : ICalculateMethod
    {
        public Node currentNode { get; set; }

        public ICalculateMethod Clone()
        {
            return new ACos();
        }

        public string ConvertToString()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return this.GetType().Name;
        }

        public int GetParamCount()
        {
            return 1;
        }

        public Node GetUnOpperationCalculateNode(int paramIndex)
        {
            throw new NotImplementedException();
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
