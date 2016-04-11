using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    internal static class QueuePreparer
    {
        internal static List<string> PrepareOuterString(string expression)
        {
            List<string> queue = new List<string>();
            List<char> stack = new List<char>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (Checker.IsNumber(expression[i]))
                {
                    try
                    {
                        if (Checker.IsNumberPart(expression[i - 1]))
                        {
                            Formatter.AppendSymbol(queue, expression[i]);
                        }
                        else
                        {
                            queue.Add(expression[i].ToString());
                        }
                    }
                    catch
                    {
                        queue.Add(expression[i].ToString());
                    }
                }
                else
                {
                    if (Checker.IsDot(expression[i]))
                    {
                        try
                        {
                            if (Checker.IsNumber(expression[i - 1]) && Checker.IsNumber(expression[i+1]))
                            {
                                Formatter.AppendSymbol(queue, expression[i]);
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                        catch
                        {
                            return Messenger.ErrorList("odd dot detected");
                        }
                    }
                    else
                    {
                        try
                        {
                            ResolveStack(queue, stack, expression[i]);
                        }
                        catch
                        {
                            return Messenger.ErrorList("brackets expected");
                        }
                    }
                }
            }
            try
            {
                MoveStack(queue, stack);
            }
            catch
            {
                return Messenger.ErrorList("brackets expected");
            }
            return queue;
        }
        private static void ResolveStack(List<string> queue, List<char> stack, char symbol)
        {
            if (stack.Count == 0)
            {
                stack.Add(symbol);
            }
            else
            {
                switch (symbol)
                {
                    case '(':
                        stack.Add(symbol);
                        break;
                    case ')':
                        MoveBracketsContent(queue, stack);
                        break;
                    case '+':
                        MoveStackTillSymbols(queue, stack, symbol, new char[] { '(' });
                        break;
                    case '-':
                        MoveStackTillSymbols(queue, stack, symbol, new char[] { '(' });
                        break;
                    case '*':
                        MoveStackTillSymbols(queue, stack, symbol, new char[] { '(', '+', '-' });
                        break;
                    case '/':
                        MoveStackTillSymbols(queue, stack, symbol, new char[] { '(', '+', '-' });
                        break;
                    case '^':
                        MoveStackTillSymbols(queue, stack, symbol, new char[] { '(', '+', '-', '/', '*' });
                        break;
                }
            }
        }
        private static void MoveStack(List<string> queue, List<char> stack)
        {
            if (stack.Count > 0)
            {
                for (int i = stack.Count - 1; i >= 0; i--)
                {
                    if (Checker.IsNotAny(stack[i], new char[] { '(', ')' }))
                    {
                        queue.Add(stack[i].ToString());
                        stack.RemoveAt(i);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }
        private static void MoveBracketsContent(List<string> queue, List<char> stack)
        {
            for (int i = stack.Count - 1; i >= 0; i--)
            {
                if (stack[i] != '(')
                {
                    if (stack[i] == ')')
                        throw new Exception();
                    queue.Add(stack[i].ToString());
                    stack.RemoveAt(i);
                }
                else
                {
                    stack.RemoveAt(i);
                    return;
                }
            }
            throw new Exception();
        }
        private static void MoveStackTillSymbols(List<string> queue, List<char> stack, char symbol, char[] elements)
        {
            for (int i = stack.Count - 1; i >= 0; i--)
            {
                if (Checker.IsNotAny(stack[i], elements))
                {
                    if (stack[i] == ')')
                        throw new Exception();
                    queue.Add(stack[i].ToString());
                    stack.RemoveAt(i);
                }
                else
                {
                    stack.Add(symbol);
                    return;
                }
            }
            stack.Add(symbol);
        }
    }
}
