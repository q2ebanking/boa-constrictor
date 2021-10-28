using Boa.Constrictor.RestSharp;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Boa.Constrictor.UnitTests.RestSharp
{
    [TestFixture]
    public class ResponseDataTest
    {
        [Test]
        public void Init()
        {
            var clientUri = new Uri("https://www.pl.com");
            var statusCode = HttpStatusCode.Accepted;
            var errorMessage = "error";
            var content = "got some cool stuff";

            #pragma warning disable 0618

            var parameters = new List<Parameter>()
            {
                new Parameter("p1", "hello", ParameterType.HttpHeader),
                new Parameter("p2", "goodbye", ParameterType.Cookie),
            };

            #pragma warning restore 0618

            var responseMock = new Mock<IRestResponse>();
            responseMock.Setup(x => x.ResponseUri).Returns(clientUri);
            responseMock.Setup(x => x.StatusCode).Returns(statusCode);
            responseMock.Setup(x => x.Content).Returns(content);
            responseMock.Setup(x => x.Headers).Returns(parameters);
            responseMock.Setup(x => x.ErrorMessage).Returns(errorMessage);

            var data = new ResponseData(responseMock.Object);
            data.Uri.Should().Be(clientUri);
            data.StatusCode.Should().Be(statusCode);
            data.ErrorMessage.Should().Be(errorMessage);
            data.Content.Should().Be(content);
            data.Headers.Count.Should().Be(2);
            data.Headers[0].Name.Should().Be("p1");
            data.Headers[0].Value.Should().Be("hello");
            data.Headers[0].Type.Should().Be(ParameterType.HttpHeader.ToString());
            data.Headers[1].Name.Should().Be("p2");
            data.Headers[1].Value.Should().Be("goodbye");
            data.Headers[1].Type.Should().Be(ParameterType.Cookie.ToString());
        }
    }
}
