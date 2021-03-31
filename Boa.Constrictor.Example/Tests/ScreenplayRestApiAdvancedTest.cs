using Boa.Constrictor.Logging;
using Boa.Constrictor.RestSharp;
using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System;
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
            // `Rest<CallDogApi>.Request(...)` uses the built-in `Rest` builder methods

            var request = DogRequests.GetRandomDog();
            var response = Actor.Calls(Rest<CallDogApi>.Request<DogResponse>(request));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data.Status.Should().Be("success");
            response.Data.Message.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void TestDogApiImage()
        {
            // `DogApi.Request(...)` is a custom alias for `Rest<CallDogApi>.Request(...)` that looks nicer.

            var request = DogRequests.GetRandomDog();
            var response = Actor.Calls(DogApi.Request<DogResponse>(request));
            var resource = new Uri(response.Data.Message).AbsolutePath;

            var imageRequest = new RestRequest(resource);
            var imageData = Actor.Calls(DogImagesApi.Download(imageRequest));
            imageData.Should().NotBeNullOrEmpty();
        }
    }
}
