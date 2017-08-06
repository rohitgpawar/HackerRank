using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class BalancedBrackets
    {
        static string CheckValidParenthesis(string expression)
        {
            Stack<char> openingParenthesis = new Stack<char>();
            bool isValid = true;
            foreach (char parenthesis in expression)
            {
                char lastOpenedBrace;
                if (parenthesis == '(' || parenthesis == '[' || parenthesis == '{')
                {
                    openingParenthesis.Push(parenthesis);
                }
                else if (openingParenthesis.Count() > 0) // } ) ]
                {
                    switch (parenthesis)
                    {
                        case ')':
                            lastOpenedBrace = openingParenthesis.Peek();
                            if (lastOpenedBrace != '(')
                            {
                                isValid = false;
                            }
                            else
                                openingParenthesis.Pop();
                            break;
                        case ']':
                            lastOpenedBrace = openingParenthesis.Peek();
                            if (lastOpenedBrace != '[')
                            {
                                isValid = false;
                            }
                            else
                                openingParenthesis.Pop();
                            break;
                        case '}':
                            lastOpenedBrace = openingParenthesis.Peek();
                            if (lastOpenedBrace != '{')
                            {
                                isValid = false;
                            }
                            else
                                openingParenthesis.Pop();
                            break;
                    }
                }
                else
                    isValid = false;

                if (!isValid)
                    return "NO";
            }
            if (isValid && openingParenthesis.Count() == 0)
                return "YES";
            return "NO";
        }

        public static void BalancedBracketsMain()
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int a0 = 0; a0 < t; a0++)
            {
                string expression = Console.ReadLine();
                Console.WriteLine(CheckValidParenthesis(expression));
            }
        }
    }
}
