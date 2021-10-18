using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Utilities;
using OpenQA.Selenium;
using System;
using System.IO;

namespace Boa.Constrictor.WebDriver
{
    /// <summary>
    /// Captures a screenshot of the current Web browser and return its path.
    /// </summary>
    public class CurrentScreenshot : AbstractWebQuestion<string>
    {
        #region Constants

        /// <summary>
        /// The default screenshot image format.
        /// </summary>
        public const ScreenshotImageFormat DefaultImageFormat = ScreenshotImageFormat.Png;

        #endregion

        #region Constructors

        /// <summary>
        /// Private constructor.
        /// Uses default values for format and prefix.
        /// (Use static methods for public construction.)
        /// </summary>
        /// <param name="outputDir">The path to the output directory where screenshot image files will be saved.</param>
        /// <param name="fileName">The file name (without the extension).</param>
        private CurrentScreenshot(string outputDir, string fileName)
        {
            OutputDir = outputDir;
            FileName = fileName;
            Format = DefaultImageFormat;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The file name for the screenshot image.
        /// Do NOT include the directory or the extension!
        /// </summary>
        private string FileName { get; set; }

        /// <summary>
        /// Image file format.
        /// </summary>
        private ScreenshotImageFormat Format { get; set; }

        /// <summary>
        /// The path to the output directory where screenshot image files will be saved.
        /// </summary>
        private string OutputDir { get; set; }
        
        #endregion

        #region Builder Methods

        /// <summary>
        /// Constructs the Question object.
        /// </summary>
        /// <param name="outputDir">The output directory.</param>
        /// <param name="fileName">The file name (without the extension).</param>
        /// <returns></returns>
        public static CurrentScreenshot SavedTo(string outputDir, string fileName = null) =>
            new CurrentScreenshot(outputDir, fileName);
        
        /// <summary>
        /// Changes the image format.
        /// </summary>
        /// <param name="format">Image file format.</param>
        /// <returns></returns>
        public CurrentScreenshot UsingFormat(ScreenshotImageFormat format)
        {
            Format = format;
            return this;
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Captures a screenshot and saves it to an image file in the <i>OutputDir</i> directory.
        /// Creates the directory if it does not already exist.
        /// Returns the path to the screenshot file.
        /// The file name will include a timestamp and the thread name if not explicitly provided.
        /// </summary>
        /// <param name="actor">The Screenplay Actor.</param>
        /// <param name="driver">The WebDriver.</param>
        /// <returns></returns>
        public override string RequestAs(IActor actor, IWebDriver driver)
        {
            string fileName = FileName;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                // Use the default name with a timestamp.
                fileName = Names.ConcatUniqueName("Screenshot");
                actor.Logger.Info($"Set the screenshot file name to '{fileName}'");
            }
            else if (Path.GetExtension(fileName) != "")
            {
                // Remove any given filename extension.
                actor.Logger.Warning($"Screenshot file name '{fileName}' should not be given an extension");
                actor.Logger.Warning("Removing the extension from the name");
                fileName = Path.GetFileNameWithoutExtension(fileName);
            }
            
            if (!Directory.Exists(OutputDir))
            {
                // Create the output directory if it doesn't exist.
                actor.Logger.Debug($"Creating screenshot directory '{OutputDir}'");
                Directory.CreateDirectory(OutputDir);
            }

            // Capture and save the screenshot.
            string path = Path.Combine(OutputDir, $"{fileName}.{Format.ToString().ToLower()}");
            (driver as ITakesScreenshot).GetScreenshot().SaveAsFile(path, Format);
            actor.Logger.LogArtifact(ArtifactTypes.Screenshots, path);

            // Return the path to the screenshot image file.
            return path;
        }

        /// <summary>
        /// Checks if this interaction is equal to another interaction.
        /// </summary>
        /// <param name="obj">The other object.</param>
        public override bool Equals(object obj) =>
            obj is CurrentScreenshot screenshot &&
            FileName == screenshot.FileName &&
            Format == screenshot.Format &&
            OutputDir == screenshot.OutputDir;

        /// <summary>
        /// Gets a unique hash code for this interaction.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() =>
            HashCode.Combine(GetType(), FileName, Format, OutputDir);

        /// <summary>
        /// Returns a description of the Question.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => "current browser screenshot";

        #endregion
    }
}
