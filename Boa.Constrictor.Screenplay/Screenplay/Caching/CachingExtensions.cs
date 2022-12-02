namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Extension methods for caching answers.
    /// </summary>
    public static class CachingExtensions
    {
        /// <summary>
        /// A simplified extension method for caching answers.
        /// Calls will look like `Actor.GetsCached(...)` instead of `Actor.AsksFor(CachedAnswer.For(...))`.
        /// WARNING: Do NOT cache answers to every Question.
        /// Only cache answers that you know will be fairly constant.
        /// Use this extension method only when explicitly caching answers.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="question">The Question.</param>
        /// <returns></returns>
        public static TAnswer GetsCached<TAnswer>(this IActor actor, ICacheableQuestion<TAnswer> question) =>
            actor.AsksFor(CachedAnswer<TAnswer>.For(question));

        /// <summary>
        /// A simplified extension method for caching answers.
        /// Calls will look like `Actor.Discovers(...)` instead of `Actor.AsksFor(CachedAnswer.For(...))`.
        /// WARNING: Do NOT cache answers to every Question.
        /// Only cache answers that you know will be fairly constant.
        /// Use this extension method only when explicitly caching answers.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="question">The Question.</param>
        /// <returns></returns>
        public static TAnswer Discovers<TAnswer>(this IActor actor, ICacheableQuestion<TAnswer> question) =>
            GetsCached(actor, question);
    }
}
