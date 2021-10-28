using Boa.Constrictor.RestSharp;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.UnitTests.RestSharp
{
    [TestFixture]
    public class RequestDataTest
    {
        [Test]
        public void Init_Basic()
        {
            var clientUri = new Uri("https://www.pl.com");
            var resource = "/path/to/thing";
            var requestMethod = Method.GET;

            var clientMock = new Mock<IRestClient>();
            clientMock.Setup(x => x.BaseUrl).Returns(clientUri);
            clientMock.Setup(x => x.BuildUri(It.IsAny<IRestRequest>())).Returns(clientUri);

            var request = new RestRequest(resource, requestMethod);

            var data = new RequestData(request, clientMock.Object);
            data.Method.Should().Be(requestMethod.ToString());
            data.Uri.Should().Be(clientUri.ToString());
            data.Resource.Should().Be(resource);
            data.Parameters.Should().BeEmpty();
            data.Body.Should().BeNull();
        }

        [Test]
        public void Init_Full()
        {
            var clientUri = new Uri("https://www.pl.com");
            var resource = "/path/to/thing";
            var requestMethod = Method.GET;

            #pragma warning disable 0618

            var parameters = new List<Parameter>()
            {
                new Parameter("p1", "hello", ParameterType.HttpHeader),
                new Parameter("p2", "goodbye", ParameterType.Cookie),
            };

            var body = new RequestBody("json", "body", "value");
            
            #pragma warning restore 0618

            var clientMock = new Mock<IRestClient>();
            clientMock.Setup(x => x.BaseUrl).Returns(clientUri);
            clientMock.Setup(x => x.BuildUri(It.IsAny<IRestRequest>())).Returns(clientUri);

            var requestMock = new Mock<IRestRequest>();
            requestMock.Setup(x => x.Method).Returns(requestMethod);
            requestMock.Setup(x => x.Resource).Returns(resource);
            requestMock.Setup(x => x.Parameters).Returns(parameters);
            requestMock.Setup(x => x.Body).Returns(body);

            var data = new RequestData(requestMock.Object, clientMock.Object);
            data.Method.Should().Be(requestMethod.ToString());
            data.Uri.Should().Be(clientUri.ToString());
            data.Resource.Should().Be(resource);
            data.Parameters.Count.Should().Be(2);
            data.Parameters[0].Name.Should().Be("p1");
            data.Parameters[0].Value.Should().Be("hello");
            data.Parameters[0].Type.Should().Be(ParameterType.HttpHeader.ToString());
            data.Parameters[1].Name.Should().Be("p2");
            data.Parameters[1].Value.Should().Be("goodbye");
            data.Parameters[1].Type.Should().Be(ParameterType.Cookie.ToString());
            data.Body.Should().Be(body);
        }
    }
}
