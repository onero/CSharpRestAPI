using System;
using System.Text.RegularExpressions;

namespace RestAppBLL
{
    public static class InputValidation
    {
        // Filter numbers in string
        private static readonly Regex RegexNumbers = new Regex("^(?=.*[0-9])");

        /// <summary>
        ///     Name does not contain numbers
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsValidName(string name)
        {
            return !RegexNumbers.IsMatch(name);
        }

        /// <summary>
        ///     Parse integer from user input
        /// </summary>
        /// <param name="selection"></param>
        /// <returns>true if input is valid number, false if not</returns>
        public static bool ParseIntegerFromConsole(out int selection)
        {
            return int.TryParse(Console.ReadLine(), out selection);
        }

        public static bool SelectionOverIndex(int selection, int maxIndex)
        {
            return selection > maxIndex;
        }

        public static bool IsSelectionUnderIndex(int selection)
        {
            return selection < 1;
        }
    }
}