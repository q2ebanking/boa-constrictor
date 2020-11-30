using Boa.Constrictor.Dumping;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Utilities;
using RestSharp;
using System;
using System.IO;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Calls the REST API given by the request spec and returns the array of bytes that make up the file in the response.
    /// Requires the CallRestApi ability.
    /// Can also log requests and downloaded files to a given output directory if the actor has the IncrementFiles ability.
    /// </summary>
    public class RestFileDownload : AbstractRestQuestion<byte[]>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="request">The REST request to call.</param>
        private RestFileDownload(string baseUrl, IRestRequest request) :
            base(baseUrl)
        {
            Request = request;
            OutputDir = null;
            FileSuffix = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The REST request to call.
        /// </summary>
        private IRestRequest Request { get; }

        /// <summary>
        /// The output directory for logging.
        /// </summary>
        private string OutputDir { get; set; }

        /// <summary>
        /// The suffix that denotes the file type of the file being downloaded.
        /// </summary>
        private string FileSuffix { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="baseUrl">The base URL for the request.</param>
        /// <param name="request">The REST request to call.</param>
        /// <returns></returns>
        public static RestFileDownload From(string baseUrl, IRestRequest request) =>
            new RestFileDownload(baseUrl, request);

        /// <summary>
        /// Sets the output directory for logging as well as the file suffix for specifying the file type being downloaded.
        /// </summary>
        /// <param name="outputDir"></param>
        /// <param name="fileSuffix"></param>
        /// <returns></returns>
        public RestFileDownload To(string outputDir, string fileSuffix)
        {
            OutputDir = outputDir;
            FileSuffix = fileSuffix;
            return this;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Concatenates the request filepath.
        /// Creates the output directory if necessary.
        /// Precondition: Output directory and file suffix must be given (e.g., not null).
        /// </summary>
        /// <param name="token"></param>
        /// <param name="fileSuffix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        private string PreparePath(string token, string fileSuffix, string suffix = null)
        {
            if (!Directory.Exists(OutputDir))
                Directory.CreateDirectory(OutputDir);

            string name = Names.ConcatUniqueName(token, suffix) + fileSuffix;
            string path = Path.Combine(OutputDir, name);

            return path;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calls the REST request and returns the file byte array.
        /// Throws a RestApiDownloadException if the request's response code is a client or server error or response status is a transport error.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <param name="client">The REST client.</param>
        /// <returns></returns>
        public override byte[] RequestAs(IActor actor, IRestClient client)
        {
            byte[] fileBytes = null;
            DateTime? start = null;
            DateTime? end = null;

            try
            {
                start = DateTime.UtcNow;

                var response = client.Execute(Request);
                if ((int)response.StatusCode >= 400 || response.ResponseStatus == ResponseStatus.Error)
                    throw new RestApiDownloadException(Request, response);

                fileBytes = response.RawBytes;

                end = DateTime.UtcNow;

                actor.Logger.Info($"Bytes received successfully");
            }
            finally
            {
                if (OutputDir == null || FileSuffix == null)
                {
                    actor.Logger.Debug("Request will not be logged because no output directory and or file suffix was provided");
                }
                else if (fileBytes == null)
                {
                    actor.Logger.Debug("No bytes were received for the file to download");
                }
                else
                {
                    var data = new FullRestData(client, Request, null, start, end);
                    string logPath = new JsonDumper("Request Dumper", OutputDir, "Request").Dump(data);
                    actor.Logger.Info($"Logged request to: {logPath}");

                    string downloadPath = new ByteDumper("Download Dumper", OutputDir, "Download").Dump(fileBytes, FileSuffix);
                    actor.Logger.Info($"Downloaded file to: {downloadPath}");
                }
            }

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
