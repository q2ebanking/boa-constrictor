using Boa.Constrictor.Screenplay;

namespace Boa.Constrictor.Safety
{
    /// <summary>
    /// This Ability enables Actors to run Tasks using SafeActions.
    /// </summary>
    public class RunSafeActions : IAbility
    {
        #region Properties

        /// <summary>
        /// The SafeActions object.
        /// </summary>
        public SafeActions Safely { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use the static methods for public construction.)
        /// </summary>
        /// <param name="safe">The SafeActions object.</param>
        private RunSafeActions(SafeActions safe) => Safely = safe;

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs this Ability.
        /// </summary>
        /// <param name="safe">The SafeActions object.</param>
        /// <returns></returns>
        public static RunSafeActions Using(SafeActions safe) => new RunSafeActions(safe);

        #endregion

        #region Methods

        /// <summary>
        /// Returns a description of this Ability.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "run Tasks safely";

        #endregion
    }
}
