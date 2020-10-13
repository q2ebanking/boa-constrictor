using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace Boa.Constrictor.Utilities
{
    /// <summary>
    /// Provides static methods for handling names.
    /// </summary>
    public static class Names
    {
        /// <summary>
        /// Returns a unique name for a file using a UTC timestamp and an optional suffix.
        /// Format: {name}_{timestamp}(_{suffix})?(_{thread})?
        /// Warning: Do NOT include a file extension in the name!
        /// </summary>
        /// <param name="name">Base name for the file.</param>
        /// <param name="suffix">An optional suffix.</param>
        /// <returns></returns>
        public static string ConcatUniqueName(string name, string suffix = null)
        {
            string trimmed = name.Trim();
            string thread = GetCurrentThreadName();
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");

            string unique = $"{trimmed}_{timestamp}";

            if (!string.IsNullOrWhiteSpace(suffix))
                unique += '_' + suffix;

            if (!string.IsNullOrWhiteSpace(thread))
                unique += '_' + thread;

            return unique;
        }

        /// <summary>
        /// Returns the current thread name in a format safe to use for file names.
        /// </summary>
        /// <returns>The current thread name in a format safe to use for file names.</returns>
        public static string GetCurrentThreadName() =>
            Regex.Replace(Thread.CurrentThread.Name, @"[^A-Za-z0-9]+", "");
    }
}
