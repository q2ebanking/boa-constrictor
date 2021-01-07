using Newtonsoft.Json;
using System.Collections.Generic;

namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Models data for one test case.
    /// Can be used directly for JSON serialization.
    /// Used by TestLogger.
    /// </summary>
    public class TestLogData
    {
        #region Properties

        /// <summary>
        /// The test name.
        /// </summary>
        [JsonProperty]
        public string Name { get; private set; }
        
        /// <summary>
        /// The test result.
        /// This is a string value so that it may be free form.
        /// </summary>
        [JsonProperty]
        public string Result { get; set; }

        /// <summary>
        /// The test steps.
        /// </summary>
        [JsonProperty]
        public IList<StepArtifactData> Steps { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// Initializes the test name to be blank.
        /// Initializes the list of steps to be empty.
        /// </summary>
        public TestLogData() : this("") { }

        /// <summary>
        /// Constructor.
        /// Initializes the list of steps to be empty.
        /// </summary>
        /// <param name="name">The test name.</param>
        public TestLogData(string name)
        {
            Name = name;
            Result = null;
            Steps = new List<StepArtifactData>();
        }

        #endregion
    }
}
