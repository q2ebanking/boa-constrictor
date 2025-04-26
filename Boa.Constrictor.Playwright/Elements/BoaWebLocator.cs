using Boa.Constrictor.Selenium;
using Microsoft.Playwright;
using OpenQA.Selenium;
using System;

namespace Boa.Constrictor.Playwright
{
    /// <summary>
    /// Serves as both the IWebLocator for Selenium code, but also as the IPlaywrightLocator for Playwright code.
    /// </summary>
    public class BoaWebLocator : WebLocator, IPlaywrightLocator, IBoaWebLocator
    {
        /// <summary>
        /// Constructor to make a BoaWebLocator using the Selenium By.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="query"></param>
        public BoaWebLocator(string description, By query) : base(description, query) { }

        string IPlaywrightLocator.Description { get => Description; set => throw new NotImplementedException(); }

        private IPlaywrightLocator PlaywrightLocator { get; set; }

        /// <summary>
        /// Extension method to format the description of the weblocator for exceptions and logging
        /// </summary>
        /// <param name="description">Description for the query</param>
        /// <param name="query">By object to be used to query for a specific BoaWebLocator</param>
        /// <returns></returns>
        public static new BoaWebLocator L(string description, By query) => new BoaWebLocator(description, query);

        /// <summary>
        /// Extension method to format the description of the weblocator for exceptions and logging
        /// </summary>
        /// <param name="description">Description for the query</param>
        /// <param name="query">By object to be used to query for a specific BoaWebLocator</param>
        /// <returns></returns>
        public BoaWebLocator L(By query, string description = null)
        {
            if (string.IsNullOrEmpty(description))
                description = query.Mechanism;

            string formattedDescription = $"{description}:\n{query.Criteria}\n";
            return new BoaWebLocator(formattedDescription, query);
        }

        ILocator IPlaywrightLocator.FindIn(IPage page)
        {
            PlaywrightLocator = ConvertToPlaywrightLocator(page);
            return PlaywrightLocator.FindIn(page);
        }

        /// <summary>
        /// Converts the wrapped BoaWebLocator to a PlaywrightLocator
        /// </summary>
        private PlaywrightLocator ConvertToPlaywrightLocator(IPage page, IFrame frame = null)
        {
            string playwrightSelector = ConvertSeleniumByToPlaywrightSelector(Query);
            
            // Check if a frame was explicitly provided
            if (frame == null)
            {
                // Get the browser context from the page
                var browserContext = page.Context;
                
                // Try to get the associated ability from our registry
                var ability = BrowseTheWebWithPlaywright.GetForContext(browserContext);
                
                // If we found the ability and it has a current frame, use it
                if (ability != null && ability.CurrentFrame != null)
                {
                    frame = ability.CurrentFrame;
                }
                else
                {
                    // If no frame is found, use the main frame
                    frame = page.MainFrame;
                }
            }

            return new PlaywrightLocator(
                Description,
                selector => page.Frame(frame.Name).Locator(playwrightSelector)
            );
        }

        /// <summary>
        /// Converts a Selenium By locator to a Playwright selector string.
        /// </summary>
        /// <param name="by">The Selenium By locator.</param>
        /// <returns>A Playwright selector string.</returns>
        private static string ConvertSeleniumByToPlaywrightSelector(By by)
        {
            string mechanism = by.ToString().Split(':')[0].Split('[')[0].Trim();
            string criteria = by.Criteria;

            switch (mechanism.ToLower())
            {
                case "by.id":
                    return $"#{criteria}";
                case "by.classname":
                    return $".{criteria}";
                case "by.tagname":
                    return criteria;
                case "by.name":
                    return $"[name='{criteria}']";
                case "by.cssselector":
                    return criteria;
                case "by.xpath":
                    return $"xpath={criteria}";
                case "by.linktext":
                    return $"text={criteria}";
                case "by.partiallinktext":
                    return $"text='{criteria}'";
                default:
                    throw new NotSupportedException($"Unsupported Selenium locator type: {mechanism}");
            }
        }
    }
}