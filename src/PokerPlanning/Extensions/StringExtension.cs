using System.Text.RegularExpressions;

namespace PokerPlanning.Extensions
{
    public static class StringExtension
    {

        public static Regex DigitsOnly = new Regex(@"[^\d]");

        public static string ToDigitsOnly(this string value)
        {
            return DigitsOnly.Replace(value, "");
        }
    }
}
