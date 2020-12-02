using Boa.Constrictor.Dumping;
using Boa.Constrictor.Screenplay;
using RestSharp;
using System;
using System.Net;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Enables the actor to make REST API calls using RestSharp.
    /// It constructs and holds one RestSharp client for the given base URL.
    /// This ability also holds dumpers for requests/responses and downloaded files.
    /// If dumpers are null, then no dumping is performed.
    /// This ability also handles adding and retrieving cookies.
    /// 
    /// To use more than one RestSharp client, create subclasses of this ability.
    /// The subclass will bear a unique type.
    /// Then, the Actor can use that subclass as a new type of ability for lookup.
    /// RestSharp interactions without type generics use the CallRestApi ability,
    /// while interactions with type generics will use the given ability by type.
    /// </summary>
    public class CallRestApi : IAbility
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the static methods for public construction.)
        /// </summary>
        /// <param name="baseUrl">The base URL for the RestSharp client.</param>
        private CallRestApi(string baseUrl)
        {
            Client = new RestClient()
            {
                BaseUrl = new Uri(baseUrl),
                CookieContainer = new CookieContainer()
            };

            RequestDumper = null;
            DownloadDumper = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The RestSharp client.
        /// </summary>
        public IRestClient Client { get; private set; }

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
        /// </summary>
        /// <param name="baseUrl">The base URL for the RestSharp client.</param>
        /// <returns></returns>
        public static CallRestApi At(string baseUrl) => new CallRestApi(baseUrl);

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
        /// Adds a cookie to the RestSharp client.
        /// </summary>
        /// <param name="cookie"></param>
        public void AddCookie(Cookie cookie) => Client.CookieContainer.Add(cookie);

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
        /// Gets a cookie from the RestSharp client by name.
        /// If the cookie does not exist, then this method returns null.
        /// </summary>
        /// <param name="name">The cookie name.</param>
        /// <returns></returns>
        public Cookie GetCookie(string name) => Client.CookieContainer.GetCookies(Client.BaseUrl)[name];

        /// <summary>
        /// Returns a description of this ability.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"call REST API at: {Client.BaseUrl}";

        #endregion
    }
}
