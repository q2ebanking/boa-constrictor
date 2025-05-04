using Boa.Constrictor.RestSharp;
using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Net;
using System.Reflection;

namespace Boa.Constrictor.Example
{
    public class ScreenplayRestApiAdvancedTest
    {
        private IActor Actor;
        private string AssemblyDir;

        [SetUp]
        public void InitializeScreenplay()
        {
            Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
            AssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Actor.Can(CallDogApi.DumpingTo(AssemblyDir));
            Actor.Can(CallDogImagesApi.DumpingTo(AssemblyDir));
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
