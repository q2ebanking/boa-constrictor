using Boa.Constrictor.Dumping;
using Boa.Constrictor.Screenplay;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Enables the actor to make REST API service calls using RestSharp.
    /// It holds a RestClient object per base URL.
    /// Each RestClient is given a CookieContainer.
    /// It also holds dumpers for requests/responses and downloaded files.
    /// If dumpers are null, then no dumping is performed.
    /// </summary>
    public class CallRestApi : IAbility
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the static methods for public construction.)
        /// </summary>
        private CallRestApi()
        {
            Clients = new Dictionary<string, IRestClient>();
            RequestDumper = null;
            DownloadDumper = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The available REST clients.
        /// The keys are the base URLs.
        /// </summary>
        private IDictionary<string, IRestClient> Clients { get; }

        /// <summary>
        /// The base URLs for the available REST clients.
        /// </summary>
        public ICollection<string> BaseUrls => Clients.Keys;

        /// <summary>
        /// The dumper for requests and responses.
        /// </summary>
        public JsonDumper RequestDumper { get; private set; }

        /// <summary>
        /// The dumper for downloaded files.
        /// </summary>
        public ByteDumper DownloadDumper { get; private set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs this ability.
        /// Initially, no REST API clients are available.
        /// They must be added.
        /// </summary>
        /// <returns></returns>
        public static CallRestApi UsingRestSharp() => new CallRestApi();

        /// <summary>
        /// Sets the ability to dump requests/responses to the given path.
        /// </summary>
        /// <param name="dumpDir">The dump directory path.</param>
        /// <param name="fileToken">The file token.</param>
        /// <returns></returns>
        public CallRestApi DumpingRequestsTo(string dumpDir, string fileToken = "Request")
        {
            RequestDumper = new JsonDumper("REST Request Dumper", dumpDir, fileToken);
            return this;
        }

        /// <summary>
        /// Sets the ability to dump requests/responses to the given path.
        /// </summary>
        /// <param name="dumpDir">The dump directory path.</param>
        /// <param name="fileToken">The file token.</param>
        /// <returns></returns>
        public CallRestApi DumpingDownloadsTo(string dumpDir, string fileToken = "Download")
        {
            DownloadDumper = new ByteDumper("REST Download Dumper", dumpDir, fileToken);
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a REST client for the given base URL.
        /// The client will be given a CookieContainer.
        /// If a client already exists for the base URL, then it is overwritten.
        /// </summary>
        /// <param name="baseUrl">The REST client's base URL.</param>
        public void AddClient(string baseUrl)
        {
            IRestClient client = new RestClient()
            {
                BaseUrl = new Uri(baseUrl),
                CookieContainer = new System.Net.CookieContainer()
            };

            Clients.Add(baseUrl, client);
        }

        /// <summary>
        /// Checks if downloads can be dumped.
        /// </summary>
        /// <returns></returns>
        public bool CanDumpDownloads() => DownloadDumper != null;

        /// <summary>
        /// Checks if requests and responses can be dumped
        /// </summary>
        /// <returns></returns>
        public bool CanDumpRequests() => RequestDumper != null;

        /// <summary>
        /// Gets a REST client by its base URL.
        /// Optionally can create a client for the base URL if one does not already exist.
        /// Throws a RestApiException if no client exists and one should not be created.
        /// </summary>
        /// <param name="baseUrl">The REST client's base URL.</param>
        /// <param name="addIfMissing">If true, add a new client for the base URL if one does not already exists.</param>
        /// <returns></returns>
        public IRestClient GetClient(string baseUrl, bool addIfMissing = true)
        {
            if (!HasClient(baseUrl))
            {
                if (addIfMissing)
                    AddClient(baseUrl);
                else
                    throw new RestApiException($"Ability '{GetType()}' does not contain a REST client with the base URL '{baseUrl}'");
            }

            return Clients[baseUrl];
        }

        /// <summary>
        /// Checks if this ability has a REST client for the given base URL.
        /// </summary>
        /// <param name="baseUrl">The REST client's base URL.</param>
        /// <returns></returns>
        public bool HasClient(string baseUrl) => Clients.ContainsKey(baseUrl);

        /// <summary>
        /// Returns a description of this ability.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string message = "call REST APIs";

            if (Clients.Keys.Count > 0)
                message += $" at {string.Join(", ", Clients.Keys)}";

            return message;
        }

        #endregion
    }
}
