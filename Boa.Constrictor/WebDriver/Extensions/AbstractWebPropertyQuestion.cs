namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Abstract class for any Web questions that use a Web element locator and a property name.
    /// </summary>
    public abstract class AbstractWebPropertyQuestion<TAnswer> : AbstractWebLocatorQuestion<TAnswer>
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="propertyName">The name of the property to access.</param>
        public AbstractWebPropertyQuestion(IWebLocator locator, string propertyName) : base(locator) =>
            PropertyName = propertyName;

        #endregion

        #region Properties

        /// <summary>
        /// The adjective to use for the Locator in the ToString method.
        /// </summary>
        protected override string ToStringAdjective => "for";

        /// <summary>
        /// The name of the property to access.
        /// </summary>
        public string PropertyName { get; }

        #endregion

        #region Methods
        
        /// <summary>
        /// Returns a description of the property question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"{GetType()} of '{PropertyName}' {ToStringAdjective} '{Locator.Description}'";

        #endregion
    }
}
