using RestSharp;

namespace Boa.Constrictor.Example
{
    public static class DogRequests
    {
        public static IRestRequest GetRandomDog() =>
            new RestRequest("api/breeds/image/random", Method.GET);
    }
}
