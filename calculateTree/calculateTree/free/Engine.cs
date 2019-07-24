using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    public class Engine
    {
        private Dictionary<string, Varible> varibleDic = new Dictionary<string, Varible>();
        private Analyse analyse = new Analyse();

        public void AddVaribles(string name, Func<dynamic> getValue)
        {
            if (string.IsNullOrEmpty(name) || getValue == null)
                throw new ArgumentNullException();
            if (varibleDic.ContainsKey(name))
                throw new ArgumentException(string.Format(" varible its name {0} exists", name));
            varibleDic.Add(name, new Varible(name, getValue));
        }

        public void deleteVarible(string name)
        {
            varibleDic.Remove(name);
            //TODO 需要冲其他变量的node中删除
        }

        public void AddContantVarible(string name, dynamic value)
        {
            if (string.IsNullOrEmpty(name) || value == null)
                throw new ArgumentNullException();
            if (varibleDic.ContainsKey(name))
                throw new ArgumentException(string.Format(" varible its name {0} exists", name));
            varibleDic.Add(name, new Varible(name, value));
        }


        public string GetVaribleDescription(string name)
        {
            if (!varibleDic.ContainsKey(name))
                return string.Format("名为：{0}的变量不存在", name);
            StringBuilder builder = new StringBuilder();
            var res = varibleDic[name].GetAllDescription();
            return res;
        }


        /// <summary>
        /// 当前解析的表达式仅限于等式，还不支持不等式
        /// </summary>
        /// <param name="expression"></param>
        public string Parse(string expression)
        {
            Node node;
            StringBuilder builder = new StringBuilder();
#if DEBUG
            node = analyse.Prase(expression);
#else
            try
            {
                node = analyse.Prase(expression);
            }
            catch (Exception e)
            {
                return e.Message;
            }
#endif
            List<string> varibles = node.GetAllRequiredVarible();
            if (varibles != null)
            {
                if (varibles.Count > 0)
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
                                if (temp.GetAllRequiredVarible().Count == 0)
                                {
                                    var res = temp.InvokeMethod();
                                    vari.SetDefaultValue(res);
                                    Node final = new Node(vari);
                                    vari.AddCalculateTree(expression, final);
                                    //vari.AddCalculateTree(expression, temp);
                                    builder.AppendLine(string.Format("{0}={1}", vari.name, res));
                                }
                                else
                                {
                                    vari.AddCalculateTree(expression, temp);
                                }
                            }
                        }
                        catch (CannnotGetNodeFromParam e)
                        {
                            throw e;
                        }
                    });
                }
                else
                {
                    builder.AppendLine(string.Format("{0}", node.InvokeMethod()));
                }
            }
            return builder.ToString();
        }

        internal string PrintDebugInfo()
        {
            StringBuilder builder = new StringBuilder();
            return builder.ToString();
        }

    }
}
