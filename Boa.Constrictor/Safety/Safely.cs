using Boa.Constrictor.Screenplay;

namespace Boa.Constrictor.Safety
{
    /// <summary>
    /// Runs Tasks safely using SafeActions.
    /// Actor must have the RunSafeActions Ability.
    /// </summary>
    public class Safely : ITask
    {
        #region Properties

        /// <summary>
        /// The Task to run safely.
        /// </summary>
        private ITask Task { get; set; }

        #endregion

        #region Constructors 

        /// <summary>
        /// Private constructor.
        /// (Use the static methods for public construction.)
        /// </summary>
        /// <param name="task">The Task to run safely.</param>
        private Safely(ITask task) => Task = task;

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs this Task.
        /// </summary>
        /// <param name="task">The Task to run safely.</param>
        /// <returns></returns>
        public static Safely Run(ITask task) => new Safely(task);

        #endregion

        #region Methods

        /// <summary>
        /// Runs the Task safely.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public void PerformAs(IActor actor)
        {
            void action() => actor.AttemptsTo(Task);
            var ability = actor.Using<RunSafeActions>();
            ability.Safely.Attempt(action);
        }

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"safely {Task}";

        #endregion
    }
}
