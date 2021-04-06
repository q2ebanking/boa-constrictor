using Boa.Constrictor.RestSharp;
using RestSharp;
using System.IO;

namespace Boa.Constrictor.Example
{
    public class CallDogApi : AbstractRestSharpAbility
    {
        public const string BaseUrl = "https://dog.ceo/";
        public const string RequestToken = "DogApiRequest";

        private CallDogApi(RestClient client) :
            base(client) { }

        public static CallDogApi DumpingTo(string dumpDir) =>
            new CallDogApi(new RestClient(BaseUrl))
            {
                RequestDumper = new RequestDumper(
                    "Dog API Request Dumper",
                    Path.Combine(dumpDir, RequestToken),
                    RequestToken)
            };
    }
}
