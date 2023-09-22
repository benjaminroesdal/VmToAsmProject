using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VmToHackASM
{
    public static class StringHelper
    {
        public static string RemoveComments(string text)
        {
            return Regex.Replace(text, "//.*", "").Trim();
        }
    }
}
