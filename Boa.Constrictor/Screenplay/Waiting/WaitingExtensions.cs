namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// Provides IActor extension methods to simplify waiting syntax.
    /// 
    /// </summary>
    public static class WaitingExtensions
    {
        /// <summary>
        /// A simplified extension method for waiting.
        /// Calls will look like `Actor.WaitsUntil(...)` instead of `Actor.AsksFor(ValueAfterWaiting.Until(...))`.
        /// </summary>
        /// <typeparam name="TAnswer">The type of the Question's answer value.</typeparam>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="question">The Question upon whose answer to wait.</param>
        /// <param name="condition">The expected condition for which to wait.</param>
        /// <param name="timeout">The timeout override in seconds. If null, use the standard timeout value.</param>
        /// <param name="additional">An additional amount to add to the timeout. Defaults to 0.</param>
        /// <returns></returns>
        public static TAnswer WaitsUntil<TAnswer>(
            this IActor actor,
            IQuestion<TAnswer> question,
            ICondition<TAnswer> condition,
            int? timeout = null,
            int additional = 0) =>

            actor.AsksFor(ValueAfterWaiting.Until(question, condition).ForUpTo(timeout).ForAnAdditional(additional));
    }
}
