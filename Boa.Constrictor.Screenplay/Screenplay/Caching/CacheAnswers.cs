namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Enables the Actor to cache answers to Questions.
    /// Any Question may be cached as long as it implements the Equals and GetHashCode methods.
    /// WARNING: Do NOT cache answers to every Question.
    /// Only cache answers that you know will be fairly constant.
    /// </summary>
    public class CacheAnswers : IAbility
    {
        /// <summary>
        /// The answer cache.
        /// </summary>
        public AnswerCache Cache { get; private set; }

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="cache">The answer cache.</param>
        private CacheAnswers(AnswerCache cache) => Cache = cache;

        /// <summary>
        /// Builder method for this Ability.
        /// </summary>
        /// <param name="cache">The answer cache.</param>
        /// <returns></returns>
        public static CacheAnswers With(AnswerCache cache) => new CacheAnswers(cache);
    }
}
