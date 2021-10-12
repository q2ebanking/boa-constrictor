using Boa.Constrictor.Logging;
using System.Threading.Tasks;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// A Screenplay Actor.
    /// An Actor can perform Tasks and ask Questions based on his/her Abilities.
    /// </summary>
    public interface IActor
    {
        #region Properties

        /// <summary>
        /// The logger.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// The name of the Actor.
        /// </summary>
        string Name { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Asks a Question and returns the answer value.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question);
        
        /// <summary>
        /// Asks a Question and returns the answer value asynchronously.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        Task<TAnswer> AsksForAsync<TAnswer>(IQuestionAsync<TAnswer> question);

        /// <summary>
        /// Alias for "AsksFor".
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        TAnswer AskingFor<TAnswer>(IQuestion<TAnswer> question);

        /// <summary>
        /// Alias for "AsksFor".
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        Task<TAnswer> AskingForAsync<TAnswer>(IQuestionAsync<TAnswer> question);

        /// <summary>
        /// Performs a Task.
        /// The Actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        void AttemptsTo(ITask task);
        
        /// <summary>
        /// Performs a Task asynchronously.
        /// The Actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        Task AttemptsToAsync(ITaskAsync task);

        /// <summary>
        /// Performs multiple Tasks
        /// The Actor must have the Abilities needed by the Task(s).
        /// </summary>
        /// <param name="tasks">The Tasks to perform.</param>
        void AttemptsTo(params ITask[] tasks);

        /// <summary>
        /// Asks a Question and returns the answer value.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        TAnswer Calls<TAnswer>(IQuestion<TAnswer> question);

        /// <summary>
        /// Asks a Question and returns the answer value asynchronously.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        Task<TAnswer> CallsAsync<TAnswer>(IQuestionAsync<TAnswer> question);

        /// <summary>
        /// Performs a Task.
        /// The Actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        void Calls(ITask task);

        /// <summary>
        /// Performs a Task asynchronously.
        /// The Actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        Task CallsAsync(ITaskAsync task);

        /// <summary>
        /// Adds an Ability.
        /// </summary>
        /// <param name="ability">The Ability to add.</param>
        void Can(IAbility ability);

        /// <summary>
        /// Checks if the Actor has the Ability.
        /// </summary>
        /// <typeparam name="TAbility">The Ability type.</typeparam>
        /// <returns></returns>
        bool HasAbilityTo<TAbility>() where TAbility : IAbility;

        /// <summary>
        /// Gets one of the Actor's Abilities by type so that it may be used.
        /// </summary>
        /// <typeparam name="TAbility">The Ability type.</typeparam>
        /// <returns></returns>
        TAbility Using<TAbility>() where TAbility : IAbility;

        #endregion
    }
}
