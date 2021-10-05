namespace Boa.Constrictor.Screenplay
{
    /// <summary>
    /// This wraps a generic Question and Condition in a non-generic wrapper.
    /// </summary>
    /// <typeparam name="TAnswer"></typeparam>
    public class ConditionWrapper<TAnswer> : IConditionAdaptor
    {
        #region Properties

        /// <summary>
        /// The IQuestion.
        /// </summary>
        private readonly IQuestion<TAnswer> Question;

        /// <summary>
        /// The ICondition.
        /// </summary>
        private readonly ICondition<TAnswer> Condition;

        /// <summary>
        /// The Answer.
        /// </summary>
        protected TAnswer Answer { get; set; } = default;

        /// <summary>
        /// The boolean operator associated with the pair of question and condition.
        /// </summary>
        public Operators Operator { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the wrapper.
        /// </summary>
        /// <param name="question"></param>
        /// <param name="condition"></param>
        /// <param name="boolOp"></param>
        public ConditionWrapper(IQuestion<TAnswer> question, ICondition<TAnswer> condition, Operators boolOp = Operators.And)
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
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool Evaluate(IActor actor)
        {
            Answer = actor.AsksFor(Question);
            return Condition.Evaluate(Answer);
        }

        /// <summary>
        /// Return the most recent Answer.
        /// </summary>
        /// <returns></returns>
        public string GetAnswer()
        {
            return Answer?.ToString();
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
