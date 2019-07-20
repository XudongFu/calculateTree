using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    interface ICalculateMethod
    {
        int GetParamCount();

        dynamic GetValue(params dynamic[] param);

    }
}
