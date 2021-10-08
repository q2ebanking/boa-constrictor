using Boa.Constrictor.Dumping;
using RestSharp;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// The "default" RestSharp Ability.
    /// Enables the Actor to make REST API calls using RestSharp.
    /// It constructs and holds one RestSharp client for the given base URL.
    /// This Ability also holds dumpers for requests/responses and downloaded files.
    /// If dumpers are null, then no dumping is performed.
    /// This Ability also handles adding and retrieving cookies.
    /// 
    /// To use more than one RestSharp client, create subclasses of AbstractRestSharpAbility,
    /// or implement the IRestSharpAbility interface.
    /// Do not make subclasses of CallRestApi, or else static builders will break.
    /// The subclass will bear a unique type.
    /// Then, the Actor can use that subclass as a new type of Ability for lookup.
    /// RestSharp interactions without type generics use the CallRestApi Ability,
    /// while interactions with type generics will use the given Ability by type.
    /// </summary>
    public class CallRestApi : AbstractRestSharpAbility
    {
        #region Constructors

        /// <summary>
        /// Protected constructor.
        /// (Use static builder methods for public construction.)
        /// </summary>
        /// <param name="client">The RestSharp client.</param>
        protected CallRestApi(IRestClient client) : base(client) { }

        /// <summary>
        /// Protected constructor.
        /// (Use static builder methods for public construction.)
        /// </summary>
        /// <param name="baseUrl">The base URL for the RestSharp client.</param>
        protected CallRestApi(string baseUrl) : base(baseUrl) { }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs this Ability.
        /// </summary>
        /// <param name="baseUrl">The base URL for the RestSharp client.</param>
        /// <returns></returns>
        public static CallRestApi At(string baseUrl) => new CallRestApi(baseUrl);

        /// <summary>
        /// Constructs this Ability.
        /// </summary>
        /// <param name="client">The RestSharp client.</param>
        /// <returns></returns>
        public static CallRestApi Using(IRestClient client) => new CallRestApi(client);

        /// <summary>
        /// Sets the Ability to dump requests/responses to the given path.
        /// </summary>
        /// <param name="dumpDir">The dump directory path.</param>
        /// <param name="fileToken">The file token.</param>
        /// <returns></returns>
        public CallRestApi DumpingRequestsTo(string dumpDir, string fileToken = "Request")
        {
            RequestDumper = new RequestDumper("REST Request Dumper", dumpDir, fileToken);
            return this;
        }

        /// <summary>
        /// Sets the Ability to dump requests/responses to the given path.
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
    }
}
