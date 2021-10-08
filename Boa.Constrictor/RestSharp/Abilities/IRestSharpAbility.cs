using Boa.Constrictor.Dumping;
using Boa.Constrictor.Screenplay;
using RestSharp;
using System.Net;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Enables the Actor to make REST API calls using RestSharp.
    /// It holds one RestSharp client for the given base URL.
    /// This Ability also holds dumpers for requests/responses and downloaded files.
    /// If dumpers are null, then no dumping is performed.
    /// This Ability also handles adding and retrieving cookies.
    /// 
    /// To use more than one RestSharp client, create classes that implement this interface.
    /// Then, the Actor can use that class as a new type of Ability for lookup.
    /// </summary>
    public interface IRestSharpAbility : IAbility
    {
        #region Properties

        /// <summary>
        /// The RestSharp client.
        /// </summary>
        IRestClient Client { get; }

        /// <summary>
        /// The dumper for downloaded files.
        /// </summary>
        ByteDumper DownloadDumper { get; }

        /// <summary>
        /// The dumper for requests and responses.
        /// </summary>
        RequestDumper RequestDumper { get; }

        /// <summary>
        /// The last request object dumped.
        /// Warning: it might be null.
        /// </summary>
        IRestRequest LastRequest { get; }

        /// <summary>
        /// The last response object dumped.
        /// Warning: it might be null.
        /// </summary>
        IRestResponse LastResponse { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a cookie to the RestSharp client.
        /// </summary>
        /// <param name="cookie">The cookie to add to the RestSharp client.</param>
        void AddCookie(Cookie cookie);

        /// <summary>
        /// Checks if downloads can be dumped.
        /// </summary>
        /// <returns></returns>
        bool CanDumpDownloads();

        /// <summary>
        /// Checks if requests and responses can be dumped
        /// </summary>
        /// <returns></returns>
        bool CanDumpRequests();

        /// <summary>
        /// Gets a cookie from the RestSharp client by name.
        /// If the cookie does not exist, then this method returns null.
        /// </summary>
        /// <param name="name">The cookie name.</param>
        Cookie GetCookie(string name);

        #endregion
    }
}