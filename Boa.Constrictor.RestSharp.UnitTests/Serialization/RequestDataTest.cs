using System;
using System.Collections.Generic;

using FluentAssertions;

using NUnit.Framework;

using RestSharp;

namespace Boa.Constrictor.RestSharp.UnitTests
{
    [TestFixture]
    public class RequestDataTest
    {
        [Test]
        public void Init_Basic()
        {
            var clientUri = new Uri("https://www.pl.com");
            var resource = "/path/to/thing";
            var requestMethod = Method.Get;

            var client = new RestClient(clientUri);

            var request = new RestRequest(resource, requestMethod);

            var data = new RequestData(request, client);
            data.Method.Should().Be(requestMethod.ToString());
            data.Uri.Should().Be(clientUri.OriginalString + resource);
            data.Resource.Should().Be(resource);
            data.Parameters.Should().BeEmpty();
            data.Body.Should().BeNull();
        }

        [Test]
        public void Init_Full()
        {
            var clientUri = new Uri("https://www.pl.com");
            var resource = "/path/to/thing";
            var requestMethod = Method.Get;
            var body = "This is a body";

            var parameters = new List<Parameter>()
            {
                new HeaderParameter("p1", "hello"),
                new GetOrPostParameter("p2", "goodbye"),
            };

            var client = new RestClient(clientUri);
            var request = new RestRequest(resource, requestMethod);
            request.AddParameter(parameters[0]);
            request.AddParameter(parameters[1]);
            request.AddBody(body);

            var data = new RequestData(request, client);
            data.Method.Should().Be(requestMethod.ToString());
            data.Uri.Should().Be($"{clientUri.OriginalString}{resource}?{parameters[1].Name}={parameters[1].Value}");
            data.Resource.Should().Be(resource);
            data.Parameters.Count.Should().Be(3);
            data.Parameters[0].Name.Should().Be("p1");
            data.Parameters[0].Value.Should().Be("hello");
            data.Parameters[0].Type.Should().Be(ParameterType.HttpHeader.ToString());
            data.Parameters[1].Name.Should().Be("p2");
            data.Parameters[1].Value.Should().Be("goodbye");
            data.Parameters[1].Type.Should().Be(ParameterType.GetOrPost.ToString());
            data.Body.Should().NotBeNull();
            data.Body.Value.Should().Be(body);
        }
    }
}