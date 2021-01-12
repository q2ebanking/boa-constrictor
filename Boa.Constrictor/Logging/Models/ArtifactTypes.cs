namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Provides string constants for logging artifacts.
    /// Strings are used instead of enumerations so that loggers can technically log any type of artifact.
    /// </summary>
    public class ArtifactTypes
    {
        /// <summary>
        /// Artifact type for downloaded files.
        /// </summary>
        public const string Downloads = "Downloads";

        /// <summary>
        /// Artifact type for request data dump files.
        /// </summary>
        public const string Requests = "Requests";

        /// <summary>
        /// Artifact type for screenshot images.
        /// </summary>
        public const string Screenshots = "Screenshots";

        /// <summary>
        /// Artifact type for test log reports.
        /// </summary>
        public const string TestLogReports = "TestLogReports";

        /// <summary>
        /// Artifact type for test logs.
        /// </summary>
        public const string TestLogs = "TestLogs";
    }
}
