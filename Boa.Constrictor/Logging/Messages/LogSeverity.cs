namespace Boa.Constrictor.Logging
{
    /// <summary>
    /// Enum for log severity levels.
    /// The higher the value, the more severe the level.
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        /// Trace level.
        /// </summary>
        Trace = 0,

        /// <summary>
        /// Debug level.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Info level.
        /// </summary>
        Info = 2,

        /// <summary>
        /// Warning level.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Error level.
        /// </summary>
        Error = 4,

        /// <summary>
        /// Fatal level.
        /// </summary>
        Fatal = 5,
    }
}
