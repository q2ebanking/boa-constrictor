using System;
using System.Collections.Generic;

namespace Boa.Constrictor.Safety
{
    /// <summary>
    /// "Safe" actions are a sequence of Action calls that should be executed despite any exceptions.
    /// If an action causes an exception, then the exception should be caught and stored for the future.
    /// This allows all actions to be attempted before aborting.
    /// 
    /// One common example of safe actions are "soft assertions".
    /// A "soft assertion" is an assertion that does not immediately abort a test.
    /// For example, a test may need to verify the existence of two things on a Web page.
    /// With traditional "hard" assertions,
    /// the second thing would not be checked if the first thing does not exist,
    /// because the assertion for the first thing would abort the test.
    /// With soft assertions, however, an abort would not happen until after both things are checked.
    /// 
    /// Another example is a chain of cleanup routines.
    /// Some test frameworks, like SpecFlow, allow mutiple cleanup methods.
    /// However, if one aborts due to an exception, the following cleanup methods are not executed.
    /// Safe actions enable all cleanup routines to complete before aborting.
    /// 
    /// Safe actions may be called as a static method or as an object.
    /// It is typically best (and easiest) to use the static methods.
    /// Failure handlers may also be provided to customize what happens after a failure happens.
    /// </summary>
    public class SafeActions
    {
        #region Class Methods

        /// <summary>
        /// Performs a list of actions safely.
        /// Any exceptions that happen are bundled into a new SafeActionsException.
        /// All actions are performed before any SafeActionsException is thrown.
        /// </summary>
        /// <param name="actions">The list of actions.</param>
        /// <param name="failureHandler">A handler to be called whenever a failure happens.</param>
        public static void Safely(IEnumerable<Action> actions, Action<Exception> failureHandler = null) =>
            new SafeActions(failureHandler).Attempt(actions).ThrowAnyFailures();

        /// <summary>
        /// Performs a list of actions safely.
        /// Any exceptions that happen are bundled into a new SafeActionsException.
        /// All actions are performed before any SafeActionsException is thrown.
        /// This method cannot take a failure handler.
        /// </summary>
        /// <param name="actions">The list of actions.</param>
        public static void Safely(params Action[] actions) =>
            Safely((IEnumerable<Action>)actions);

        #endregion

        #region Properties

        /// <summary>
        /// The list of exceptions from failures.
        /// </summary>
        private IList<Exception> Failures { get; }

        /// <summary>
        /// A handler to be called whenever a failure happens.
        /// The handler will be called after adding the exception to the Failures list.
        /// Its single argument is the exception causing the failure.
        /// </summary>
        public Action<Exception> FailureHandler { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Basic constructor.
        /// Initializes the failure list to be empty.
        /// </summary>
        /// <param name="failureHandler">A handler to be called whenever a failure happens.</param>
        public SafeActions(Action<Exception> failureHandler = null)
        {
            Failures = new List<Exception>();
            FailureHandler = failureHandler ?? ((Exception e) => { return; });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Attempt each operation.
        /// Any exceptions from failures are caught and stored for later.
        /// </summary>
        /// <param name="actions">The list of actions.</param>
        /// <returns></returns>
        public SafeActions Attempt(IEnumerable<Action> actions)
        {
            foreach (Action action in actions)
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Failures.Add(e);
                    FailureHandler(e);
                }
            }

            return this;
        }

        /// <summary>
        /// Attempt each operation.
        /// Any exceptions from failures are caught and stored for later.
        /// </summary>
        /// <param name="actions">The list of actions.</param>
        /// <returns></returns>
        public SafeActions Attempt(params Action[] actions) =>
            Attempt((IEnumerable<Action>)actions);

        /// <summary>
        /// Throws a SafeActionsException if any failures happened during actions.
        /// The exception bundles all failures together.
        /// </summary>
        public void ThrowAnyFailures()
        {
            if (Failures.Count > 0)
                throw new SafeActionsException(Failures);
        }

        #endregion
    }
}
