namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Makes a Question compatible with caching answers.
    /// WARNING: Do NOT cache answers to every Question.
    /// Only cache answers that you know will be fairly constant.
    /// </summary>
    /// <typeparam name="TAnswer"></typeparam>
    public interface ICacheableQuestion<TAnswer> : IQuestion<TAnswer>
    {
        /// <summary>
        /// Checks if this Question is equal to another Question.
        /// Questions should be "equal" if they are the same type and have the same property values.
        /// </summary>
        /// <param name="obj">The other Question object.</param>
        /// <returns></returns>
        bool Equals(object obj);

        /// <summary>
        /// Generates a unique hash code for this Question.
        /// The hash code should depend upon the type and the property values.
        /// </summary>
        /// <returns></returns>
        int GetHashCode();
    }
}
