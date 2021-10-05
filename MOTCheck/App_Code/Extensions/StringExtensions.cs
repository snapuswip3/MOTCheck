using System.Linq;

namespace MOTCheck.Extensions
{
    public static class StringExtensions
    {
        private static readonly char[] VOWELS = { 'a', 'e', 'i', 'o', 'u' };

        public static string PrefixNoun(this string a_sInput)
        {
            return (VOWELS.Contains(a_sInput.Trim().ToLower().FirstOrDefault()) ? "an " : "a ") + a_sInput;
        }

        public static string TrimTrailing(this string a_sInput, string a_sSuffix)
        {
            return a_sInput.EndsWith(a_sSuffix) ? a_sInput.Substring(0, a_sInput.Length - a_sSuffix.Length) : a_sInput;
        }
    }
}