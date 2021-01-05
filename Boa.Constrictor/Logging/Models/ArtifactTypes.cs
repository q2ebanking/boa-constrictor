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
        public const string Download = "Download";

        /// <summary>
        /// Artifact type for request data dump files.
        /// </summary>
        public const string Request = "Request";

        /// <summary>
        /// Artifact type for screenshot images.
        /// </summary>
        public const string Screenshot = "Screenshot";
    }
}
