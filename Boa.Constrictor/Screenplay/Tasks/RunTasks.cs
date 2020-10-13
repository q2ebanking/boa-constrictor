using System.Collections.Generic;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Runs a list of other tasks.
    /// It can be a useful shortcut.
    /// Tasks are run in the order given by the list.
    /// </summary>
    public class RunTasks : ITask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="tasks"></param>
        private RunTasks(IEnumerable<ITask> tasks) => Tasks = tasks;

        #endregion

        #region Properties

        /// <summary>
        /// The task list.
        /// </summary>
        private IEnumerable<ITask> Tasks { get; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Builder method.
        /// </summary>
        /// <param name="tasks">The task list.</param>
        /// <returns></returns>
        public static RunTasks InOrder(IEnumerable<ITask> tasks) =>
            new RunTasks(tasks);

        /// <summary>
        /// Builder method.
        /// </summary>
        /// <param name="tasks">The task list.</param>
        public static RunTasks InOrder(params ITask[] tasks) =>
            new RunTasks(tasks);

        #endregion

        #region Methods

        /// <summary>
        /// Runs the tasks in the order given by the list.
        /// </summary>
        /// <param name="actor"></param>
        public void PerformAs(IActor actor)
        {
            foreach (ITask doTheNeedful in Tasks)
                actor.AttemptsTo(doTheNeedful);
        }

        /// <summary>
        /// Returns a description of the task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "Run multiple tasks in order";

        #endregion
    }
}
