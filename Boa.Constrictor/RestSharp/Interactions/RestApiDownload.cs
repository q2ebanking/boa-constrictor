﻿using Boa.Constrictor.Screenplay;
using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Calls the REST API given by the request and returns the array of bytes that make up the file data in the response.
    /// Requires the applicable IRestSharpAbility ability.
    /// Automatically dumps the downloaded file if the ability has a dumper.
    /// </summary>
    /// <typeparam name="TAbility">The RestSharp Ability type.</typeparam>
    public class RestApiDownload<TAbility> : AbstractRestQuestion<TAbility, byte[]>
        where TAbility: IRestSharpAbility
    {
        #region Properties

        /// <summary>
        /// The extension for the file to download.
        /// </summary>
        public string FileExtension { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Internal constructor.
        /// (Use the Rest class to construct the question.)
        /// </summary>
        /// <param name="request">The REST request to call.</param>
        /// <param name="fileExtension">The extension for the file to download.</param>
        internal RestApiDownload(IRestRequest request, string fileExtension = null) :
            base(request) => FileExtension = fileExtension;

        #endregion

        #region Methods

        /// <summary>
        /// Executes the request using the given client.
        /// </summary>
        /// <param name="client">The RestSharp client.</param>
        /// <returns></returns>
        protected override IRestResponse Execute(IRestClient client) => client.Execute(Request);

        /// <summary>
        /// Calls the REST request and returns the downloaded file data as a byte array.
        /// Throws a RestApiDownloadException if the request's response code is a client or server error or response status is a transport error.
        /// </summary>
        /// <param name="actor">The Screenplay actor.</param>
        /// <returns></returns>
        public override byte[] RequestAs(IActor actor) => CallDownload(actor, FileExtension);

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"REST API download using {typeof(TAbility)}: {Request.Method} '{Request.Resource}'";

        #endregion
    }
}
