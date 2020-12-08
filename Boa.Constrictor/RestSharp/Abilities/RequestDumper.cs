using Boa.Constrictor.Dumping;
using RestSharp;
using System;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Dumps RestSharp requests and responses.
    /// </summary>
    public class RequestDumper : JsonDumper
    {
        #region Properties

        /// <summary>
        /// The last request object dumped.
        /// Warning: it might be null.
        /// </summary>
        public IRestRequest LastRequest { get; private set; } = null;

        /// <summary>
        /// The last response object dumped.
        /// Warning: it might be null.
        /// </summary>
        public IRestResponse LastResponse { get; private set; } = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A descriptive name for the dumper.</param>
        /// <param name="dumpDir">The output directory for dumping requests and responses.</param>
        /// <param name="fileToken">The token for the file name.</param>
        public RequestDumper(string name, string dumpDir, string fileToken) :
            base(name, dumpDir, fileToken) { }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the JSON dump file to the dump directory for the request and the response.
        /// Returns the dumped file's path.
        /// </summary>
        /// <param name="client">RestSharp client.</param>
        /// <param name="request">Request object.</param>
        /// <param name="response">Response object.</param>
        /// <param name="start">Request's start time.</param>
        /// <param name="end">Request's end time.</param>
        /// <returns></returns>
        public string Dump(
            IRestClient client,
            IRestRequest request = null,
            IRestResponse response = null,
            DateTime? start = null,
            DateTime? end = null)
        {
            LastRequest = request;
            LastResponse = response;

            return Dump(new FullRestData(client, request, response, start, end));
        }

        #endregion
    }
}
