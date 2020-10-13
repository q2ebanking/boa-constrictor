using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Makes a direct JavaScript call and returns the result.
    /// The call may optionally take arguments.
    /// To use arguments in the script, refer to them using the "arguments" list.
    /// For example, "arguments[0]" would be the first argument.
    /// </summary>
    /// <typeparam name="TValue">The answer type.</typeparam>
    public class JavaScriptCall<TValue> : AbstractWebQuestion<TValue>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.></param>
        private JavaScriptCall(string script, params object[] args)
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
        /// <param name="args">Arguments for the JavaScript code to execute.></param>
        /// <returns></returns>
        public static JavaScriptCall<TValue> To(string script, params object[] args) =>
            new JavaScriptCall<TValue>(script, args);

        #endregion

        #region Methods

        /// <summary>
        /// Gets a web element's JavaScript textContent value.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override TValue RequestAs(IActor actor, IWebDriver driver) =>
            (TValue)((IJavaScriptExecutor)driver).ExecuteScript(Script, Args);

        /// <summary>
        /// Returns a description of the question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"JavaScript call to '{Script}'";

        #endregion
    }

    /// <summary>
    /// Static builder class to help readability of fluent calls for JavaScriptCall.
    /// </summary>
    public static class JavaScriptCall
    {
        /// <summary>
        /// Constructs a JavaScriptCall question.
        /// This variant allows "JavaScriptCall.Of" calls to avoid generic type specification.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.></param>
        /// <returns></returns>
        public static JavaScriptCall<object> To(string script, params object[] args) =>
            JavaScriptCall<object>.To(script, args);
    }
}
