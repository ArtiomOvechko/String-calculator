using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public static class Calculator
    {
        public static string Calculate(string expression)
        {
            expression=Formatter.FormatExpression(expression);
            List<string> queue = QueuePreparer.PrepareOuterString(expression);
            if (queue[0].Contains("Error"))
            {
                return queue[0];
            }
            else
            {
                return CalculationProcess(queue);
            }
        }
        private static string CalculationProcess(List<string> queue)
        {
            List<double> stack = new List<double>();
            stack.Add(0);
            foreach(string element in queue)
            {
                if(Checker.IsNumber(element))
                {
                    stack.Add(double.Parse(element));
                }
                else
                {
                    try
                    {
                        if (Checker.IsAny(element, new string[] { "/", "*", "+", "-", "^" }))
                        {
                            stack[stack.Count - 2] = Operation(stack[stack.Count - 2], stack[stack.Count - 1], element);
                            stack.RemoveAt(stack.Count - 1);
                        }
                        else
                        {
                            return Messenger.ErrorList("incorrect symbol in expression").First();
                        }
                    }
                    catch
                    {
                        return Messenger.ErrorList("IndexOutOfStackRange").First();
                    }
                }
            }
            return stack.Last().ToString();
        }
        private static double Operation(double left, double right, string operatorKey)
        {
            switch(operatorKey)
            {
                case "+":
                    left += right;
                    break;
                case "-":
                    left -= right;
                    break;
                case "*":
                    left *= right;
                    break;
                case "/":
                    left /= right;
                    break;
                case "^":
                    left = Math.Pow(left, right);
                    break;
            }
            return left;
        }
    }
}
