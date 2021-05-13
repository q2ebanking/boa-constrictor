using Boa.Constrictor.Screenplay;
using RestSharp;
using System;
using System.IO;

using DogApi = Boa.Constrictor.RestSharp.Rest<Boa.Constrictor.Example.CallDogApi>;
using DogImagesApi = Boa.Constrictor.RestSharp.Rest<Boa.Constrictor.Example.CallDogImagesApi>;

namespace Boa.Constrictor.Example
{
    public class RandomDogImage : IQuestion<byte[]>
    {
        private RandomDogImage() { }

        public static RandomDogImage FromDogApi() =>
            new RandomDogImage();

        public byte[] RequestAs(IActor actor)
        {
            var request = DogRequests.GetRandomDog();
            var response = actor.Calls(DogApi.Request<DogResponse>(request));

            var resource = new Uri(response.Data.Message).AbsolutePath;
            var imageRequest = new RestRequest(resource);
            var extension = Path.GetExtension(resource);
            var imageData = actor.Calls(DogImagesApi.Download(imageRequest, extension));

            return imageData;
        }
    }
}