namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Question to get an answer from the answer cache.
    /// If the answer is cached, this returns that value.
    /// If the answer is not cached, this calls the question, caches the answer, and returns the value.
    /// </summary>
    /// <typeparam name="TAnswer"></typeparam>
    public class CachedAnswer<TAnswer> : IQuestion<TAnswer>
    {
        /// <summary>
        /// The target Question.
        /// </summary>
        public IQuestion<TAnswer> Question { get; private set; }

        /// <summary>
        /// Private constructor.
        /// </summary>
        /// <param name="question">The target Question.</param>
        private CachedAnswer(IQuestion<TAnswer> question) => Question = question;

        /// <summary>
        /// Builder method.
        /// </summary>
        /// <param name="question">The target Question.</param>
        /// <returns></returns>
        public static CachedAnswer<TAnswer> For(IQuestion<TAnswer> question) =>
            new CachedAnswer<TAnswer>(question);

        /// <summary>
        /// Gets the answer to the question through the cache.
        /// If the answer is cached, this returns that value.
        /// If the answer is not cached, this calls the question, caches the answer, and returns the value.
        /// </summary>
        /// <param name="actor">The Actor.</param>
        /// <returns></returns>
        public TAnswer RequestAs(IActor actor) =>
            actor.Using<CacheAnswers>().Cache.Get(Question, actor);
    }
}
