namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Enables the Actor to use default timeout values.
    /// The "standard" timeout is the amount of time to wait by default.
    /// This may be overridden by individual calls.
    /// The "extra" timeout will be added to every timeout, even when the standard timeout is overridden.
    /// </summary>
    public class SetTimeouts : IAbility
    {
        #region Constants

        /// <summary>
        /// The default standard timeout value in seconds.
        /// </summary>
        public const int DefaultStandardTimeout = 30;

        /// <summary>
        /// The default extra timeout value in seconds.
        /// </summary>
        public const int DefaultExtraTimeout = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the static methods for public construction.)
        /// </summary>
        /// <param name="standardSeconds">The standard timeout value in seconds.</param>
        /// <param name="extraSeconds">The extra timeout value in seconds.</param>
        private SetTimeouts(int standardSeconds = DefaultStandardTimeout, int extraSeconds = DefaultExtraTimeout)
        {
            StandardSeconds = standardSeconds;
            ExtraSeconds = extraSeconds;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The standard timeout value in seconds.
        /// This value may be overridden when interactions are called.
        /// </summary>
        public int StandardSeconds { get; set; }

        /// <summary>
        /// The extra timeout value in seconds.
        /// This value is added to the timeout, even when the standard timeout is overridden.
        /// </summary>
        public int ExtraSeconds { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Ability.
        /// </summary>
        /// <param name="standardSeconds">The standard timeout value in seconds.</param>
        /// <param name="extraSeconds">The extra timeout value in seconds.</param>
        /// <returns></returns>
        public static SetTimeouts To(int standardSeconds = DefaultStandardTimeout, int extraSeconds = DefaultExtraTimeout) =>
            new SetTimeouts(standardSeconds, extraSeconds);

        /// <summary>
        /// Constructs the Ability using default values.
        /// </summary>
        /// <returns></returns>
        public static SetTimeouts ToDefaultValues() =>
            new SetTimeouts();

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the timeout to use for an interaction.
        /// If an override value is provided, it overrides the standard timeout.
        /// The extra timeout value is added even if an override value is provided.
        /// </summary>
        /// <param name="overrideSeconds">Overrides the standard timeout value.</param>
        /// <returns></returns>
        public int CalculateTimeout(int? overrideSeconds = null) =>
            (overrideSeconds ?? StandardSeconds) + ExtraSeconds;

        /// <summary>
        /// Returns a description of this Ability.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"use a standard timeout of {StandardSeconds}s and an extra timeout of {ExtraSeconds}s";

        #endregion
    }
}
