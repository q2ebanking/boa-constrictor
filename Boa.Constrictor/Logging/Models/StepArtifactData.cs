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
        /// Screenshot paths for the test step.
        /// </summary>
        [JsonProperty]
        public IList<string> Screenshots { get; private set; }

        /// <summary>
        /// Request dump paths for the test step.
        /// </summary>
        [JsonProperty]
        public IList<string> Requests { get; private set; }

        /// <summary>
        /// Downloaded file paths for the test step.
        /// </summary>
        [JsonProperty]
        public IList<string> Downloads { get; private set; }

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
            Screenshots = new List<string>();
            Requests = new List<string>();
            Downloads = new List<string>();
        }

        #endregion
    }
}
