namespace Boa.Constrictor.Xunit.UnitTests;

using global::Xunit.Abstractions;
using global::Xunit.Sdk;

/// <summary>
/// A test double that enables reading the output of a message sink
/// </summary>
public class ConcreteMessageSink : IMessageSink
{
    public bool OnMessage(IMessageSinkMessage message)
    {
        var diagnosticMessage = (DiagnosticMessage)message;
        LastMessage = diagnosticMessage.Message;
        return true;
    }

    public string LastMessage { get; set; }
}