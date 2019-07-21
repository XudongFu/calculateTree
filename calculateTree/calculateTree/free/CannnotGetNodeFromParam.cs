using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    class CannnotGetNodeFromParam:Exception
    {
        public CannnotGetNodeFromParam(string str) : base(str)
        { }
    }
}
