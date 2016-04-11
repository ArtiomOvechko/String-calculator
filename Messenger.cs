using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    internal static class Messenger
    {
        internal static List<string> ErrorList(string errorMessage)
        {
            List<string> errorList = new List<string>();
            errorList.Add("Error: "+errorMessage);
            return errorList;
        }
    }
}
