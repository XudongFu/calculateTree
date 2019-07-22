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

        Dictionary<string, int> level = new Dictionary<string, int>() { {"+",1 },{"-",1 } , { "*", 2 }, { "/", 2 } };

        //https://blog.csdn.net/zsuguangh/article/details/6280863


        private void Clear()
        {
            result.Clear();
            operate.Clear();
        }


        public static void main()
        {
            string exp = "(23+34*45/(5+6+7))";
            List<string> ee = new Analyse().prase(exp);
            ee.ForEach(p=>Console.Write(p+" "));
        }

        public List<string> prase(string express)
        {
            Clear();
            if (string.IsNullOrWhiteSpace(express))
                throw new ArgumentException();
            //if (!express.Any(p => p == '='))
            //{
            //    throw new ArgumentException("该表达式不是一个等式");
            //}
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
                if (opp.Any(p => p.ToString().Equals(nextterm)) || false)
                {

                    while (operate.Count != 0 && operate.Peek() != "(" && level[operate.Peek()]  >= level[nextterm] )
                    {
                        result.Add(operate.Pop());
                    }
                    operate.Push(nextterm);

                }
                nextterm = readNextTerm(express, ref pos);

            }
            while (operate.Count != 0)
            {
                result.Add(operate.Pop());
            }
            return result;
        }


        string readNextTerm(string express,ref int pos)
        {
            if (pos < express.Length)
            {
                if (opp.Contains(express[pos]))
                {
                    return express[pos++].ToString();
                }

                if (express[pos] >= '0' && express[pos] <= '9' || express[pos]=='.')
                {
                    int res = express[pos] - '0';
                    pos++;
                    while (express[pos] >= '0' && express[pos] <= '9')
                    {
                        res = res * 10 + express[pos] - '0';
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
                }
                if (new string[] { "(",")"}.Contains(express[pos].ToString()))
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
