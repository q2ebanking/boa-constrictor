using Boa.Constrictor.RestSharp;
using RestSharp;

namespace Boa.Constrictor.Example
{
    public static class DogApi
    {
        public static RestApiDownload<CallDogApi> Download(IRestRequest request, string fileExtension = null) =>
            Rest<CallDogApi>.Download(request, fileExtension);

        public static RestApiCall<CallDogApi> Request(IRestRequest request) =>
            Rest<CallDogApi>.Request(request);

        public static RestApiCall<CallDogApi, TData> Request<TData>(IRestRequest request) =>
            Rest<CallDogApi>.Request<TData>(request);
    }
}
