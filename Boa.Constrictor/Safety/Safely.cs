using Boa.Constrictor.Screenplay;

namespace Boa.Constrictor.Safety
{
    /// <summary>
    /// Runs Tasks safely using SafeActions.
    /// Actor must have the RunSafeActions ability.
    /// </summary>
    public class Safely : ITask
    {
        #region Properties

        /// <summary>
        /// The task to run safely.
        /// </summary>
        private ITask Task { get; set; }

        #endregion

        #region Constructors 

        /// <summary>
        /// Private constructor.
        /// (Use the static methods for public construction.)
        /// </summary>
        /// <param name="task">The task to run safely.</param>
        private Safely(ITask task) => Task = task;

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs this task.
        /// </summary>
        /// <param name="task">The task to run safely.</param>
        /// <returns></returns>
        public static Safely Run(ITask task) => new Safely(task);

        #endregion

        #region Methods

        /// <summary>
        /// Runs the task safely.
        /// </summary>
        /// <param name="actor"></param>
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
