using System;

namespace Boa.Constrictor.Utilities
{
    /// <summary>
    /// Provides methods for handling URLs.
    /// </summary>
    public static class Urls
    {
        /// <summary>
        /// Concatenates a base URL and a relative URL.
        /// Automatically handles the "/" in between.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <returns></returns>
        public static string Combine(string baseUrl, string relativeUrl) =>
            new Uri(new Uri(baseUrl), relativeUrl).ToString();
    }
}
