using Boa.Constrictor.RestSharp;
using RestSharp;

namespace Boa.Constrictor.Example
{
    public class CallDogImagesApi : AbstractRestSharpAbility
    {
        public const string BaseUrl = "https://images.dog.ceo/";

        private CallDogImagesApi(RestClient client) :
            base(client) { }

        public static CallDogImagesApi UsingBaseUrl() =>
            new CallDogImagesApi(new RestClient(BaseUrl));
    }
}
