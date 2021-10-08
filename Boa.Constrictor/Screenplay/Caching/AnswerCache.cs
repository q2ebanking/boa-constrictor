using System.Collections.Generic;

namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Caches answers to Questions.
    /// Questions must implement `ICacheableQuestion` to be compatible with caching.
    /// WARNING: Do NOT cache answers to every Question.
    /// Only cache answers that you know will be fairly constant.
    /// </summary>
    public class AnswerCache
    {
        #region Instance Variables

        /// <summary>
        /// Lock object for safe multi-threading.
        /// </summary>
        protected readonly object Lock = new object();

        /// <summary>
        /// The answer cache.
        /// Maps Question objects to answer objects.
        /// </summary>
        protected IDictionary<IInteraction, object> Cache { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// Initializes the cache to be empty.
        /// </summary>
        public AnswerCache()
        {
            Cache = new Dictionary<IInteraction, object>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an answer from the cache.
        /// If the cache does not have the answer, it asks the Question using the given Actor.
        /// Uses the lock to be thread-safe.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question.</param>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public TAnswer Get<TAnswer>(ICacheableQuestion<TAnswer> question, IActor actor)
        {
            lock (Lock)
            {
                if (!Cache.ContainsKey(question))
                {
                    TAnswer answer = actor.AsksFor(question);
                    Cache.Add(question, answer);
                }

                return (TAnswer)Cache[question];
            }
        }

        /// <summary>
        /// Checks if the cache contains an answer for the given Question.
        /// Uses the lock to be thread-safe.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question.</param>
        /// <returns></returns>
        public bool Has<TAnswer>(ICacheableQuestion<TAnswer> question)
        {
            lock (Lock)
            {
                return Cache.ContainsKey(question);
            }
        }

        /// <summary>
        /// Invalidates a cached answer for a Question by removing it from the cache.
        /// Uses the lock to be thread-safe.
        /// </summary>
        /// <typeparam name="TAnswer">The answer type.</typeparam>
        /// <param name="question">The Question.</param>
        public void Invalidate<TAnswer>(ICacheableQuestion<TAnswer> question)
        {
            lock (Lock)
            {
                Cache.Remove(question);
            }
        }

        /// <summary>
        /// Invalidates all answers in the cache by clearing it completely.
        /// Uses the lock to be thread-safe.
        /// </summary>
        public void InvalidateAll()
        {
            lock (Lock)
            {
                Cache.Clear();
            }
        }

        #endregion
    }
}
