using System;
using System.Collections.Generic;

namespace hkhCoreLib
{
    /// <summary>
    /// Utilities and extensions for the string class.
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// Always return a non-null string (replace null with empty).
        /// </summary>
        /// <param name="value">Value to replace empty.</param>
        /// <returns>If empty, the passed string, otherwise the unchanged original value.</returns>
        public static string EmptyIfNull(this string value)
        {
            return value ?? string.Empty;
        }

        /// <summary>
        /// Set the destination value to that of the source, but only if the destination
        /// was null or string.Empty, or consisted only of white space.
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <returns>True if the destination was modified.</returns>
        public static string IfMissingReplaceWith(this string destination, string source)
        {
            return string.IsNullOrWhiteSpace(destination) ? source : destination;
        }

        /// <summary>
        /// Extract the leftmost part of a string.
        /// </summary>
        /// <param name="length">The maximum length of the returned string.</param>
        /// <returns>The leftmost part of the source string.</returns>
        public static string Left(this string s, int length)
        {
            if (null == s) return string.Empty;
            return s.Length > length ? s.Substring(0, length) : s;
        }

        /// <summary>
        /// Extract the rightmost part of a string.
        /// </summary>
        /// <param name="length">The maximum length of the returned string.</param>
        /// <returns>The rightmost part of the source string.</returns>
        public static string Right(this string s, int length)
        {
            if (null == s) return string.Empty;
            return s.Length > length ? s.Substring(s.Length - length) : s;
        }

        /// <summary>
        /// Split a long string into chunks of specified length.
        /// </summary>
        /// <param name="input">The long string to be split.</param>
        /// <param name="chunkSize">Length of each chunk. The last chunk may be shorter.</param>
        /// <returns>An IEnumerable of string chunks.</returns>
        public static IEnumerable<string> SplitIntoChunks(this string input, int chunkSize)
        {
            int len = input.Length;
            for (int pos = 0; pos < len; pos += chunkSize)
            {
                string chunk = input.Substring(pos, Math.Min(chunkSize, len - pos));
                yield return chunk;
            }
        }
    }
}
