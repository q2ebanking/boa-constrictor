using Boa.Constrictor.Screenplay;
using RestSharp;
using System;

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
            var imageData = actor.Calls(DogImagesApi.Download(imageRequest));

            return imageData;
        }
    }
}