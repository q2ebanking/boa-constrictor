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

        private List<Parameter> Parameters;

        #pragma warning restore 0618

        private Cookie Cookie;
        private CookieContainer Container;

        private Mock<IRestClient> ClientMock;
        private Mock<IRestRequest> RequestMock;
        private Mock<IRestResponse> ResponseMock;

        #endregion

        #region Setup and Teardown

        [SetUp]
        public void SetUp()
        {
            ClientUri = new Uri("https://www.pl.com");
            Resource = "/path/to/thing";
            RequestMethod = Method.GET;
            StatusCode = HttpStatusCode.Accepted;
            ErrorMessage = "error";
            Content = "got some cool stuff";

            #pragma warning disable 0618

            Parameters = new List<Parameter>()
            {
                new Parameter("a", "a", ParameterType.HttpHeader),
                new Parameter("b", "b", ParameterType.Cookie),
            };

            #pragma warning restore 0618

            Cookie = new Cookie("name", "value");
            Container = new CookieContainer();

            ClientMock = new Mock<IRestClient>();
            ClientMock.Setup(x => x.BaseUrl).Returns(ClientUri);
            ClientMock.Setup(x => x.BuildUri(It.IsAny<IRestRequest>())).Returns(ClientUri);
            ClientMock.Setup(x => x.CookieContainer).Returns(Container);

            RequestMock = new Mock<IRestRequest>();
            RequestMock.Setup(x => x.Method).Returns(RequestMethod);
            RequestMock.Setup(x => x.Resource).Returns(Resource);
            RequestMock.Setup(x => x.Parameters).Returns(Parameters);

            ResponseMock = new Mock<IRestResponse>();
            ResponseMock.Setup(x => x.ResponseUri).Returns(ClientUri);
            ResponseMock.Setup(x => x.StatusCode).Returns(StatusCode);
            ResponseMock.Setup(x => x.Content).Returns(Content);
            ResponseMock.Setup(x => x.Headers).Returns(Parameters);
            ResponseMock.Setup(x => x.ErrorMessage).Returns(ErrorMessage);
        }

        #endregion

        #region Tests

        [Test]
        public void InitAllNull()
        {
            var data = new FullRestData(ClientMock.Object);

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
            Container.Add(ClientUri, Cookie);
            var data = new FullRestData(ClientMock.Object);

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
            var data = new FullRestData(ClientMock.Object, start: start);

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
            var data = new FullRestData(ClientMock.Object, end: end);

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
            var data = new FullRestData(ClientMock.Object, start: start, end: end);

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
            Container.Add(ClientUri, Cookie);
            DateTime start = DateTime.UtcNow;
            var data = new FullRestData(ClientMock.Object, RequestMock.Object, start: start);

            data.Duration.StartTime.Should().Be(start);
            data.Duration.EndTime.Should().BeNull();
            data.Duration.Duration.Should().BeNull();

            data.Request.Should().NotBeNull();
            data.Request.Method.Should().Be(RequestMethod.ToString());
            data.Request.Resource.Should().Be(Resource);
            data.Request.Parameters.Count.Should().Be(Parameters.Count);
            data.Request.Parameters[0].Name.Should().Be(Parameters[0].Name);
            data.Request.Parameters[1].Name.Should().Be(Parameters[1].Name);
            data.Request.Body.Should().BeNull();

            data.Response.Should().BeNull();

            data.Cookies.Count.Should().Be(1);
            data.Cookies[0].Name.Should().Be(Cookie.Name);
            data.Cookies[0].Value.Should().Be(Cookie.Value);
        }

        [Test]
        public void InitRequestAndResponse()
        {
            Container.Add(ClientUri, Cookie);
            DateTime start = DateTime.UtcNow;
            DateTime end = start.AddSeconds(1);
            var data = new FullRestData(ClientMock.Object, RequestMock.Object, ResponseMock.Object, start, end);

            data.Duration.StartTime.Should().Be(start);
            data.Duration.EndTime.Should().Be(end);
            data.Duration.Duration.Should().Be(new TimeSpan(0, 0, 1));

            data.Request.Should().NotBeNull();
            data.Request.Method.Should().Be(RequestMethod.ToString());
            data.Request.Resource.Should().Be(Resource);
            data.Request.Parameters.Count.Should().Be(Parameters.Count);
            data.Request.Parameters[0].Name.Should().Be(Parameters[0].Name);
            data.Request.Parameters[1].Name.Should().Be(Parameters[1].Name);
            data.Request.Body.Should().BeNull();

            data.Response.Should().NotBeNull();
            data.Response.Uri.Should().Be(ClientUri);
            data.Response.StatusCode.Should().Be(StatusCode);
            data.Response.ErrorMessage.Should().Be(ErrorMessage);
            data.Response.Content.Should().Be(Content);
            data.Response.Headers.Count.Should().Be(Parameters.Count);
            data.Response.Headers[0].Name.Should().Be(Parameters[0].Name);
            data.Response.Headers[1].Name.Should().Be(Parameters[1].Name);

            data.Cookies.Count.Should().Be(1);
            data.Cookies[0].Name.Should().Be(Cookie.Name);
            data.Cookies[0].Value.Should().Be(Cookie.Value);
        }

        #endregion
    }
}
