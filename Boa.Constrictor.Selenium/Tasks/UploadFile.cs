using Boa.Constrictor.Screenplay;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Waits for an file input element to appear and uploads a file to it.
    /// Internally calls Wait.
    /// </summary>
    public class UploadFile : AbstractWebLocatorTask
    {
        #region Constructors

        /// <summary>
        /// Private constructor.
        /// (Use static methods for public construction.)
        /// </summary>
        private UploadFile(IWebLocator locator, string filePath) :
            base(locator)
        {
            FilePath = filePath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The path of the file to upload.
        /// </summary>
        public string FilePath { get; private set; }

        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Task object.
        /// </summary>
        /// <param name="locator">The target Web element's locator.</param>
        /// <param name="filePath">The path of the file to upload.</param>
        /// <returns></returns>
        public static UploadFile Through(IWebLocator locator, string filePath) => new UploadFile(locator, filePath);

        #endregion

        #region Methods

        /// <summary>
        /// Waits for an file input element to appear and uploads a file to it.
        /// Internally calls Wait.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        public override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.AttemptsTo(Wait.Until(Existence.Of(Locator), IsEqualTo.True()));
            var element = driver.FindElement(Locator.Query);
            element.SendKeys(FilePath);
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is UploadFile uploadFile &&
            EqualityComparer<IWebLocator>.Default.Equals(Locator, uploadFile.Locator) &&
            FilePath == uploadFile.FilePath;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), Locator, FilePath);

        /// <summary>
        /// Returns a description of the Task.
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"Upload a file using {Locator.Description}";

        #endregion
    }
}
