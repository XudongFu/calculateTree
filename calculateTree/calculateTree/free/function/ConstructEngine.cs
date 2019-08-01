using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.function
{
    class ConstructEngine
    {
        /// <summary>
        /// 第一个string为type,第二个string为varibleName
        /// </summary>
        internal Dictionary<string, Dictionary<string, VaribleWithType>> VaribleDic = new Dictionary<string, Dictionary<string, VaribleWithType>>();


        /// <summary>
        /// 源type,目标type
        /// </summary>
        Dictionary<string, Tuple<string, IMethodWithType>> directions = new Dictionary<string, Tuple<string, IMethodWithType>>();


        private void IniteDirections()
        {

        }

        public ConstructEngine()
        {
            IniteDirections();
        }


        private NodeWithType ConstructWayFrom(VaribleWithType variA, VaribleWithType variB)
        {

        }


        private NodeWithType ConstructWayFrom(string typeA,string  typeB)
        {

        }


        List<IMethodWithType> GetMethodChain(string typeA, string typeB)
        {

        }





    }
}
