using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    internal static class Checker
    {
        internal static bool IsNumber(char symbol)
        {
            string stringSymbol = "" + symbol;
            double checker;
            return double.TryParse(stringSymbol, out checker);
        }
        internal static bool IsNumber(string element)
        {
            double checker;
            return double.TryParse(element, out checker);
        }
        internal static bool IsDot(char symbol)
        {
            return symbol == '.' ? true : false;
        }
        internal static bool IsNumberPart(char symbol)
        {
            return IsNumber(symbol) || IsDot(symbol) ? true : false;
        }
        internal static bool IsNotAny(char symbol, char[] examples)
        {
            foreach(char example in examples)
            {
                if (symbol == example)
                    return false;
            }
            return true;
        }
        internal static bool IsAny(string element, string[] examples)
        {
            foreach (string example in examples)
            {
                if (element == example)
                    return true;
            }
            return false;
        }
    }
}
