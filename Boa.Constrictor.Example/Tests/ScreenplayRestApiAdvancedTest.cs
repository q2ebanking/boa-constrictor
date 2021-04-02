using Boa.Constrictor.Logging;
using Boa.Constrictor.RestSharp;
using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;
using System.Net;

namespace Boa.Constrictor.Example
{
    public class ScreenplayRestApiAdvancedTest
    {
        private IActor Actor;

        [SetUp]
        public void InitializeScreenplay()
        {
            Actor = new Actor(name: "Andy", logger: new ConsoleLogger());

            Actor.Can(CallDogApi.UsingBaseUrl());
            Actor.Can(CallDogImagesApi.UsingBaseUrl());
        }

        [Test]
        public void TestDogApi()
        {
            var request = DogRequests.GetRandomDog();
            var response = Actor.Calls(Rest<CallDogApi>.Request<DogResponse>(request));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data.Status.Should().Be("success");
            response.Data.Message.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void TestDogApiImage()
        {
            var imageData = Actor.AsksFor(RandomDogImage.FromDogApi());
            imageData.Should().NotBeNullOrEmpty();
        }
    }
}
