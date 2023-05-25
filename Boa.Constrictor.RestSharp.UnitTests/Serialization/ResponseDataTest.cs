using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using FluentAssertions;

using NUnit.Framework;

using RestSharp;

using RichardSzalay.MockHttp;

namespace Boa.Constrictor.RestSharp.UnitTests
{
    [TestFixture]
    public class ResponseDataTest
    {
        [Test]
        public void Init()
        {
            var clientUri = new Uri("https://www.pl.com");
            var resource = "/path/to/thing";
            var statusCode = HttpStatusCode.Accepted;
            var errorMessage = "error";
            var content = "got some cool stuff";

            var responseHeaders = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("p1", "hello"),
                new KeyValuePair<string, string>("p2", "goodbye")
            };

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When($"{clientUri.OriginalString}/*")
                .Respond(statusCode, responseHeaders, new StringContent(content));

            var client = new RestClient(
                new RestClientOptions
                {
                    ConfigureMessageHandler = _ => mockHttp,
                    BaseUrl = clientUri
                }
            );

            var request = new RestRequest(resource);
            var response = client.Get(request);
            response.ErrorMessage = errorMessage;

            var data = new ResponseData(response);
            data.Uri.Should().Be(clientUri.OriginalString + resource);
            data.StatusCode.Should().Be(statusCode);
            data.ErrorMessage.Should().Be(errorMessage);
            data.Content.Should().Be(content);
            data.Headers.Count.Should().Be(2);
            data.Headers[0].Name.Should().Be("p1");
            data.Headers[0].Value.Should().Be("hello");
            data.Headers[0].Type.Should().Be(ParameterType.HttpHeader.ToString());
            data.Headers[1].Name.Should().Be("p2");
            data.Headers[1].Value.Should().Be("goodbye");
            data.Headers[1].Type.Should().Be(ParameterType.HttpHeader.ToString());
        }
    }
}