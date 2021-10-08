using Boa.Constrictor.Screenplay;
using System;
using System.Text.RegularExpressions;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Sometimes, the browser is already at the desired page, and it would be wasteful to reload.
    /// This Task will navigate the browser to a new URL only if the browser is not already acceptable.
    /// A regex for acceptable URLs is used because there could be more than one acceptable URL.
    /// By default, alerts are automatically accepted, but that can be overridden.
    /// </summary>
    public class NavigateIfNew : ITask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="acceptable">The regex for acceptable URLs. If null, use the target URL.</param>
        private NavigateIfNew(string url, Regex acceptable = null)
        {
            Url = url;
            Acceptable = acceptable ?? new Regex("^" + Regex.Escape(url) + "$", RegexOptions.IgnoreCase);
            AcceptAlerts = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The target URL.
        /// </summary>
        private string Url { get; set; }

        /// <summary>
        /// The regex for acceptable URLs.
        /// </summary>
        private Regex Acceptable { get; set; }

        /// <summary>
        /// If true, forcibly accept alerts.
        /// </summary>
        private bool AcceptAlerts { get; set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="ifNot">The regex for acceptable URLs. If null, use the target URL.</param>
        /// <returns></returns>
        public static NavigateIfNew ToUrl(string url, Regex ifNot = null) =>
            new NavigateIfNew(url, ifNot);

        /// <summary>
        /// Sets the Task to forcibly accept any alerts that appear.
        /// </summary>
        /// <param name="accept">Flag indicating if alerts should be accepted.</param>
        /// <returns></returns>
        public NavigateIfNew AndAcceptAlerts(bool accept = true)
        {
            AcceptAlerts = accept;
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Navigates the browser to the target URL if necessary.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        public void PerformAs(IActor actor)
        {
            string currentUrl = actor.AsksFor(CurrentUrl.FromBrowser());

            if (Acceptable.Match(currentUrl).Success)
            {
                actor.Logger.Info("The current URL is acceptable, so navigation will not be attempted");
            }
            else
            {
                actor.Logger.Info("The current URL is not acceptable, so navigation will be attempted");
                actor.AttemptsTo(Navigate.ToUrl(Url));

                if (AcceptAlerts)
                    actor.AttemptsTo(AcceptAlert.IfItExists());
            }
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is NavigateIfNew interaction &&
            Url == interaction.Url &&
            Acceptable.ToString() == interaction.Acceptable.ToString() &&
            AcceptAlerts == interaction.AcceptAlerts;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Url, Acceptable.ToString(), AcceptAlerts);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string message = $"navigate browser to '{Url}' if not '{Acceptable}'";

            if (AcceptAlerts)
                message += " and accept any alerts";

            return message;
        }

        #endregion
    }
}
