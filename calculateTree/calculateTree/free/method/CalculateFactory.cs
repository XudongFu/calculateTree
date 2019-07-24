using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class CalculateFactory
    {

        private static Dictionary<string, Type> oppDic = null;

        public static ICalculateMethod GetMethod(string method)
        {
            if (oppDic == null)
            {
                oppDic = new Dictionary<string, Type>();
                var types = typeof(CalculateFactory).Assembly.GetTypes().Where(p => p.GetInterface("ICalculateMethod") != null).ToList();
                types.ForEach(p =>
                {
                    var temp = Activator.CreateInstance(p);
                    oppDic[((ICalculateMethod)temp).GetName().ToUpper()] = p;
                });
            }
            if (oppDic.ContainsKey(method.ToUpper()))
            {
                return (ICalculateMethod)Activator.CreateInstance(oppDic[method.ToUpper()]);
            }
            throw new NotImplementedException();
        }

    }
}
