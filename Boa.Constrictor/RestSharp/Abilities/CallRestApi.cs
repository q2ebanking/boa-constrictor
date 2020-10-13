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
    /// </summary>
    public class CallRestApi : IAbility
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the static methods for public construction.)
        /// </summary>
        private CallRestApi() => Clients = new Dictionary<string, IRestClient>();

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

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs this ability.
        /// Initially, no REST API clients are available.
        /// They must be added.
        /// </summary>
        /// <returns></returns>
        public static CallRestApi UsingRestSharp() => new CallRestApi();

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
