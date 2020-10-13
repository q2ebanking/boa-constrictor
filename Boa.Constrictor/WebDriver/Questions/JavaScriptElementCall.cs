using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System.Linq;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Makes a direct call to a JavaScript element and returns the result.
    /// The call may optionally take arguments.
    /// To use arguments in the script, refer to them using the "arguments" list.
    /// For example, "arguments[0]" would be the element object.
    /// Indexes 1+ would be the arguments.
    /// </summary>
    /// <typeparam name="TValue">The answer type.</typeparam>
    public class JavaScriptElementCall<TValue> : AbstractWebLocatorQuestion<TValue>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="locator">The Web element locator.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.></param>
        private JavaScriptElementCall(string script, IWebLocator locator, params object[] args) :
            base(locator)
        {
            Script = script;
            Args = args;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The JavaScript code to execute.
        /// </summary>
        public string Script { get; private set; }

        /// <summary>
        /// Arguments for the JavaScript code to execute.
        /// </summary>
        public object[] Args { get; private set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the question.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="locator">The Web element locator.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.></param>
        /// <returns></returns>
        public static JavaScriptElementCall<TValue> To(string script, IWebLocator locator, params object[] args) =>
            new JavaScriptElementCall<TValue>(script, locator, args);

        #endregion

        #region Methods

        /// <summary>
        /// Makes a direct call to a JavaScript element and returns the result.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override TValue RequestAs(IActor actor, IWebDriver driver)
        {
            actor.AttemptsTo(Wait.Until(Existence.Of(Locator), IsEqualTo.True()));

            var e = driver.FindElement(Locator.Query);
            var newList = Args.ToList();
            newList.Insert(0, e);

            return (TValue)((IJavaScriptExecutor)driver).ExecuteScript(Script, newList.ToArray());
        }

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"JavaScript call to '{Script}' using {Locator.Description}";

        #endregion
    }

    /// <summary>
    /// Static builder class to help readability of fluent calls for JavaScriptElementCall.
    /// </summary>
    public static class JavaScriptElementCall
    {
        /// <summary>
        /// Constructs a JavaScriptElementCall question.
        /// This variant allows "JavaScriptCall.Of" calls to avoid generic type specification.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="locator">The Web element locator.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.></param>
        /// <returns></returns>
        public static JavaScriptElementCall<object> To(string script, IWebLocator locator, params object[] args) =>
            JavaScriptElementCall<object>.To(script, locator, args);
    }
}
