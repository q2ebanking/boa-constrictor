using Boa.Constrictor.RestSharp;
using RestSharp;

namespace Boa.Constrictor.Example
{
    public class CallDogApi : AbstractRestSharpAbility
    {
        public const string BaseUrl = "https://dog.ceo/";

        private CallDogApi(RestClient client) :
            base(client) { }

        public static CallDogApi UsingBaseUrl() =>
            new CallDogApi(new RestClient(BaseUrl));
    }
}
