using Boa.Constrictor.Logging;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// A screenplay actor that implements IActor.
    /// An actor can perform tasks and ask questions based on his/her abilities.
    /// </summary>
    public class Actor : IActor
    {
        #region Constants

        /// <summary>
        /// The default name to use if no actor name is provided.
        /// </summary>
        public const string DefaultName = "Screenplayer";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// Initializes the abilities to be empty.
        /// </summary>
        /// <param name="name">The name of the actor. If null, use DefaultName.</param>
        /// <param name="logger">The logger. If null, use a NoOpLogger.</param>
        public Actor(string name = null, ILogger logger = null)
        {
            Abilities = new Dictionary<Type, IAbility>();
            Logger = logger ?? new NoOpLogger();
            Name = name ?? DefaultName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The collection of abilities.
        /// Abilities are resolved by type name.
        /// </summary>
        private IDictionary<Type, IAbility> Abilities { get; set; }

        /// <summary>
        /// The logger.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// The name of the actor.
        /// </summary>
        public string Name { get; }

        #endregion

        #region IActor Methods

        /// <summary>
        /// Asks a question and returns the answer value.
        /// The actor must have the abilities needed by the question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The question to ask.</param>
        /// <returns></returns>
        public TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question)
        {
            Logger.Info($"{this} asks for {question}");
            TAnswer answer = question.RequestAs(this);
            Logger.Info($"{question} was {answer}");
            return answer;
        }

        /// <summary>
        /// Alias for "AsksFor".
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The question to ask.</param>
        /// <returns></returns>
        public TAnswer AskingFor<TAnswer>(IQuestion<TAnswer> question)
        {
            return AsksFor(question);
        }

        /// <summary>
        /// Performs a task.
        /// The actor must have the abilities needed by the task.
        /// </summary>
        /// <param name="task">The task to perform.</param>
        public void AttemptsTo(ITask task)
        {
            Logger.Info($"{this} attempts to {task}");
            task.PerformAs(this);
            Logger.Info($"{this} successfully did {task}");
        }

        /// <summary>
        /// Performs multiple tasks
        /// The actor must have the abilities needed by the task(s).
        /// </summary>
        /// <param name="tasks">The tasks to perform.</param>
        public void AttemptsTo(params ITask[] tasks)
        {
            foreach(ITask task in tasks)
            {
                AttemptsTo(task);
            }
        }

        /// <summary>
        /// Asks a question and returns the answer value.
        /// The actor must have the abilities needed by the question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The question to ask.</param>
        public TAnswer Calls<TAnswer>(IQuestion<TAnswer> question)
        {
            Logger.Info($"{this} calls {question}");
            TAnswer answer = question.RequestAs(this);
            Logger.Info($"{question} return \"{answer}\"");
            return answer;
        }

        /// <summary>
        /// Performs a task.
        /// The actor must have the abilities needed by the task.
        /// </summary>
        /// <param name="task">The task to perform.</param>
        public void Calls(ITask task)
        {
            Logger.Info($"{this} calls {task}");
            task.PerformAs(this);
            Logger.Info($"{this} successfully called {task}");
        }

        /// <summary>
        /// Adds an ability.
        /// </summary>
        /// <param name="ability">The ability to add.</param>
        public void Can(IAbility ability)
        {
            Logger.Info($"Adding ability for {this} to {ability}");
            Abilities.Add(ability.GetType(), ability);
        }

        /// <summary>
        /// Checks if the actor has the ability.
        /// </summary>
        /// <typeparam name="TAbility">The ability type.</typeparam>
        /// <returns></returns>
        public bool HasAbilityTo<TAbility>() where TAbility : IAbility
        {
            return Abilities.ContainsKey(typeof(TAbility));
        }

        /// <summary>
        /// Gets one of the actor's abilities by type so that it may be used.
        /// </summary>
        /// <typeparam name="TAbility">The ability type.</typeparam>
        /// <returns></returns>
        public TAbility Using<TAbility>() where TAbility : IAbility
        {
            Type t = typeof(TAbility);

            if (!Abilities.ContainsKey(t))
                throw new ScreenplayException($"{this} does not have the ability '{t}'");

            return (TAbility)Abilities[t];
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Returns the name of this screenplay actor.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Screenplay actor '{Name}'";
        }

        #endregion
    }
}
