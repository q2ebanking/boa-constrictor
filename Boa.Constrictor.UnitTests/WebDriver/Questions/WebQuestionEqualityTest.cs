using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    [TestFixture]
    public class WebQuestionEqualityTest
    {
        #region Interactions

        private static readonly IWebLocator LocatorA = L("hello", By.Id("moto"));
        private static readonly IWebLocator LocatorA1 = L("hello", By.Id("moto"));
        private static readonly IWebLocator LocatorB = L("hello", By.Id("goodbye"));

        private static readonly object[] SameInteractions =
        {
            // The interaction object must be duplicated to prove that two separate instances may be equal to each other.

            new object[] { AlertPresence.InBrowser(), AlertPresence.InBrowser() },
            new object[] { Appearance.Of(LocatorA), Appearance.Of(LocatorA1) },
            new object[] { BrowserCookie.Named("peanut"), BrowserCookie.Named("peanut") },
            new object[] { BrowserCookieExistence.Named("peanut"), BrowserCookieExistence.Named("peanut") },
            new object[] { Classes.Of(LocatorA), Classes.Of(LocatorA1) },
            new object[] { Count.Of(LocatorA), Count.Of(LocatorA1) },
            new object[] { CssValue.Of(LocatorA, "property"), CssValue.Of(LocatorA1, "property") },
            new object[] { CssValueList.For(LocatorA, "property"), CssValueList.For(LocatorA1, "property") },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot"), CurrentScreenshot.SavedTo("/path/to/screenshot") },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot", "file"), CurrentScreenshot.SavedTo("/path/to/screenshot", "file") },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot", "file").UsingFormat(ScreenshotImageFormat.Png), CurrentScreenshot.SavedTo("/path/to/screenshot", "file").UsingFormat(ScreenshotImageFormat.Png) },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot").UsingFormat(ScreenshotImageFormat.Png), CurrentScreenshot.SavedTo("/path/to/screenshot").UsingFormat(ScreenshotImageFormat.Png) },
            new object[] { CurrentUrl.FromBrowser(), CurrentUrl.FromBrowser() },
            new object[] { EnabledState.Of(LocatorA), EnabledState.Of(LocatorA1) },
            new object[] { Existence.Of(LocatorA), Existence.Of(LocatorA1) },
            new object[] { HtmlAttribute.Of(LocatorA, "property"), HtmlAttribute.Of(LocatorA1, "property") },
            new object[] { HtmlAttributeList.For(LocatorA, "property"), HtmlAttributeList.For(LocatorA1, "property") },
            new object[] { JavaScript.OnPage("script"), JavaScript.OnPage("script") },
            new object[] { JavaScript.OnPage("script", "a", "b"), JavaScript.OnPage("script", "a", "b") },
            new object[] { JavaScript.On(LocatorA, "script"), JavaScript.On(LocatorA, "script") },
            new object[] { JavaScript.On(LocatorA, "script", "a", "b"), JavaScript.On(LocatorA, "script", "a", "b") },
            new object[] { JavaScriptProperty.Of(LocatorA, "property"), JavaScriptProperty.Of(LocatorA1, "property") },
            new object[] { JavaScriptText.Of(LocatorA), JavaScriptText.Of(LocatorA1) },
            new object[] { Location.Of(LocatorA), Location.Of(LocatorA1) },
            new object[] { PixelSize.Of(LocatorA), PixelSize.Of(LocatorA1) },
            new object[] { SelectedOptionText.Of(LocatorA), SelectedOptionText.Of(LocatorA1) },
            new object[] { SelectedState.Of(LocatorA), SelectedState.Of(LocatorA1) },
            new object[] { SelectOptionsAvailable.For(LocatorA), SelectOptionsAvailable.For(LocatorA1) },
            new object[] { SystemNetCookie.Named("peanut"), SystemNetCookie.Named("peanut") },
            new object[] { SystemNetCookie.Named("peanut").AndResetExpirationTo(new DateTime(2020, 1, 1)), SystemNetCookie.Named("peanut").AndResetExpirationTo(new DateTime(2020, 1, 1)) },
            new object[] { TagName.Of(LocatorA), TagName.Of(LocatorA1) },
            new object[] { Text.Of(LocatorA), Text.Of(LocatorA1) },
            new object[] { TextList.For(LocatorA), TextList.For(LocatorA1) },
            new object[] { Title.OfPage(), Title.OfPage() },
            new object[] { WindowHandle.At(1), WindowHandle.At(1) },
        };

        private static readonly object[] DifferentInteractions =
        {
            new object[] { AlertPresence.InBrowser(), Title.OfPage() },
            new object[] { Appearance.Of(LocatorA), Title.OfPage() },
            new object[] { Appearance.Of(LocatorA), Appearance.Of(LocatorB) },
            new object[] { BrowserCookie.Named("peanut"), Title.OfPage() },
            new object[] { BrowserCookie.Named("peanut"), BrowserCookie.Named("snickerdoodle") },
            new object[] { BrowserCookieExistence.Named("peanut"), Title.OfPage() },
            new object[] { BrowserCookieExistence.Named("peanut"), BrowserCookieExistence.Named("snickerdoodle") },
            new object[] { Classes.Of(LocatorA), Title.OfPage() },
            new object[] { Classes.Of(LocatorA), Classes.Of(LocatorB) },
            new object[] { Count.Of(LocatorA), Title.OfPage() },
            new object[] { Count.Of(LocatorA), Count.Of(LocatorB) },
            new object[] { CssValue.Of(LocatorA, "property"), Title.OfPage() },
            new object[] { CssValue.Of(LocatorA, "property"), CssValue.Of(LocatorB, "property") },
            new object[] { CssValue.Of(LocatorA, "property"), CssValue.Of(LocatorA1, "attribute") },
            new object[] { CssValueList.For(LocatorA, "property"), Title.OfPage() },
            new object[] { CssValueList.For(LocatorA, "property"), CssValueList.For(LocatorB, "property") },
            new object[] { CssValueList.For(LocatorA, "property"), CssValueList.For(LocatorA1, "attribute") },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot"), Title.OfPage() },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot"), CurrentScreenshot.SavedTo("/path/to/screenshot2") },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot", "file"), CurrentScreenshot.SavedTo("/path/to/screenshot") },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot", "file"), CurrentScreenshot.SavedTo("/path/to/screenshot", "file2") },
            new object[] { CurrentScreenshot.SavedTo("/path/to/screenshot").UsingFormat(ScreenshotImageFormat.Png), CurrentScreenshot.SavedTo("/path/to/screenshot").UsingFormat(ScreenshotImageFormat.Jpeg) },
            new object[] { CurrentUrl.FromBrowser(), Title.OfPage() },
            new object[] { EnabledState.Of(LocatorA), Title.OfPage() },
            new object[] { EnabledState.Of(LocatorA), EnabledState.Of(LocatorB) },
            new object[] { Existence.Of(LocatorA), Title.OfPage() },
            new object[] { Existence.Of(LocatorA), Existence.Of(LocatorB) },
            new object[] { HtmlAttribute.Of(LocatorA, "property"), Title.OfPage() },
            new object[] { HtmlAttribute.Of(LocatorA, "property"), HtmlAttribute.Of(LocatorB, "property") },
            new object[] { HtmlAttribute.Of(LocatorA, "property"), HtmlAttribute.Of(LocatorA1, "attribute") },
            new object[] { HtmlAttributeList.For(LocatorA, "property"), Title.OfPage() },
            new object[] { HtmlAttributeList.For(LocatorA, "property"), HtmlAttributeList.For(LocatorB, "property") },
            new object[] { HtmlAttributeList.For(LocatorA, "property"), HtmlAttributeList.For(LocatorA1, "attribute") },
            new object[] { JavaScript.OnPage("script"), Title.OfPage() },
            new object[] { JavaScript.OnPage("script"), JavaScript.OnPage("nonscript") },
            new object[] { JavaScript.OnPage("script"), JavaScript.OnPage("script", "a", "b") },
            new object[] { JavaScript.OnPage("script", "a"), JavaScript.OnPage("script", "a", "b") },
            new object[] { JavaScript.OnPage("script", "a", "c"), JavaScript.OnPage("script", "a", "b") },
            new object[] { JavaScript.On(LocatorA, "script"), JavaScript.On(LocatorB, "script") },
            new object[] { JavaScript.On(LocatorA, "script"), JavaScript.On(LocatorA, "nonscript") },
            new object[] { JavaScript.On(LocatorA, "script", "a"), JavaScript.On(LocatorA, "script", "a", "b") },
            new object[] { JavaScript.On(LocatorA, "script", "a", "c"), JavaScript.On(LocatorA, "script", "a", "b") },
            new object[] { JavaScriptProperty.Of(LocatorA, "property"), Title.OfPage() },
            new object[] { JavaScriptProperty.Of(LocatorA, "property"), JavaScriptProperty.Of(LocatorB, "property") },
            new object[] { JavaScriptProperty.Of(LocatorA, "property"), JavaScriptProperty.Of(LocatorA1, "attribute") },
            new object[] { JavaScriptText.Of(LocatorA), Title.OfPage() },
            new object[] { JavaScriptText.Of(LocatorA), JavaScriptText.Of(LocatorB) },
            new object[] { Location.Of(LocatorA), Title.OfPage() },
            new object[] { Location.Of(LocatorA), Location.Of(LocatorB) },
            new object[] { PixelSize.Of(LocatorA), Title.OfPage() },
            new object[] { PixelSize.Of(LocatorA), PixelSize.Of(LocatorB) },
            new object[] { SelectedOptionText.Of(LocatorA), Title.OfPage() },
            new object[] { SelectedOptionText.Of(LocatorA), SelectedOptionText.Of(LocatorB) },
            new object[] { SelectedState.Of(LocatorA), Title.OfPage() },
            new object[] { SelectedState.Of(LocatorA), SelectedState.Of(LocatorB) },
            new object[] { SelectOptionsAvailable.For(LocatorA), Title.OfPage() },
            new object[] { SelectOptionsAvailable.For(LocatorA), SelectOptionsAvailable.For(LocatorB) },
            new object[] { SystemNetCookie.Named("peanut"), Title.OfPage() },
            new object[] { SystemNetCookie.Named("peanut"), SystemNetCookie.Named("snickerdoodle") },
            new object[] { SystemNetCookie.Named("peanut"), SystemNetCookie.Named("peanut").AndResetExpirationTo(DateTime.UtcNow) },
            new object[] { SystemNetCookie.Named("peanut").AndResetExpirationTo(DateTime.UtcNow), SystemNetCookie.Named("peanut").AndResetFutureExpirationTo(new TimeSpan(1, 1, 1)) },
            new object[] { TagName.Of(LocatorA), Title.OfPage() },
            new object[] { TagName.Of(LocatorA), TagName.Of(LocatorB) },
            new object[] { Text.Of(LocatorA), Title.OfPage() },
            new object[] { Text.Of(LocatorA), Text.Of(LocatorB) },
            new object[] { TextList.For(LocatorA), Title.OfPage() },
            new object[] { TextList.For(LocatorA), TextList.For(LocatorB) },
            new object[] { Title.OfPage(), AlertPresence.InBrowser() },
            new object[] { WindowHandle.At(1), Title.OfPage() },
            new object[] { WindowHandle.At(1), WindowHandle.At(2) },
        };

        #endregion

        #region Tests

        [TestCaseSource(nameof(SameInteractions))]
        public void Equals_True(IInteraction a, IInteraction b)
        {
            a.Equals(b).Should().BeTrue();
        }

        [TestCaseSource(nameof(DifferentInteractions))]
        public void Equals_False(IInteraction a, IInteraction b)
        {
            a.Equals(b).Should().BeFalse();
        }

        [TestCaseSource(nameof(SameInteractions))]
        public void GetHashCode_Same(IInteraction a, IInteraction b)
        {
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().Be(codeB);
        }

        [TestCaseSource(nameof(DifferentInteractions))]
        public void GetHashCode_Different(IInteraction a, IInteraction b)
        {
            int codeA = a.GetHashCode();
            int codeB = b.GetHashCode();
            codeA.Should().NotBe(codeB);
        }

        #endregion
    }
}
