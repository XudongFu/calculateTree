using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free.function
{
    interface IMethodWithType:ICalculateMethod
    {
        /// <summary>
        /// 获取指定参数的参数类型
        /// </summary>
        /// <param name="paramIndex"></param>
        /// <returns></returns>
        string GetParamType(int paramIndex);

        /// <summary>
        /// 当前方法得到的变量类型
        /// </summary>
        /// <returns></returns>
        string GetCurrentNodeType();

        /// <summary>
        /// 是否具有反向操作
        /// </summary>
        /// <param name="paramIndex"></param>
        /// <returns></returns>
        bool HasUnOpperation(int paramIndex);

    }
}
