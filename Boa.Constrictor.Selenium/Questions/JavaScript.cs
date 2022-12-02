using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Makes a direct JavaScript call in the browser and returns the result.
    /// The call may optionally take arguments.
    /// To use arguments in the script, refer to them using the "arguments" list.
    /// For example, "arguments[0]" would be the first argument.
    /// Use "Actor.Calls()" instead of "Actor.AsksFor()" for this Question.
    /// </summary>
    /// <typeparam name="TValue">The answer type.</typeparam>
    public class JavaScript<TValue> : AbstractWebLocatorQuestion<TValue>
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="locator">The Web element locator. (null for no locator)</param>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.</param>
        private JavaScript(IWebLocator locator, string script, params object[] args):
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
        /// Constructs the Question without a target element.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.</param>
        /// <returns></returns>
        public static JavaScript<TValue> OnPage(string script, params object[] args) =>
            new JavaScript<TValue>(null, script, args);

        /// <summary>
        /// Constructs the Question with a target element.
        /// </summary>
        /// <param name="locator">The Web element locator.</param>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.</param>
        /// <returns></returns>
        public static JavaScript<TValue> On(IWebLocator locator, string script, params object[] args) =>
            new JavaScript<TValue>(locator, script, args);

        #endregion

        #region Methods

        /// <summary>
        /// Gets a web element's JavaScript textContent value.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override TValue RequestAs(IActor actor, IWebDriver driver)
        {
            object[] newArgs = Args;

            // If a locator is given, get the element and make it the first argument
            if (Locator != null)
            {
                actor.WaitsUntil(Existence.Of(Locator), IsEqualTo.True());
                var e = driver.FindElement(Locator.Query);

                IList<object> tempList = Args.ToList();
                tempList.Insert(0, e);
                newArgs = tempList.ToArray();
            }

            // Log the script
            actor.Logger.Info("JavaScript code to execute:");
            actor.Logger.Info(Script);

            // Log the arguments
            if (newArgs != null && newArgs.Length > 0)
            {
                actor.Logger.Info("JavaScript code arguments:");
                foreach (var a in newArgs)
                    actor.Logger.Info(a.ToString());
            }

            // Execute the script
            object response = ((IJavaScriptExecutor)driver).ExecuteScript(Script, newArgs);

            // Return the type-casted result
            return (TValue)response;
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj)
        {
            bool same =
                obj is JavaScript<TValue> interaction &&
                EqualityComparer<IWebLocator>.Default.Equals(Locator, interaction.Locator) &&
                Script == interaction.Script &&
                Args.Length == interaction.Args.Length;

            if (same)
                for (int i = 0; i < Args.Length; i++)
                    same = same && Args[i].Equals(((JavaScript<TValue>)obj).Args[i]);

            return same;
        }

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 17;
            foreach (object item in Args)
                hash = HashCode.Combine(hash, item);

            hash = HashCode.Combine(GetType(), Locator, Script, hash);
            return hash;
        }

        /// <summary>
        /// Returns a description of the Question.
        /// The script and the arguments will be printed during execution.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "JavaScript in browser";

        #endregion
    }

    /// <summary>
    /// Static builder class to help readability of fluent calls for JavaScript interactions.
    /// </summary>
    public static class JavaScript
    {
        /// <summary>
        /// Constructs a JavaScript Question.
        /// This variant allows "JavaScript.InBrowser" calls to avoid generic type specification.
        /// </summary>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.></param>
        /// <returns></returns>
        public static JavaScript<object> OnPage(string script, params object[] args) =>
            JavaScript<object>.OnPage(script, args);

        /// <summary>
        /// Constructs a JavaScript Question.
        /// This variant allows "JavaScript.On" calls to avoid generic type specification.
        /// </summary>
        /// <param name="locator">The Web element locator.</param>
        /// <param name="script">The JavaScript code to execute.</param>
        /// <param name="args">Arguments for the JavaScript code to execute.</param>
        /// <returns></returns>
        public static JavaScript<object> On(IWebLocator locator, string script, params object[] args) =>
            JavaScript<object>.On(locator, script, args);
    }
}
