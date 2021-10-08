namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// This contains a generic Question and Condition and exposes
    /// the evaluation through the IConditionEvaluator interface.
    /// </summary>
    /// <typeparam name="TAnswer"></typeparam>
    public class ConditionEvaluator<TAnswer> : IConditionEvaluator
    {
        #region Properties

        /// <summary>
        /// The Answer to the Question
        /// </summary>
        public object Answer => _answer;

        /// <summary>
        /// The Condition.
        /// </summary>
        private readonly ICondition<TAnswer> Condition;

        /// <summary>
        /// The boolean operator associated with the pair of Question and Condition.
        /// </summary>
        public ConditionOperators Operator { get; }

        /// <summary>
        /// The Question.
        /// </summary>
        private readonly IQuestion<TAnswer> Question;

        private TAnswer _answer;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="question"></param>
        /// <param name="condition"></param>
        /// <param name="boolOp"></param>
        public ConditionEvaluator(
            IQuestion<TAnswer> question,
            ICondition<TAnswer> condition,
            ConditionOperators boolOp = ConditionOperators.And)
        {
            Question = question;
            Condition = condition;
            Operator = boolOp;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Evalutes the Condition against the Question's answer.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <returns></returns>
        public bool Evaluate(IActor actor)
        {
            _answer = actor.AsksFor(Question);
            return Condition.Evaluate(_answer);
        }

        /// <summary>
        /// Return the WaitingException caused by interaction.
        /// </summary>
        /// <param name="interaction"></param>
        /// <returns></returns>
        public WaitingException WaitingException(AbstractWait interaction)
        {
            return new WaitingException<TAnswer>(interaction, _answer);
        }

        /// <summary>
        /// Description of Question and Condition contents.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Question} {Condition}";
        }

        #endregion
    }
}
