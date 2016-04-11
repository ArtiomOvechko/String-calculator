using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    internal static class Formatter
    {
        internal static string FormatExpression(string expression)
        {
            expression=expression.Replace(',', '.');
            expression=expression.Replace('\\', '/');
            return expression;
        }
        internal static void AppendSymbol(List<string> basis, char symbol)
        {
            basis[basis.Count - 1] += symbol.ToString();
        }
    }
}
