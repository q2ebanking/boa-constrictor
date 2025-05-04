using Boa.Constrictor.RestSharp;
using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace Boa.Constrictor.Example
{
    public class ScreenplayRestApiBasicTest
    {
        private IActor Actor;

        [SetUp]
        public void InitializeScreenplay()
        {
            Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
            Actor.Can(CallRestApi.Using(new RestClient("https://dog.ceo/")));
        }

        [Test]
        public void TestDogApiStatusCode()
        {
            // This test shows how to call a REST API without parsing response content.

            var request = DogRequests.GetRandomDog();
            var response = Actor.Calls(Rest.Request(request));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void TestDogApiContent()
        {
            // This test shows how to call a REST API and parse its response content.
            // It should supersede the previous test, but both are kept for the sake of examples.

            var request = DogRequests.GetRandomDog();
            var response = Actor.Calls(Rest.Request<DogResponse>(request));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data.Status.Should().Be("success");
            response.Data.Message.Should().NotBeNullOrWhiteSpace();
        }
    }
}
