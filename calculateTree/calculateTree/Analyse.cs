using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateTree
{
    /*
    class Analyse
    {

        class Expression
        {
            bool IsNumber;
            int Number = 0;
            char Operator;
            Expression left;
            Expression right;

            public Expression(int aNumber)
            {
                IsNumber = true;
                Number = aNumber;
                Operator = '0';
            }


            public Expression(char aoperator, Expression left, Expression right)
            {
                IsNumber = false;
                Operator = aoperator;
                this.left = left;
                this.right = right;
            }

            void PrintLISP()
            {
                if (IsNumber)
                {
                    Console.Write(Number);
                }
                else
                {
                    Console.Write("(" + Operator + " ");
                    left.PrintLISP();
                    Console.Write(" ");
                    right.PrintLISP();
                    Console.Write(")");
                }
            }

        }

        bool IsBlank(char ch)
        {
            return ch == ' ' || ch == '\t';
        }

        bool IsPrefix(string Stream, ref int pos, string Text)
        {
            int read = pos;
            while (IsBlank(Stream[read]))
            {
                ++read;
            }
            if (Stream.Substring(read, Text.Length) == Text)
            {
                pos = read + Text.Length;
                return true;
            }
            else
            {
                return false;
            }
        }

        Expression GetNumber(string Stream, ref int pos)
        {
            int Result = 0;
            bool GotNumber = false;

            int read = pos;

            while (IsBlank(Stream[read]))
            {
                ++read;
            }

            while (true)
            {
                char ch = Stream[read];

                if (ch >= '0' && ch <= '9')
                {
                    Result = Result * 10 + ch - '0';
                    GotNumber = true;
                    ++read;
                }
                else
                {
                    break;
                }
            }

            if (GotNumber)
            {
                pos = read;
                return new Expression(Result);
            }
            else
            {
                throw new Exception("此处需要表达式"); // 不仅仅是需要数字
            }
        }

        Expression GetTerm(string Stream,ref int pos)
        {
            try
            {
                return GetNumber(Stream,ref pos);
            }
            catch (Exception e)
            {
                int read = pos;
                // 检测左括号
                if (IsPrefix(Stream, ref read, "("))
                {
                    // 检测表达式
                    Expression Result = GetExp(Stream, ref read);
                    if (IsPrefix(Stream,ref read, ")"))
                    {
                        // 如果使用右括号结束，则返回结果
                        pos = read;
                        return Result;
                    }
                    else // 否则抛出异常
                    {
                        Result = null;
                        throw new Exception("此处需要右括号");
                    }
                }
                else
                {
                    throw e; // 这里要求GetNumber函数中抛出的异常信息为“需要表达式”，而非“需要数字”
                             // 或者：
                             // throw Exception(read, "此处需要数字或左括号");
                }
            }
        }

        Expression GetFactor(string Stream, ref int pos)
        {
            int read = pos;
            Expression Result = GetTerm(Stream,ref read);

            while (true)
            {
                char Operator = '0';
                if (IsPrefix(Stream,ref read, "*"))
                {
                    Operator = '*';
                }
                else if (IsPrefix(Stream,ref read, "/"))
                {
                    Operator = '/';
                }
                else
                {
                    break;
                }

                if (Operator != '0')
                {
                    // 如果是乘除号，则获得下一个Term
                    try
                    {
                        Result = new Expression(Operator, Result, GetTerm(Stream,ref read));
                    }
                    catch (Exception e)
                    {
                        Result = null;
                        throw e;
                    }
                }
            }
            pos = read;
            return Result;
        }

        Expression GetExp(string Stream, ref int pos)
        {
            int read = pos;
            Expression Result = GetFactor(Stream,ref read);

            while (true)
            {
                char Operator = '0';
                if (IsPrefix(Stream,ref read, "+"))
                {
                    Operator = '+';
                }
                else if (IsPrefix(Stream,ref read, "-"))
                {
                    Operator = '-';
                }
                else
                {
                    break;
                }

                if (Operator != '0')
                {
                    // 如果是加减号，则获得下一个Factor
                    try
                    {
                        Result = new Expression(Operator, Result, GetFactor(Stream,ref read));
                    }
                    catch (Exception e)
                    {
                        Result = null;
                        throw e;
                    }
                }
            }
            pos = read;
            return Result;
        }





    }
*/

}
