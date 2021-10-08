using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using RestSharp;
using System;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Abstract parent class for RestSharp Questions.
    /// Provides protected methods for calling requests and downloads.
    /// </summary>
    /// <typeparam name="TAbility">The RestSharp Ability type.</typeparam>
    /// <typeparam name="TAnswer">The answer type for the Question.</typeparam>
    public abstract class AbstractRestQuestion<TAbility, TAnswer> : IQuestion<TAnswer>
        where TAbility : IRestSharpAbility
    {
        #region Properties

        /// <summary>
        /// The REST request to call.
        /// </summary>
        public IRestRequest Request { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Protected constructor.
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        protected AbstractRestQuestion(IRestRequest request) => Request = request;

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Executes the request using the given client.
        /// </summary>
        /// <param name="client">The RestSharp client.</param>
        /// <returns></returns>
        protected abstract IRestResponse Execute(IRestClient client);

        /// <summary>
        /// Calls the Question.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public abstract TAnswer RequestAs(IActor actor);

        #endregion

        #region Methods

        /// <summary>
        /// Calls the REST request and returns the downloaded file byte array.
        /// Throws a RestApiDownloadException if the request's response code is a client or server error or response status is a transport error.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="fileExtension">The extension for the file to download.</param>
        /// <returns></returns>
        protected byte[] CallDownload(IActor actor, string fileExtension)
        {
            // Prepare objects
            var ability = actor.Using<TAbility>();
            byte[] fileBytes = null;

            try
            {
                // Call the request for the download
                var response = CallRequest(actor);
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
                    string path = ability.DownloadDumper.Dump(fileBytes, fileExtension);
                    actor.Logger.LogArtifact(ArtifactTypes.Downloads, path);

                    // Warn about blank file extensions
                    if (fileExtension == "")
                        actor.Logger.Warning("The extension for the downloaded file was blank");
                }
                else
                {
                    // Warn that the downloaded file will not be dumped
                    actor.Logger.Debug("The downloaded file will not be dumped");
                }
            }

            // Return the file data
            return fileBytes;
        }

        /// <summary>
        /// Calls the REST request and returns the response.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        protected IRestResponse CallRequest(IActor actor)
        {
            // Prepare variables
            var ability = actor.Using<TAbility>();
            IRestResponse response = null;
            DateTime? start = null;
            DateTime? end = null;

            try
            {
                // Make the request
                start = DateTime.UtcNow;
                response = Execute(ability.Client);
                end = DateTime.UtcNow;

                // Log the response code
                actor.Logger.Info($"Response code: {(int)response.StatusCode}");
            }
            finally
            {
                if (ability.CanDumpRequests())
                {
                    // Try to dump the request and the response
                    string path = ability.RequestDumper.Dump(ability.Client, Request, response, start, end);
                    actor.Logger.LogArtifact(ArtifactTypes.Requests, path);
                }
                else
                {
                    // Warn that the request will not be dumped
                    actor.Logger.Debug("Request will not be dumped");
                }
            }

            // Return the response object
            return response;
        }

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"REST API using {typeof(TAbility)}: {Request.Method} '{Request.Resource}'";

        #endregion
    }
}
