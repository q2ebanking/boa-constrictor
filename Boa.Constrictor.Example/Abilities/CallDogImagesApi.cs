using Boa.Constrictor.RestSharp;
using Boa.Constrictor.Screenplay;
using RestSharp;
using System.IO;

namespace Boa.Constrictor.Example
{
    public class CallDogImagesApi : AbstractRestSharpAbility
    {
        public const string BaseUrl = "https://images.dog.ceo/";
        public const string DownloadToken = "DogImagesApiDownload";
        public const string RequestToken = "DogImagesApiRequest";

        private CallDogImagesApi(RestClient client, string dumpDir) :
            base(client)
        {
            RequestDumper = new RequestDumper(
                "Dog Images API Request Dumper",
                Path.Combine(dumpDir, RequestToken),
                RequestToken);
            DownloadDumper = new ByteDumper(
                "Dog Images API Download Dumper",
                Path.Combine(dumpDir, DownloadToken),
                DownloadToken);
        }

        public static CallDogImagesApi DumpingTo(string dumpDir) =>
            new CallDogImagesApi(new RestClient(BaseUrl), dumpDir);
    }
}
