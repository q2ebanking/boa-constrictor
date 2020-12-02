using Boa.Constrictor.Dumping;
using Boa.Constrictor.Screenplay;
using RestSharp;
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
    public interface IRestSharpAbility : IAbility
    {
        #region Properties

        /// <summary>
        /// The RestSharp client.
        /// </summary>
        IRestClient Client { get; }

        /// <summary>
        /// The dumper for requests and responses.
        /// </summary>
        ByteDumper DownloadDumper { get; }

        /// <summary>
        /// The dumper for downloaded files.
        /// </summary>
        JsonDumper RequestDumper { get; }

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