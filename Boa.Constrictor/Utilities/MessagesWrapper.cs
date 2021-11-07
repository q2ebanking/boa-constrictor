using System;
using System.Collections.Generic;

namespace Boa.Constrictor.Utilities
{
    /// <summary>
    /// Provides methods for wrapping long messages string.
    /// </summary>
    public static class MessagesWrapper
    {
        /// <summary>
        /// Wraps long lines of messages with provided limit of text to display as wrapped line.
        /// </summary>
        /// <param name="message">The Message on which word wrap will apply.</param>
        /// <param name="limit">The limit of characters to be applied.</param>
        /// <returns></returns>
        public static List<string> GetWrappedMessage(string text, int limit)
        {
            var words = text.Split(new string[] { " ", "\r\n", "\n" }, StringSplitOptions.None);

            List<string> wordList = new List<string>();

            string line = "";
            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    var newLine = string.Join(" ", line, word).Trim();
                    if (newLine.Length >= limit)
                    {
                        wordList.Add(line);
                        line = word;
                    }
                    else
                    {
                        line = newLine;
                    }
                }
            }

            if (line.Length > 0)
                wordList.Add(line);

            return wordList;
        }
    }
}
