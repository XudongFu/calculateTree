using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree.free
{
    class Analyse
    {
        Stack<string> result = new Stack<string>();

        Stack<string> operate = new Stack<string>();

        HashSet<char> opp = new HashSet<char>{ '(','-','+','*','/' };

        //https://blog.csdn.net/zsuguangh/article/details/6280863
        public void prase(string express)
        {
            if (string.IsNullOrWhiteSpace(express))
                throw new ArgumentException();
            if (!express.Any(p => p == '='))
            {
                throw new ArgumentException("该表达式不是一个等式");
            }
            int pos = 0;
            express = express.Replace(" ","");
            string nextterm = readNextTerm(express,ref pos);
            while (!string.IsNullOrWhiteSpace(express))
            {
                if (float.TryParse(nextterm, out float number))
                {
                    // float number;
                    result.Push(nextterm);
                }
                if (nextterm=="(")
                {
                    operate.Push(nextterm);

                }

                if (opp.Any(p=>p.ToString().Equals(nextterm)))
                {

                }

                nextterm = readNextTerm(express, ref pos);
            }

        }


        string readNextTerm(string express,ref int pos)
        {
            if (pos < express.Length)
            {
                if (opp.Contains(express[pos]))
                {
                    pos++;
                    return express[pos].ToString();
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
                throw new Exception("TODO");
            }
            else
            {
                return "";
            }
        }



    }
}
