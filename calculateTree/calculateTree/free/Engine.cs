using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    public class Engine
    {
        Dictionary<string, Varible> varibleDic = new Dictionary<string, Varible>();


        void AddVaribles(string name,Func<dynamic> getValue)
        {
            if (string.IsNullOrEmpty(name) || getValue == null)
                throw new ArgumentNullException();
            if (varibleDic.ContainsKey(name))
                throw new ArgumentException(string.Format(" varible its name {0} exists", name));
            varibleDic.Add(name,new Varible(name,getValue));
        }

        void deleteVarible(string name)
        {
            varibleDic.Remove(name);
            //TODO 需要冲其他变量的node中删除
        }

        void AddContantVarible(string name,dynamic value)
        {
            if (string.IsNullOrEmpty(name) || value == null)
                throw new ArgumentNullException();
            if (varibleDic.ContainsKey(name))
                throw new ArgumentException(string.Format(" varible its name {0} exists", name));
            varibleDic.Add(name, new Varible(name, value));
        }


        /// <summary>
        /// 当前解析的表达式仅限于等式，还不支持不等式
        /// </summary>
        /// <param name="expression"></param>
        void Parse(string expression)
        {
            Node node = Exp2Node(expression);
            List<string> varibles = node.GetAllVarible();
            if (varibles != null && varibles.Count > 0)
            {
                varibles.ForEach(p =>
                {
                    try
                    {
                        Varible vari;
                        if (varibleDic.ContainsKey(p))
                        {
                            vari = varibleDic[p];
                        }
                        else
                        {
                            vari = new Varible(p);
                            varibleDic.Add(p, vari);
                        }
                        Node temp = node.GetNodeFromParam(p);
                        if (temp != null)
                        {
                            vari.AddCalculateTree(expression, temp);
                        }
                    }
                    catch (CannnotGetNodeFromParam e)
                    {


                    }
                });
            }
        }

        private Node Exp2Node(string express)
        {
            return new Analyse().Prase(express);
        }

        public string PrintDebugInfo()
        {
            StringBuilder builder = new StringBuilder();



            return builder.ToString();
        }
        
    }
}
