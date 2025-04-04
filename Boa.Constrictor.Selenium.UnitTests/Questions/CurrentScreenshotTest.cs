using Boa.Constrictor.Selenium;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Concurrent;
using System.IO;

namespace Boa.Constrictor.Selenium.UnitTests
{
    public class CurrentScreenshotTest : BaseWebQuestionTest
    {
        #region Test Variables

        private static readonly string Dir = TestContext.CurrentContext.WorkDirectory;
        private readonly ConcurrentBag<string> ImagesToDelete = new();

        #endregion

        #region SetUp

        [SetUp]
        public void SetupScreenshot()
        {
            WebDriver.Setup(x => x.GetScreenshot()).Returns(new Screenshot(string.Empty));
        }

        #endregion

        #region TearDown

        [OneTimeTearDown]
        public void DeleteScreenshots()
        {
            foreach(var path in ImagesToDelete)
            {
                File.Delete(path);
            }
        }

        #endregion

        #region Tests

        [Test]
        public void TestSaveFileToDirNoName()
        {
            var image = Actor.AsksFor(CurrentScreenshot.SavedTo(Dir));
            ImagesToDelete.Add(image);
            image.Should().Match(Dir + Path.DirectorySeparatorChar + "Screenshot*.png");
            Logger.Messages.Should().ContainMatch("*Set the screenshot file name to 'Screenshot*");
            File.Exists(image).Should().BeTrue();
        }

        [Test]
        public void TestSaveFileToDirWithName()
        {
            var image = Actor.AsksFor(CurrentScreenshot.SavedTo(Dir, "webpage"));
            ImagesToDelete.Add(image);
            image.Should().Match(Dir + Path.DirectorySeparatorChar + "webpage*.png");
            Logger.Messages.Should().ContainMatch($"*Screenshots: {Dir}{Path.DirectorySeparatorChar}webpage*.png");
            File.Exists(image).Should().BeTrue();
        }

        [Test]
        public void TestSaveFileWithExtension()
        {
            var image = Actor.AsksFor(CurrentScreenshot.SavedTo(Dir, "webpage.png"));
            ImagesToDelete.Add(image);
            image.Should().Match(Dir + Path.DirectorySeparatorChar + "webpage*.png");
            Logger.Messages.Should().ContainMatch($"*Screenshots: {Dir}{Path.DirectorySeparatorChar}webpage*.png");
            Logger.Messages.Should().ContainMatch("*Screenshot file name 'webpage.png' should not be given an extension");
            Logger.Messages.Should().ContainMatch("*Removing the extension from the name");
            File.Exists(image).Should().BeTrue();
        }

        [Test]
        public void TestSaveFileWithJpegFormat()
        {
            var image = Actor.AsksFor(CurrentScreenshot.SavedTo(Dir, "webpage").UsingFormat(".jpeg"));
            ImagesToDelete.Add(image);
            image.Should().Match(Dir + Path.DirectorySeparatorChar + "webpage*.jpeg");
            Logger.Messages.Should().ContainMatch($"*Screenshots: {Dir}{Path.DirectorySeparatorChar}webpage*.jpeg");
            File.Exists(image).Should().BeTrue();
        }

        [Test]
        public void TestSaveFileWithBmpFormatNoName()
        {
            var image = Actor.AsksFor(CurrentScreenshot.SavedTo(Dir).UsingFormat(".bmp"));
            ImagesToDelete.Add(image);
            image.Should().Match(Dir + Path.DirectorySeparatorChar + "Screenshot*.bmp");
            Logger.Messages.Should().ContainMatch($"*Screenshots: {Dir}{Path.DirectorySeparatorChar}Screenshot*.bmp");
            File.Exists(image).Should().BeTrue();
        }

        #endregion
    }
}
