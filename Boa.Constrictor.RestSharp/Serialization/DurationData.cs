using System;

namespace Boa.Constrictor.RestSharp
{
    /// <summary>
    /// Serialization class for duration.
    /// </summary>
    public class DurationData
    {
        #region Properties

        /// <summary>
        /// The start time.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// The end time.
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// The duration time, which is end time minus start time.
        /// </summary>
        public TimeSpan? Duration =>
            (StartTime == null || EndTime == null)
            ? null : EndTime - StartTime;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start">The start time.</param>
        /// <param name="end">The end time.</param>
        public DurationData(DateTime? start = null, DateTime? end = null)
        {
            StartTime = start;
            EndTime = end;
        }

        #endregion
    }
}
