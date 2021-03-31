using Boa.Constrictor.RestSharp;
using RestSharp;

namespace Boa.Constrictor.Example
{
    public static class DogImagesApi
    {
        public static RestApiDownload<CallDogImagesApi> Download(IRestRequest request, string fileExtension = null) =>
            Rest<CallDogImagesApi>.Download(request, fileExtension);

        public static RestApiCall<CallDogImagesApi> Request(IRestRequest request) =>
            Rest<CallDogImagesApi>.Request(request);

        public static RestApiCall<CallDogImagesApi, TData> Request<TData>(IRestRequest request) =>
            Rest<CallDogImagesApi>.Request<TData>(request);
    }
}
