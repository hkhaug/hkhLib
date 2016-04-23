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
