using Boa.Constrictor.Dumping;
using RestSharp;
using System.Net;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Abstract parent class for Abilities that enable the Actor to make REST API calls using RestSharp.
    /// It constructs and holds one RestSharp client for the given base URL.
    /// This Ability also holds dumpers for requests/responses and downloaded files.
    /// If dumpers are null, then no dumping is performed.
    /// This Ability also handles adding and retrieving cookies.
    /// 
    /// To use more than one RestSharp client, create subclasses of this abstract class,
    /// or implement the IRestSharpAbility interface.
    /// Do not make subclasses of CallRestApi, or else static builders will break.
    /// The subclass will bear a unique type.
    /// Then, the Actor can use that subclass as a new type of Ability for lookup.
    /// RestSharp interactions without type generics use the CallRestApi Ability,
    /// while interactions with type generics will use the given Ability by type.
    /// </summary>
    public class AbstractRestSharpAbility : IRestSharpAbility
    {
        #region Constructors

        /// <summary>
        /// Protected constructor.
        /// (Use static methods in a subclass for public construction.)
        /// </summary>
        /// <param name="client">The RestSharp client.</param>
        protected AbstractRestSharpAbility(IRestClient client)
        {
            Client = client;
            RequestDumper = null;
            DownloadDumper = null;

            if (Client.CookieContainer == null)
                Client.CookieContainer = new CookieContainer();
        }

        /// <summary>
        /// Protected constructor.
        /// (Use static methods in a subclass for public construction.)
        /// </summary>
        /// <param name="baseUrl">The base URL for the RestSharp client.</param>
        protected AbstractRestSharpAbility(string baseUrl) :
            this(new RestClient(baseUrl))
        { }

        #endregion

        #region Properties

        /// <summary>
        /// The RestSharp client.
        /// </summary>
        public IRestClient Client { get; protected set; }

        /// <summary>
        /// The dumper for downloaded files.
        /// </summary>
        public ByteDumper DownloadDumper { get; protected set; }

        /// <summary>
        /// The dumper for requests and responses.
        /// </summary>
        public RequestDumper RequestDumper { get; protected set; }

        /// <summary>
        /// The last request object dumped.
        /// Warning: it might be null.
        /// </summary>
        public IRestRequest LastRequest => RequestDumper.LastRequest;

        /// <summary>
        /// The last response object dumped.
        /// Warning: it might be null.
        /// </summary>
        public IRestResponse LastResponse => RequestDumper.LastResponse;

        #endregion

        #region Methods

        /// <summary>
        /// Adds a cookie to the RestSharp client.
        /// </summary>
        /// <param name="cookie">The cookie to add to the RestSharp client.</param>
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
        /// Returns a description of this Ability.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"call REST API at: {Client.BaseUrl}";

        #endregion
    }
}
