using Boa.Constrictor.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// A screenplay Actor that implements IActor.
    /// An Actor can perform Tasks and ask Questions based on his/her Abilities.
    /// </summary>
    public class Actor : IActor
    {
        #region Constants

        /// <summary>
        /// The default name to use if no Actor name is provided.
        /// </summary>
        public const string DefaultName = "Screenplayer";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// Initializes the Abilities to be empty.
        /// </summary>
        /// <param name="name">The name of the Actor. If null, use DefaultName.</param>
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
        /// The collection of Abilities.
        /// Abilities are resolved by type name.
        /// </summary>
        private IDictionary<Type, IAbility> Abilities { get; set; }

        /// <summary>
        /// The logger.
        /// </summary>
        public ILogger Logger { get; }

        /// <summary>
        /// The name of the Actor.
        /// </summary>
        public string Name { get; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calls a Question synchronously.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <param name="enterPhrase">The phrase to print before calling.</param>
        /// <param name="exitPhrase">The phrase to print after calling.</param>
        /// <returns></returns>
        private TAnswer CallQuestion<TAnswer>(IQuestion<TAnswer> question, string enterPhrase, string exitPhrase)
        {
            Logger.Info($"{this} {enterPhrase} {question}");
            TAnswer answer = question.RequestAs(this);
            Logger.Info($"{question} {exitPhrase} {answer}");
            return answer;
        }

        /// <summary>
        /// Calls a Question asynchronously.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <param name="enterPhrase">The phrase to print before calling.</param>
        /// <param name="exitPhrase">The phrase to print after calling.</param>
        /// <returns></returns>
        private async Task<TAnswer> CallQuestionAsync<TAnswer>(IQuestionAsync<TAnswer> question, string enterPhrase, string exitPhrase)
        {
            Logger.Info($"{this} {enterPhrase} {question}");
            TAnswer answer = await question.RequestAsAsync(this);
            Logger.Info($"{question} {exitPhrase} {answer}");
            return answer;
        }

        /// <summary>
        /// Calls a Task synchronously.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        /// <param name="enterPhrase">The phrase to print before calling.</param>
        /// <param name="exitPhrase">The phrase to print after calling.</param>
        public void CallTask(ITask task, string enterPhrase, string exitPhrase)
        {
            Logger.Info($"{this} {enterPhrase} {task}");
            task.PerformAs(this);
            Logger.Info($"{this} {exitPhrase} {task}");
        }

        /// <summary>
        /// Calls a Task asynchronously.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        /// <param name="enterPhrase">The phrase to print before calling.</param>
        /// <param name="exitPhrase">The phrase to print after calling.</param>
        public async Task CallTaskAsync(ITaskAsync task, string enterPhrase, string exitPhrase)
        {
            Logger.Info($"{this} {enterPhrase} {task}");
            await task.PerformAsAsync(this);
            Logger.Info($"{this} {exitPhrase} {task}");
        }

        #endregion

        #region IActor Methods

        /// <summary>
        /// Asks a Question and returns the answer value.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        public TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question)
        {
            return CallQuestion(question, "asks for", "was");
        }

        /// <summary>
        /// Asks a Question and returns the answer value asynchronously.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        public async Task<TAnswer> AsksForAsync<TAnswer>(IQuestionAsync<TAnswer> question)
        {
            return await CallQuestionAsync(question, "asks for", "was");
        }

        /// <summary>
        /// Asks a Question and returns the answer value.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        public TAnswer AskingFor<TAnswer>(IQuestion<TAnswer> question)
        {
            return CallQuestion(question, "asking for", "was");
        }

        /// <summary>
        /// Asks a Question and returns the answer value asynchronously.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        /// <returns></returns>
        public async Task<TAnswer> AskingForAsync<TAnswer>(IQuestionAsync<TAnswer> question)
        {
            return await CallQuestionAsync(question, "asking for", "was");
        }

        /// <summary>
        /// Performs a Task.
        /// The Actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        public void AttemptsTo(ITask task)
        {
            CallTask(task, "attempts to", "successfully did");
        }

        /// <summary>
        /// Performs a Task asynchronously.
        /// The Actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        public async Task AttemptsToAsync(ITaskAsync task)
        {
            await CallTaskAsync(task, "attempts to", "successfully did");
        }

        /// <summary>
        /// Performs multiple Tasks
        /// The Actor must have the Abilities needed by the Task(s).
        /// </summary>
        /// <param name="tasks">The Tasks to perform.</param>
        public void AttemptsTo(params ITask[] tasks)
        {
            foreach (ITask doTheNeedful in tasks)
                AttemptsTo(doTheNeedful);
        }

        /// <summary>
        /// Asks a Question and returns the answer value.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        public TAnswer Calls<TAnswer>(IQuestion<TAnswer> question)
        {
            return CallQuestion(question, "calls", "returned:");
        }

        /// <summary>
        /// Asks a Question and returns the answer value asynchronously.
        /// The Actor must have the Abilities needed by the Question.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question to ask.</param>
        public async Task<TAnswer> CallsAsync<TAnswer>(IQuestionAsync<TAnswer> question)
        {
            return await CallQuestionAsync(question, "calls", "returned:");
        }

        /// <summary>
        /// Performs a Task.
        /// The Actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        public void Calls(ITask task)
        {
            CallTask(task, "calls", "successfully called");
        }

        /// <summary>
        /// Performs a Task asynchronously.
        /// The Actor must have the Abilities needed by the Task.
        /// </summary>
        /// <param name="task">The Task to perform.</param>
        public async Task CallsAsync(ITaskAsync task)
        {
            await CallTaskAsync(task, "calls", "successfully called");
        }

        /// <summary>
        /// Adds an Ability.
        /// </summary>
        /// <param name="ability">The Ability to add.</param>
        public void Can(IAbility ability)
        {
            Logger.Info($"Adding Ability for {this} to {ability}");
            Abilities.Add(ability.GetType(), ability);
        }

        /// <summary>
        /// Checks if the Actor has the Ability.
        /// </summary>
        /// <typeparam name="TAbility">The Ability type.</typeparam>
        /// <returns></returns>
        public bool HasAbilityTo<TAbility>() where TAbility : IAbility
        {
            return Abilities.ContainsKey(typeof(TAbility));
        }

        /// <summary>
        /// Gets one of the Actor's Abilities by type so that it may be used.
        /// </summary>
        /// <typeparam name="TAbility">The Ability type.</typeparam>
        /// <returns></returns>
        public TAbility Using<TAbility>() where TAbility : IAbility
        {
            Type t = typeof(TAbility);

            if (!Abilities.ContainsKey(t))
                throw new ScreenplayException($"{this} does not have the Ability '{t}'");

            return (TAbility)Abilities[t];
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Returns the name of this screenplay Actor.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Screenplay Actor '{Name}'";
        }

        #endregion
    }
}
