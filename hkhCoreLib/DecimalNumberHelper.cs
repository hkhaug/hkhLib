using System.Globalization;

namespace hkhCoreLib
{
    /// <summary>
    /// Utilities for parsing decimal numbers.
    /// </summary>
    public static class DecimalNumberHelper
    {
        private const string spaceStr = " ";
        private const string pointStr = ".";
        private const char comma = ',';
        private const char point = '.';

        /// <summary>
        /// Try to parse a string into a double. The string does not need to include a decimal character.
        /// The string may use either comma or point as decimal character. The string may use either a
        /// space, a comma, a point or nothing as thousands separator, as long as the thousands separator
        /// and the decimal character are not identical.
        /// </summary>
        /// <param name="number">The number string to be parsed.</param>
        /// <param name="result">The parsed result, or 0.0 if parsing failed.</param>
        /// <returns>true for successfull parsing or false if error.</returns>
        public static bool TryParse(string number, out double result)
        {
            result = 0.0d;
            if (string.IsNullOrWhiteSpace(number)) return false;
            string num = number.Replace(spaceStr, string.Empty);
            int lastComma = num.LastIndexOf(comma);
            int lastPoint = num.LastIndexOf(point);
            if (lastPoint < 0)
            {
                if (lastComma < 0)
                {
                    // We have either an integer number, or error.
                    return double.TryParse(num, NumberStyles.AllowLeadingSign, NumberFormatInfo.InvariantInfo,
                        out result);
                }
                // We have either a number with a decimal comma but no point, or error.
                return double.TryParse(num.Replace(comma, point),
                    NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo,
                    out result);
            }
            if (lastComma < 0)
            {
                // We have either a number with a decimal point but no comma, or error.
                return double.TryParse(num, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                    NumberFormatInfo.InvariantInfo, out result);
            }
            if (lastComma < lastPoint)
            {
                // We have either a number with a decimal point and comma as thousands separator, or error.
                return double.TryParse(num,
                    NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
                    NumberFormatInfo.InvariantInfo, out result);
            }
            // We have either a number with a decimal comma and point as thousands separator, or error.
            return double.TryParse(num.Replace(pointStr, string.Empty).Replace(comma, point),
                NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands,
                NumberFormatInfo.InvariantInfo, out result);
        }
    }
}
