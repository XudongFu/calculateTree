using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.method
{
    class Utils
    {

        public static string AddBacket(string exp)
        {
            if (string.IsNullOrWhiteSpace(exp))
            {
                return "";
            }
            if (exp[0] == '(' && exp[exp.Length - 1] == ')')
            {
                return exp;
            }
            else
            {
                return string.Format("({0})",exp);
            }

        }

    }
}
