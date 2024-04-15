namespace Boa.Constrictor.Xunit.UnitTests;

using Boa.Constrictor.Screenplay;
using FluentAssertions;
using global::Xunit;

public class MessageSinkLoggerTest
{
    #region Variables

    const string TimePattern = @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}";

    ConcreteMessageSink MessageSink;

    MessageSinkLogger Logger;

    #endregion

    #region Setup

    public MessageSinkLoggerTest()
    {
        MessageSink = new ConcreteMessageSink();
        Logger = new MessageSinkLogger(MessageSink);
    }

    #endregion

    #region Tests

    [Fact]
    public void Close()
    {
        Logger.Info("hello");
        Logger.Info("moto");
        Logger.Invoking(y => y.Close()).Should().NotThrow();
    }

    [Fact]
    public void LogArtifact()
    {
        const string type = "Screenshot";
        const string path = "path/to/screen.png";

        Logger.LogArtifact(type, path);

        MessageSink.LastMessage.Should().MatchRegex(TimePattern).And.EndWith($"[INFO] {type}: {path}");
    }

    [Theory]
    [InlineData("Trace")]
    [InlineData("Debug")]
    [InlineData("Info")]
    [InlineData("Warning")]
    [InlineData("Error")]
    [InlineData("Fatal")]
    public void LogByLevel(string level)
    {
        const string message = "Message text!";

        Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });

        MessageSink.LastMessage.Should().MatchRegex(TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
    }

    [Theory]
    [InlineData("Info")]
    [InlineData("Warning")]
    [InlineData("Error")]
    [InlineData("Fatal")]
    public void LowestSeverityLogged(string level)
    {
        const string message = "Message text!";
        Logger.LowestSeverity = LogSeverity.Info;

        Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });

        MessageSink.LastMessage.Trim().Should().MatchRegex(TimePattern).And.EndWith($"[{level.ToUpper()}] {message}");
    }

    [Theory]
    [InlineData("Trace")]
    [InlineData("Debug")]
    [InlineData("Info")]
    [InlineData("Warning")]
    [InlineData("Error")]
    public void LowestSeverityBlocked(string level)
    {
        const string message = "Message text!";
        Logger.LowestSeverity = LogSeverity.Fatal;

        Logger.GetType().GetMethod(level).Invoke(Logger, new object[] { message });

        MessageSink.LastMessage.Should().BeNull();
    }

    #endregion
}