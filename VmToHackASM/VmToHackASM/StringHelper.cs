using System.Text.RegularExpressions;

namespace VmToHackASM
{
    public static class StringHelper
    {
        /// <summary>
        /// Removes '//' comments from a string.
        /// </summary>
        /// <param name="text">String to remove comments from</param>
        /// <returns>Uncommented string</returns>
        public static string RemoveComments(string text)
        {
            return Regex.Replace(text, "//.*", "").Trim();
        }
    }
}
