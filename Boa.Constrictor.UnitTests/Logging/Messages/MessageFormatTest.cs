using Boa.Constrictor.Logging;
using FluentAssertions;
using NUnit.Framework;

namespace Boa.Constrictor.UnitTests.Logging
{
    [TestFixture]
    public class MessageFormatTest
    {
        #region Variables

        public const string MessageText = "Message text!";
        public const string TimePattern = @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}";

        #endregion

        #region Tests

        [Test]
        public void StandardTimestampString()
        {
            const string severity = "pass";
            string formatted = MessageFormat.StandardTimestamp(MessageText, severity);
            formatted.Should().MatchRegex(TimePattern).And.EndWith($"[{severity.ToUpper()}] {MessageText}");
        }

        [Test]
        public void StandardTimestampLogSeverity()
        {
            const LogSeverity severity = LogSeverity.Fatal;
            string formatted = MessageFormat.StandardTimestamp(MessageText, severity);
            formatted.Should().MatchRegex(TimePattern).And.EndWith($"[{severity.ToString().ToUpper()}] {MessageText}");
        }

        #endregion
    }
}
