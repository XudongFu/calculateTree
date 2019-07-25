using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class Log : ICalculateMethod
    {
        public Node currentNode { get; set; }

        public ICalculateMethod Clone()
        {
            return new Log();
        }

        public string ConvertToString()
        {
            return string.Format("{0}({1},{2})", GetName(), currentNode.GetParamDescription(0), currentNode.GetParamDescription(1));
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
            //todo 这里存在问题
            return Math.Log(param[0],param[1]);
        }
    }
}
