﻿using calculateTree.free.method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    class Analyse
    {
        List<string> result = new List<string>();

        Stack<string> operate = new Stack<string>();

        HashSet<char> opp = new HashSet<char>{ '-','+','*','/' };

        HashSet<string> opper = new HashSet<string> { "-", "+", "*", "/" };

        HashSet<string> functionName = new HashSet<string>();

        HashSet<string> varibles = new HashSet<string>();

        Dictionary<string, int> level = new Dictionary<string, int>() { {",",0 }, {"+",1 },{"-",1 } , { "*", 2 }, { "/", 2 } };

        //https://blog.csdn.net/zsuguangh/article/details/6280863


        private void Clear()
        {
            result.Clear();
            operate.Clear();
            functionName.Clear();
            varibles.Clear();
        }


        public static void main()
        {
            string exp = "(23+34*45/(5+6+7))=9";
            string exp2 = "1+2*sin(4+5)";
            string exp3 = "1+2*pow(4,(6+7.65*var)*2)=6";
            string exp4 = "0.56";
            Node ee = new Analyse().Prase(exp);
            //ee.ForEach(p => Console.Write(p + " "));
        }
        public Node toNode(List<string> terms)
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
        }        Node ReadOneNode(List<string> terms, Node parentNode, ref int index)
        {
            string curr = terms[index++];
            if (opper.Contains(curr))
            {
                Varible vari = new Varible();
                List<Node> param = new List<Node>();
                ICalculateMethod method = CalculateFactory.GetMethod(curr);
                for (int i=0;i<method.GetParamCount();i++)
                {
                    param.Add(ReadOneNode(terms,parentNode,ref index));
                }
                Node res = new Node(parentNode,vari, param, method);
                return res;
            }
            if (functionName.Contains(curr))
            {
                int paramCount = 1;
                while (terms[index++]==",")
                {
                    paramCount += 1;
                }
                Varible vari = new Varible();
                List<Node> param = new List<Node>();
                ICalculateMethod method = CalculateFactory.GetMethod(curr);
                if (method.GetParamCount() != paramCount)
                {
                    throw new Exception(string.Format("{0}的参数数量不正确，期待{1}的变量",curr,method.GetParamCount()));
                }
                for (int i = 0; i < method.GetParamCount(); i++)
                {
                    param.Add(ReadOneNode(terms, parentNode, ref index));
                }
                Node res = new Node(parentNode, vari, param, method);
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
        }        Node Prase(string express)
        {
            if (string.IsNullOrWhiteSpace(express))
            {
                throw new ArgumentNullException();
            }
            if (!express.Contains("="))
                throw new ArgumentException("表达式必须是一个等式");
            string[] exp = express.Split('=');
            if (exp.Length!=2)
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
            return new Node(null, vari, param, method);
        }        Node GetNode(string express)
        {
            return toNode(GetHalfExpress(express));
        }
        public List<string> GetHalfExpress(string express)
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


        bool IsFunction(string express,int pos)
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



        string readNextTerm(string express,ref int pos)
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
                    while (express[pos] >= 'a' && express[pos] <= 'z' || express[pos] >= 'A' && express[pos] <= 'Z')
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
