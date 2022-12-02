namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Question to get an answer from the answer cache.
    /// If the answer is cached, this returns that value.
    /// If the answer is not cached, this calls the Question, caches the answer, and returns the value.
    /// Questions to cache must implement `ICacheableQuestion`.
    /// WARNING: Do NOT cache answers to every Question.
    /// Only cache answers that you know will be fairly constant.
    /// </summary>
    /// <typeparam name="TAnswer"></typeparam>
    public class CachedAnswer<TAnswer> : IQuestion<TAnswer>
    {
        /// <summary>
        /// The target Question.
        /// </summary>
        public ICacheableQuestion<TAnswer> Question { get; private set; }

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="question">The target Question.</param>
        private CachedAnswer(ICacheableQuestion<TAnswer> question) => Question = question;

        /// <summary>
        /// Builder method.
        /// </summary>
        /// <param name="question">The target Question.</param>
        /// <returns></returns>
        public static CachedAnswer<TAnswer> For(ICacheableQuestion<TAnswer> question) =>
            new CachedAnswer<TAnswer>(question);

        /// <summary>
        /// Gets the answer to the Question through the cache.
        /// If the answer is cached, this returns that value.
        /// If the answer is not cached, this calls the Question, caches the answer, and returns the value.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public TAnswer RequestAs(IActor actor) =>
            actor.Using<CacheAnswers>().Cache.Get(Question, actor);
    }
}
