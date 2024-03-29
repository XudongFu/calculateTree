﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{

    /// <summary>
    /// 计算核心，穿透各种变量之间的关系，得到你想要得到的变量
    /// </summary>
    public class CalculateEngine
    {
        internal Dictionary<string, Varible> varibleDic = new Dictionary<string, Varible>();

        private Analyse analyse;

        private HashSet<string> knownVaribles = new HashSet<string>();

        private HashSet<string> notKnowVaribles = new HashSet<string>();

        public CalculateEngine()
        {
            analyse = new Analyse(this);
        }


        public void SetVaribles(string name, Func<dynamic> getValue)
        {
            if (string.IsNullOrEmpty(name) || getValue == null)
                throw new ArgumentNullException();
            if (varibleDic.ContainsKey(name))
                varibleDic[name].SetGetValueMethod(getValue);
            else
                varibleDic.Add(name, new Varible(name, getValue));
            GetAllInfo();
        }

        public void SetContantVarible(string name, dynamic value)
        {
            if (string.IsNullOrEmpty(name) || value == null)
                throw new ArgumentNullException();
            if (varibleDic.ContainsKey(name))
                varibleDic[name].SetDefaultValue(value);
            else
                varibleDic.Add(name, new Varible(name, value));
            GetAllInfo();
        }

        public string GetVaribleDescription(string name)
        {
            if (!varibleDic.ContainsKey(name))
                return string.Format("名为：{0}的变量不存在", name);
            StringBuilder builder = new StringBuilder();
            var res = varibleDic[name].GetAllDescription();
            return res;
        }

        public dynamic GetVarileValue(string name)
        {
            if (!varibleDic.ContainsKey(name))
                throw new Exception(string.Format("名为：{0}的变量不存在", name));
            if (knownVaribles.Contains(name))
            {
                return varibleDic[name].GetValue();
            }
            throw new Exception(string.Format("名为：{0}的变量条件不足无法计算", name));
        }

        private void GetAllInfo()
        {
            Dictionary<string, Dictionary<string, List<string>>> allInfo = new Dictionary<string, Dictionary<string, List<string>>>();
            varibleDic.ToList().ForEach(p =>
            {
                allInfo.Add(p.Key, p.Value.GetCalculateInfo());
                if (p.Value.IsKnown)
                    knownVaribles.Add(p.Key);
            });
            //包含所有变量的计算步骤信息
            List<Tuple<string, string>> steps = GetCalculateStep(allInfo, out HashSet<string> knowVari, out HashSet<string> notknowVari);
            steps.ForEach(p =>
            {
                //对于已经修复过的节点不再进行处理
                if (!this.knownVaribles.Contains(p.Item1) && !varibleDic[p.Item1].IsKnown)
                {
                    varibleDic[p.Item1].ExecuteKey = p.Item2;
                    varibleDic[p.Item1].RepireNode(h => varibleDic[h].GetExecute());
                    varibleDic[p.Item1].Execute();
                }
            });
            knowVari.ToList().ForEach(p => knownVaribles.Add(p));
            notKnowVaribles = notknowVari;
        }

        private List<Tuple<string, string>> GetCalculateStep(Dictionary<string, Dictionary<string, List<string>>> allInfo, out HashSet<string> outKnowVari, out HashSet<string> outNotKnowVari)
        {
            HashSet<string> knownVari = new HashSet<string>(knownVaribles);
            HashSet<string> allVari = new HashSet<string>(varibleDic.Keys);
            List<Tuple<string, string>> steps = new List<Tuple<string, string>>();
            foreach (var vari in allInfo.ToList())
            {
                if (knownVaribles.Contains(vari.Key))
                    continue;
                foreach (var node in vari.Value.ToList())
                {
                    if (node.Value.Count == 0)
                    {
                        knownVari.Add(vari.Key);
                        allVari.Remove(vari.Key);
                        steps.Add(new Tuple<string, string>(vari.Key, node.Key));
                    }
                    else if (!node.Value.Any(p=>!knownVari.Contains(p)))
                    {
                        knownVari.Add(vari.Key);
                        allVari.Remove(vari.Key);
                        steps.Add(new Tuple<string, string>(vari.Key, node.Key));
                    }
                }
            }
            int lastKnownSize = 0;
            do
            {
                lastKnownSize = knownVari.Count;
                List<string> notKnowVari = new List<string>();
                notKnowVari = allVari.Except(knownVari).ToList();
                List<string> reachableVari = new List<string>();
                foreach (var vari in notKnowVari)
                {
                    foreach (var requireVariInfo in allInfo[vari].ToList())
                    {
                        if (requireVariInfo.Value.Except(knownVari).Count() == 0)
                        {
                            steps.Add(new Tuple<string, string>(vari, requireVariInfo.Key));
                            reachableVari.Add(vari);
                            break;
                            //TODO 暂时不处理一个变量可以有多个求解路径的问题
                        }
                    }
                }
                reachableVari.ForEach(p => knownVari.Add(p));

            } while (lastKnownSize != knownVari.Count);

            outKnowVari = knownVari;
            outNotKnowVari = new HashSet<string>(allVari.Except(knownVaribles).ToList());
            return steps;
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
                                    vari.AddCalculateTree(expression, temp);
                                    vari.ExecuteKey = expression;
                                    vari.Execute();
                                    builder.AppendLine(vari.GetAllDescription());
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
                    if (node.self.IsKnown || node.self.IsTemp)
                        builder.AppendLine(string.Format("{0}", node.InvokeMethod()));
                    else
                        builder.AppendLine(string.Format("{0}", GetVaribleDescription(node.self.name)));
                }
            }
            GetAllInfo();
            var resp= builder.ToString();
            if (resp == "")
            {
                resp = "ok";
            }
            return resp;
        }


        internal void AddVari(string name,Varible vari)
        {
            varibleDic[name] = vari;
        }


        internal string PrintDebugInfo()
        {
            StringBuilder builder = new StringBuilder();
            return builder.ToString();
        }

        public void Clear()
        {
            varibleDic.Clear();
            knownVaribles.Clear();
            notKnowVaribles.Clear();
        }



    }
}
