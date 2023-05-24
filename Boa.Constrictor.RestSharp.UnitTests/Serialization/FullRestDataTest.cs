using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using FluentAssertions;

using NUnit.Framework;

using RestSharp;

using RichardSzalay.MockHttp;

namespace Boa.Constrictor.RestSharp.UnitTests
{
    [TestFixture]
    public class FullRestDataTest
    {
        #region Variables

        private Uri ClientUri;
        private string Resource;
        private Method RequestMethod;
        private HttpStatusCode StatusCode;
        private string ErrorMessage;
        private string Content;

#pragma warning disable 0618

        private ParametersCollection Parameters;

#pragma warning restore 0618

        private Cookie Cookie = new Cookie("name", "value");

        private RestClient Client;
        private RestRequest Request;
        private RestResponse Response;

        #endregion Variables

        #region Setup and Teardown

        [SetUp]
        public void SetUp()
        {
            ClientUri = new Uri("https://www.pl.com");
            Resource = "/path/to/thing";
            RequestMethod = Method.Get;
            StatusCode = HttpStatusCode.Accepted;
            ErrorMessage = "error";
            Content = "got some cool stuff";

            var responseHeaders = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("a", "a"),
                new KeyValuePair<string, string>("b", "b")
            };
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When($"{ClientUri.OriginalString}/*")
                .Respond(StatusCode, responseHeaders, new StringContent(Content));

            Client = new RestClient(
                new RestClientOptions
                {
                    ConfigureMessageHandler = _ => mockHttp,
                    BaseUrl = ClientUri
                }
            );

            Request = new RestRequest(Resource, RequestMethod);
            Request.AddParameter("a", "a");
            Request.AddParameter("b", "b");

            Parameters = Request.Parameters;

            Response = Client.Get(Request);
            Response.ErrorMessage = "error";
        }

        #endregion Setup and Teardown

        #region Tests

        [Test]
        public void InitAllNull()
        {
            var data = new FullRestData(Client);

            data.Duration.StartTime.Should().BeNull();
            data.Duration.EndTime.Should().BeNull();
            data.Duration.Duration.Should().BeNull();
            data.Request.Should().BeNull();
            data.Response.Should().BeNull();
            data.Cookies.Should().BeEmpty();
        }

        [Test]
        public void InitCookiesOnly()
        {
            Client.CookieContainer.Add(ClientUri, Cookie);

            var data = new FullRestData(Client);

            data.Duration.StartTime.Should().BeNull();
            data.Duration.EndTime.Should().BeNull();
            data.Duration.Duration.Should().BeNull();
            data.Request.Should().BeNull();
            data.Response.Should().BeNull();
            data.Cookies.Count.Should().Be(1);
            data.Cookies[0].Name.Should().Be(Cookie.Name);
            data.Cookies[0].Value.Should().Be(Cookie.Value);
        }

        [Test]
        public void InitStartTimeOnly()
        {
            DateTime start = DateTime.UtcNow;
            var data = new FullRestData(Client, start: start);

            data.Duration.StartTime.Should().Be(start);
            data.Duration.EndTime.Should().BeNull();
            data.Duration.Duration.Should().BeNull();
            data.Request.Should().BeNull();
            data.Response.Should().BeNull();
            data.Cookies.Should().BeEmpty();
        }

        [Test]
        public void InitEndTimeOnly()
        {
            DateTime end = DateTime.UtcNow;
            var data = new FullRestData(Client, end: end);

            data.Duration.StartTime.Should().BeNull();
            data.Duration.EndTime.Should().Be(end);
            data.Duration.Duration.Should().BeNull();
            data.Request.Should().BeNull();
            data.Response.Should().BeNull();
            data.Cookies.Should().BeEmpty();
        }

        [Test]
        public void InitDuration()
        {
            DateTime start = DateTime.UtcNow;
            DateTime end = start.AddSeconds(1);
            var data = new FullRestData(Client, start: start, end: end);

            data.Duration.StartTime.Should().Be(start);
            data.Duration.EndTime.Should().Be(end);
            data.Duration.Duration.Should().Be(new TimeSpan(0, 0, 1));
            data.Request.Should().BeNull();
            data.Response.Should().BeNull();
            data.Cookies.Should().BeEmpty();
        }

        [Test]
        public void InitRequestOnly()
        {
            Client.CookieContainer.Add(ClientUri, Cookie);
            DateTime start = DateTime.UtcNow;
            var data = new FullRestData(Client, Request, start: start);

            data.Duration.StartTime.Should().Be(start);
            data.Duration.EndTime.Should().BeNull();
            data.Duration.Duration.Should().BeNull();

            data.Request.Should().NotBeNull();
            data.Request.Method.Should().Be(RequestMethod.ToString());
            data.Request.Resource.Should().Be(Resource);
            data.Request.Parameters.Count.Should().Be(Parameters.Count);
            data.Request.Parameters[0].Name.Should().Be(Parameters.ElementAt(0).Name);
            data.Request.Parameters[1].Name.Should().Be(Parameters.ElementAt(1).Name);
            data.Request.Body.Should().BeNull();

            data.Response.Should().BeNull();

            data.Cookies.Count.Should().Be(1);
            data.Cookies[0].Name.Should().Be(Cookie.Name);
            data.Cookies[0].Value.Should().Be(Cookie.Value);
        }

        [Test]
        public void InitRequestAndResponse()
        {
            Client.CookieContainer.Add(ClientUri, Cookie);
            DateTime start = DateTime.UtcNow;
            DateTime end = start.AddSeconds(1);
            var data = new FullRestData(Client, Request, Response, start, end);

            data.Duration.StartTime.Should().Be(start);
            data.Duration.EndTime.Should().Be(end);
            data.Duration.Duration.Should().Be(new TimeSpan(0, 0, 1));

            data.Request.Should().NotBeNull();
            data.Request.Method.Should().Be(RequestMethod.ToString());
            data.Request.Resource.Should().Be(Resource);
            data.Request.Parameters.Count.Should().Be(Parameters.Count);
            data.Request.Parameters[0].Name.Should().Be(Parameters.ElementAt(0).Name);
            data.Request.Parameters[1].Name.Should().Be(Parameters.ElementAt(1).Name);
            data.Request.Body.Should().BeNull();

            data.Response.Should().NotBeNull();
            data.Response.Uri.Should().Be(Response.ResponseUri);
            data.Response.StatusCode.Should().Be(StatusCode);
            data.Response.ErrorMessage.Should().Be(ErrorMessage);
            data.Response.Content.Should().Be(Content);
            data.Response.Headers.Count.Should().Be(Parameters.Count);
            data.Response.Headers[0].Name.Should().Be(Parameters.ElementAt(0).Name);
            data.Response.Headers[1].Name.Should().Be(Parameters.ElementAt(1).Name);

            data.Cookies.Count.Should().Be(1);
            data.Cookies[0].Name.Should().Be(Cookie.Name);
            data.Cookies[0].Value.Should().Be(Cookie.Value);
        }

        #endregion Tests
    }
}