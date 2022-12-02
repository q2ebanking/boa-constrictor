using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Scrolls a container to a specific top and/or left point using JavaScript.
    /// "Top" means the content of the container is moved up so lower content comes into view.
    /// "Left" means the content of the container is moved to the left so content to the right comes into view.
    /// Top scroll happens before left scroll.
    /// </summary>
    public class ScrollContainer : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static builder methods to construct.)
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="top">The target top scroll pixel value. If null, don't scroll.</param>
        /// <param name="left">The target left scroll pixel value. If null, don't scroll.</param>
        private ScrollContainer(IWebLocator locator, int? top = null, int? left = null) :
            base(locator)
        {
            Top = top;
            Left = left;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The target top scroll pixel value.
        /// If null, don't scroll.
        /// </summary>
        private int? Top { get; }

        /// <summary>
        /// The target left scroll pixel value.
        /// If null, don't scroll.
        /// </summary>
        private int? Left { get; }

        /// <summary>
        /// ToString adjective.
        /// </summary>
        protected override string ToStringAdjective => "to";

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object to scroll top and left.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="top">The target top scroll pixel value.</param>
        /// <param name="left">The target left scroll pixel value.</param>
        /// <returns></returns>
        public static ScrollContainer To(IWebLocator locator, int top, int left) =>
            new ScrollContainer(locator, top: top, left: left);

        /// <summary>
        /// Constructs the Task object to scroll top only.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="top">The target top scroll pixel value.</param>
        /// <returns></returns>
        public static ScrollContainer ToTop(IWebLocator locator, int top) =>
            new ScrollContainer(locator, top: top, left: null);

        /// <summary>
        /// Constructs the Task object to scroll left only.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="left">The target left scroll pixel value.</param>
        /// <returns></returns>
        public static ScrollContainer ToLeft(IWebLocator locator, int left) =>
            new ScrollContainer(locator, top: null, left: left);

        /// <summary>
        /// Constructs the Task object to scroll to the origin.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <returns></returns>
        public static ScrollContainer Reset(IWebLocator locator) =>
            new ScrollContainer(locator, top: 0, left: 0);

        #endregion

        #region Methods

        /// <summary>
        /// Scrolls directly to an element using JavaScript.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            if (Top != null)
                actor.Calls(JavaScript.On(Locator, $"arguments[0].scrollTop = {Top};"));
            if (Left != null)
                actor.Calls(JavaScript.On(Locator, $"arguments[0].scrollLeft = {Left};"));
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is ScrollContainer container &&
            EqualityComparer<IWebLocator>.Default.Equals(Locator, container.Locator) &&
            Top == container.Top &&
            Left == container.Left;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => 
            HashCode.Combine(base.GetHashCode(), Locator, $"Top {Top}", $"Left {Left}");

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string message = $"scroll container '{Locator.Description}' {ToStringAdjective} ";

            if (Top == null)
                message += $"left = {Left}";
            else if (Left == null)
                message += $"top = {Top}";
            else
                message += $"top = {Top}, left = {Left}";

            return message;
        }

        #endregion
    }
}
