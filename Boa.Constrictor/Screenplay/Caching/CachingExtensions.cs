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
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="actor">The Actor.</param>
        /// <param name="question">The Question.</param>
        /// <returns></returns>
        public static TAnswer GetsCached<TAnswer>(this IActor actor, IQuestion<TAnswer> question) =>
            actor.AsksFor(CachedAnswer<TAnswer>.For(question));

        /// <summary>
        /// A simplified extension method for caching answers.
        /// Calls will look like `Actor.Discovers(...)` instead of `Actor.AsksFor(CachedAnswer.For(...))`.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="actor">The Actor.</param>
        /// <param name="question">The Question.</param>
        /// <returns></returns>
        public static TAnswer Discovers<TAnswer>(this IActor actor, IQuestion<TAnswer> question) =>
            GetsCached(actor, question);
    }
}
