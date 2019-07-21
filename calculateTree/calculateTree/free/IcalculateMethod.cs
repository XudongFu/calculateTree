using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    interface ICalculateMethod
    {
        Node currentNode { get; set; }

        int GetParamCount();

        dynamic GetValue(params dynamic[] param);

        Node GetUnOpperationCalculateNode(int paramIndex);




    }
}
