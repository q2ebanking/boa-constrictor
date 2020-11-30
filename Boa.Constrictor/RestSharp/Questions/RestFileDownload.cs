using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Utilities;
using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Calls the REST API given by the request spec and returns the array of bytes that make up the file in the response.
    /// Requires the CallRestApi ability.
    /// Automatically dumps the downloaded file if the ability has a dumper.
    /// </summary>
    public class RestFileDownload : AbstractBaseUrlHandler, IQuestion<byte[]>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="request">The REST request to call.</param>
        /// <param name="fileExtension">The extension for the file to download.</param>
        private RestFileDownload(string baseUrl, IRestRequest request, string fileExtension = null) :
            base(baseUrl)
        {
            Request = request;
            FileExtension = fileExtension;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The REST request to call.
        /// </summary>
        public IRestRequest Request { get; private set; }

        /// <summary>
        /// The extension for the file to download.
        /// </summary>
        public string FileExtension { get; private set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="request">The REST request to call.</param>
        /// <param name="fileExtension">The extension for the file to download.</param>
        /// <returns></returns>
        public static RestFileDownload From(string baseUrl, IRestRequest request, string fileExtension = null) =>
            new RestFileDownload(baseUrl, request, fileExtension);

        #endregion

        #region Methods

        /// <summary>
        /// Calls the REST request and returns the file byte array.
        /// Throws a RestApiDownloadException if the request's response code is a client or server error or response status is a transport error.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public byte[] RequestAs(IActor actor)
        {
            // Prepare objects
            var ability = actor.Using<CallRestApi>();
            byte[] fileBytes = null;

            try
            {
                // Call the request for the download
                var response = actor.AsksFor(RestApiResponse.From(BaseUrl, Request));
                fileBytes = response.RawBytes;

                // Verify download success
                if ((int)response.StatusCode >= 400 || response.ResponseStatus == ResponseStatus.Error || fileBytes == null)
                    throw new RestApiDownloadException(Request, response);

                // Log successful download
                actor.Logger.Info($"Bytes received successfully");
            }
            finally
            {
                if (ability.CanDumpDownloads() && fileBytes != null)
                {
                    // Dump the file
                    string path = ability.DownloadDumper.Dump(fileBytes, FileExtension);
                    actor.Logger.Info($"Dumped downloaded file to: {path}");

                    // Warn about blank file extensions
                    if (FileExtension == "")
                        actor.Logger.Warning("The extension for the downloaded file was blank");
                }
                else {
                    // Warn that the downloaded file will not be dumped
                    actor.Logger.Debug("The downloaded file will not be dumped");
                }
            }

            // Return the file data
            return fileBytes;
        }

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"File download from calling {Request.Method} '{Urls.Combine(BaseUrl, Request.Resource)}'";

        #endregion
    }
}
