using Boa.Constrictor.Logging;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// A Screenplay actor.
    /// An actor can perform Tasks and ask Questions based on his/her Abilities.
    /// </summary>
    public interface IActor
    {
        #region Properties

        /// <summary>
        /// The logger.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// The name of the actor.
        /// </summary>
        string Name { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Asks a Question and returns the answer value.
        /// The actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question);

        /// <summary>
        /// Alias for "AsksFor".
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        TAnswer AskingFor<TAnswer>(IQuestion<TAnswer> question);

        /// <summary>
        /// Performs a Task.
        /// The actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        void AttemptsTo(ITask task);

        /// <summary>
        /// Performs multiple Tasks
        /// The actor must have the Abilities needed by the Task(s).
        /// </summary>
        /// <param name="tasks">The Tasks to perform.</param>
        void AttemptsTo(params ITask[] tasks);

        /// <summary>
        /// Asks a Question and returns the answer value.
        /// The actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        TAnswer Calls<TAnswer>(IQuestion<TAnswer> question);

        /// <summary>
        /// Performs a Task.
        /// The actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        void Calls(ITask task);

        /// <summary>
        /// Adds an Ability.
        /// </summary>
        /// <param name="ability">The Ability to add.</param>
        void Can(IAbility ability);

        /// <summary>
        /// Checks if the actor has the Ability.
        /// </summary>
        /// <typeparam name="TAbility">The Ability type.</typeparam>
        /// <returns></returns>
        bool HasAbilityTo<TAbility>() where TAbility : IAbility;

        /// <summary>
        /// Gets one of the actor's Abilities by type so that it may be used.
        /// </summary>
        /// <typeparam name="TAbility">The Ability type.</typeparam>
        /// <returns></returns>
        TAbility Using<TAbility>() where TAbility : IAbility;

        #endregion
    }
}
