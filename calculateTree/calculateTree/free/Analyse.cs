using calculateTree.free.method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    internal class Analyse
    {
        List<string> result = new List<string>();

        Stack<string> operate = new Stack<string>();

        HashSet<char> opp = new HashSet<char>{ '-','+','*','/' };

        HashSet<string> opper = new HashSet<string> { "-", "+", "*", "/" };

        HashSet<string> functionName = new HashSet<string>();

        HashSet<string> varibles = new HashSet<string>();

        Dictionary<string, int> level = new Dictionary<string, int>() { {",",0 }, {"+",1 },{"-",1 } , { "*", 2 }, { "/", 2 } };

        private void Clear()
        {
            result.Clear();
            operate.Clear();
            functionName.Clear();
            varibles.Clear();
        }
        private Node toNode(List<string> terms)
        {
            if (terms != null && terms.Count == 0)
            {
                Varible vari = new Varible();
                vari.SetDefaultValue(0);
                return new Node(vari);
            }
            terms.Reverse();
            int index = 0;
            return ReadOneNode(terms, null, ref index); ;
        }

        private Node ReadOneNode(List<string> terms, Node parentNode, ref int index)
        {
            string curr = terms[index++];
            if (opper.Contains(curr))
            {
                Varible vari = new Varible();
                Node res = new Node(vari);
                List<Node> param = new List<Node>();
                ICalculateMethod method = CalculateFactory.GetMethod(curr);
                for (int i=0;i<method.GetParamCount();i++)
                {
                    param.Add(ReadOneNode(terms,res,ref index));
                }
                param.Reverse();
                res.SetParams(parentNode, param, method);
                return res;
            }
            if (functionName.Contains(curr))
            {
                int paramCount = 1;
                while (terms[index]==",")
                {
                    index++;
                    paramCount += 1;
                }
                Varible vari = new Varible();
                Node res = new Node(vari);
                List<Node> param = new List<Node>();
                ICalculateMethod method = CalculateFactory.GetMethod(curr);
                if (method.GetParamCount() != paramCount)
                {
                    throw new Exception(string.Format("{0}的参数数量不正确，期待{1}个数目的变量",curr,method.GetParamCount()));
                }
                for (int i = 0; i < method.GetParamCount(); i++)
                {
                    param.Add(ReadOneNode(terms, res, ref index));
                }
                param.Reverse();
                res.SetParams(parentNode, param, method);
                return res;

            }
            if (varibles.Contains(curr))
            {
                Varible vari = new Varible(curr);
                Node res = new Node(vari);
                return res;
            }

            if (float.TryParse(curr, out float val))
            {
                Varible vari = new Varible(curr,val);
                Node res = new Node(vari);
                return res;
            }
            else
            {
                throw new Exception("unexpected condition");
            }
        }


        public Node Prase(string express)
        {
            if (string.IsNullOrWhiteSpace(express))
            {
                throw new ArgumentNullException();
            }
            if (!express.Contains("="))
            {
                Node res = GetNode(express);
                if (res.GetAllRequiredVarible().Count!=0)
                {
                    throw new ArgumentException("表达式不是等式且包含未知变量");
                }
                return res;
            }
            else
            {
                string[] exp = express.Split('=');
                if (exp.Length != 2)
                {
                    throw new ArgumentException("表达式包含多个等号");
                }
                Node lefNode = GetNode(exp[0]);
                Node rightNode = GetNode(exp[1]);
                List<Node> param = new List<Node>();
                param.Add(lefNode);
                param.Add(rightNode);
                Varible vari = new Varible();
                vari.SetDefaultValue(0);
                ICalculateMethod method = CalculateFactory.GetMethod("-");
                Node res = new Node(vari);
                lefNode.SetParent(res);
                rightNode.SetParent(res);
                res.SetParams(null,param,method);
                return res;
            }
        }

        private Node GetNode(string express)
        {
            return toNode(GetHalfExpress(express));
        }
        private List<string> GetHalfExpress(string express)
        {
            Clear();
            if (string.IsNullOrWhiteSpace(express))
                throw new ArgumentException();
            int pos = 0;
            express = express.Replace(" ","");
            string nextterm = readNextTerm(express,ref pos);
            while (!string.IsNullOrWhiteSpace(nextterm))
            {
                if (float.TryParse(nextterm, out float number))
                {
                    // float number;
                    result.Add(nextterm);
                }
                if (nextterm=="(")
                {
                    operate.Push(nextterm);

                }
                if (nextterm == ",")
                {
                    operate.Push(nextterm);
                }
                if (nextterm == ")")
                {
                    bool see = false;
                    while (operate.Count() != 0  && operate.Peek()!="(")
                    {
                        result.Add(operate.Pop());
                    }
                    if (operate.Count() != 0  && operate.Peek() == "(")
                        operate.Pop();
                    else
                    if (operate.Count() == 0)
                        throw new Exception("表达式有误");
                }
                if (opp.Any(p => p.ToString().Equals(nextterm)))
                {
                    while (operate.Count != 0 && operate.Peek() != "(" && level[operate.Peek()] >= level[nextterm])
                    {
                        result.Add(operate.Pop());
                    }
                    operate.Push(nextterm);
                }
                //当前为函数名称
                if (nextterm[0] >= 'a' && nextterm[0] <= 'z' || nextterm[0] >= 'A' && nextterm[0] <= 'Z')
                {
                    if (IsFunction(express, pos))
                    {
                        functionName.Add(nextterm);
                        operate.Push(nextterm);
                    }
                    else
                    {
                        varibles.Add(nextterm);
                        result.Add(nextterm);
                    }
                }
                nextterm = readNextTerm(express, ref pos);
            }
            while (operate.Count != 0)
            {
                result.Add(operate.Pop());
            }
            return result;
        }


        private bool IsFunction(string express,int pos)
        {
            if (pos < express.Length)
            {
                if (express[pos]=='(')
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private string readNextTerm(string express,ref int pos)
        {
            if (pos < express.Length)
            {
                if (opp.Contains(express[pos]))
                {
                    return express[pos++].ToString();
                }

                if (express[pos] >= '0' && express[pos] <= '9' )
                {
                    string res = express[pos].ToString();
                    pos++;
                    while (pos < express.Length && (express[pos] >= '0' && express[pos] <= '9' || express[pos] == '.'))
                    {
                        res += express[pos].ToString();
                        pos++;
                    }
                    return res.ToString();
                }
                if (express[pos] >= 'a' && express[pos] <= 'z' || express[pos] >= 'A' && express[pos] <= 'Z')
                {
                    string res = express[pos].ToString();
                    pos++;
                    while (pos < express.Length &&  (express[pos] >= 'a' && express[pos] <= 'z' || express[pos] >= 'A' && express[pos] <= 'Z'))
                    {
                        res += express[pos].ToString();
                        pos++;
                    }
                    return res;
                }
                if (new string[] { "(",")",","}.Contains(express[pos].ToString()))
                {
                    return express[pos++].ToString();
                }
                throw new Exception("TODO");
            }
            else
            {
                return "";
            }
        }
    }
}
