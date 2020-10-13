using Boa.Constrictor.RestSharp;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace Boa.Constrictor.UnitTests.RestSharp
{
    [TestFixture]
    public class RequestLoggerTest
    {
        #region Variables

        private string AssemblyDir;
        private string RequestPath;

        private Uri ClientUri;
        private string Resource;
        private Method RequestMethod;
        private HttpStatusCode StatusCode;
        private string ErrorMessage;
        private string Content;
        private List<Parameter> Parameters;

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
            AssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            RequestPath = Path.Combine(AssemblyDir, "ut_request.json");

            ClientUri = new Uri("https://www.pl.com");
            Resource = "/path/to/thing";
            RequestMethod = Method.GET;
            StatusCode = HttpStatusCode.Accepted;
            ErrorMessage = "error";
            Content = "got some cool stuff";

            Parameters = new List<Parameter>()
            {
                new Parameter("a", "a", ParameterType.HttpHeader),
                new Parameter("b", "b", ParameterType.Cookie),
            };

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

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(RequestPath))
                File.Delete(RequestPath);
        }

        #endregion

        #region Tests

        [Test]
        public void AllNull()
        {
            new RequestLogger(RequestPath, ClientMock.Object).Log();

            using (var file = new StreamReader(RequestPath))
            {
                var data = JsonConvert.DeserializeObject<RequestLogger.FullData>(file.ReadToEnd());
                data.Duration.StartTime.Should().BeNull();
                data.Duration.EndTime.Should().BeNull();
                data.Duration.Duration.Should().BeNull();
                data.Request.Should().BeNull();
                data.Response.Should().BeNull();
                data.Cookies.Should().BeEmpty();
            }
        }

        [Test]
        public void RequestOnly()
        {
            Container.Add(ClientUri, Cookie);
            DateTime start = DateTime.UtcNow;
            new RequestLogger(RequestPath, ClientMock.Object, RequestMock.Object, start: start).Log();

            using (var file = new StreamReader(RequestPath))
            {
                var data = JsonConvert.DeserializeObject<RequestLogger.FullData>(file.ReadToEnd());

                data.Duration.StartTime.Should().Be(start);
                data.Duration.EndTime.Should().BeNull();
                data.Duration.Duration.Should().BeNull();

                data.Request.Should().NotBeNull();
                data.Request.Method.Should().Be(RequestMethod.ToString());
                data.Request.Resource.Should().Be(Resource);
                data.Request.Parameters.Count.Should().Be(Parameters.Count);
                data.Request.Parameters[0].Name.Should().Be(Parameters[0].Name);
                data.Request.Parameters[1].Name.Should().Be(Parameters[1].Name);

                data.Response.Should().BeNull();

                data.Cookies.Count.Should().Be(1);
                data.Cookies[0].Name.Should().Be(Cookie.Name);
                data.Cookies[0].Value.Should().Be(Cookie.Value);
            }
        }

        [Test]
        public void RequestAndResponse()
        {
            Container.Add(ClientUri, Cookie);
            DateTime start = DateTime.UtcNow;
            DateTime end = start.AddSeconds(1);
            new RequestLogger(RequestPath, ClientMock.Object, RequestMock.Object, ResponseMock.Object, start, end).Log();

            using (var file = new StreamReader(RequestPath))
            {
                var data = JsonConvert.DeserializeObject<RequestLogger.FullData>(file.ReadToEnd());

                data.Duration.StartTime.Should().Be(start);
                data.Duration.EndTime.Should().Be(end);
                data.Duration.Duration.Should().Be(new TimeSpan(0, 0, 1));

                data.Request.Should().NotBeNull();
                data.Request.Method.Should().Be(RequestMethod.ToString());
                data.Request.Resource.Should().Be(Resource);
                data.Request.Parameters.Count.Should().Be(Parameters.Count);
                data.Request.Parameters[0].Name.Should().Be(Parameters[0].Name);
                data.Request.Parameters[1].Name.Should().Be(Parameters[1].Name);

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
        }
        
        #endregion
    }
}
