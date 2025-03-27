using RestSharp;

namespace Boa.Constrictor.Example
{
    public static class DogRequests
    {
        public static RestRequest GetRandomDog() =>
            new RestRequest("api/breeds/image/random", Method.Get);
    }
}
