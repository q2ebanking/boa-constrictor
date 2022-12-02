using Newtonsoft.Json;
using System.Collections.Generic;

namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Models data for one test step.
    /// Can be used directly for JSON serialization.
    /// Used by TestLogger.
    /// </summary>
    public class StepArtifactData
    {
        #region Properties

        /// <summary>
        /// The name of the test step.
        /// </summary>
        [JsonProperty]
        public string Name { get; private set; }

        /// <summary>
        /// Log messages for the test step.
        /// </summary>
        [JsonProperty]
        public IList<string> Messages { get; private set; }

        /// <summary>
        /// Artifacts for the steps.
        /// Each artifact has a type (which is the key name) and a path (which should be a list entry).
        /// Strings are used so that callers can use any artifact type for a key.
        /// </summary>
        [JsonProperty]
        public IDictionary<string, IList<string>> Artifacts { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// Initializes the step name to be blank.
        /// Initializes all artifact lists to be empty.
        /// </summary>
        public StepArtifactData() : this("") { }

        /// <summary>
        /// Constructor.
        /// Initializes all artifact lists to be empty.
        /// </summary>
        /// <param name="name">The name of the test step.</param>
        public StepArtifactData(string name)
        {
            Name = name;
            Messages = new List<string>();
            Artifacts = new Dictionary<string, IList<string>>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an artifact to this step data.
        /// Artifacts are files, like screenshot images or JSON data dumps.
        /// </summary>
        /// <param name="type">The type of artifact to add.</param>
        /// <param name="path">The file path to the artifact.</param>
        public void AddArtifact(string type, string path)
        {
            if (!Artifacts.ContainsKey(type))
                Artifacts[type] = new List<string>();

            Artifacts[type].Add(path);
        }

        #endregion
    }
}
