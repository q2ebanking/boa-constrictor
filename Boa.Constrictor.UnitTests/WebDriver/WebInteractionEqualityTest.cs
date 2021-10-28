using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Boa.Constrictor.UnitTests.WebDriver
{
    [TestFixture]
    public class WebInteractionEqualityTest
    {
        #region Interactions

        private static readonly IWebLocator LocatorA = L("hello", By.Id("moto"));
        private static readonly IWebLocator LocatorA1 = L("hello", By.Id("moto"));
        private static readonly IWebLocator LocatorB = L("hello", By.Id("goodbye"));

        private static readonly object[] SameInteractions =
        {
            // The interaction object must be duplicated to prove that two separate instances may be equal to each other.

            // Questions

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
            new object[] { DomProperty.Of(LocatorA, "property"), DomProperty.Of(LocatorA1, "property") },
            new object[] { EnabledState.Of(LocatorA), EnabledState.Of(LocatorA1) },
            new object[] { Existence.Of(LocatorA), Existence.Of(LocatorA1) },
            new object[] { HtmlAttribute.Of(LocatorA, "property"), HtmlAttribute.Of(LocatorA1, "property") },
            new object[] { HtmlAttributeList.For(LocatorA, "property"), HtmlAttributeList.For(LocatorA1, "property") },
            new object[] { JavaScript.OnPage("script"), JavaScript.OnPage("script") },
            new object[] { JavaScript.OnPage("script", "a", "b"), JavaScript.OnPage("script", "a", "b") },
            new object[] { JavaScript.On(LocatorA, "script"), JavaScript.On(LocatorA, "script") },
            new object[] { JavaScript.On(LocatorA, "script", "a", "b"), JavaScript.On(LocatorA, "script", "a", "b") },
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

            // Tasks
            
            new object[] { AcceptAlert.IfItExists(), AcceptAlert.IfItExists() },
            new object[] { AcceptAlert.ThatMustExist(), AcceptAlert.ThatMustExist() },
            new object[] { AddBrowserCookie.Named("cookie", "chips"), AddBrowserCookie.Named("cookie", "chips") },
            new object[] { Clear.On(LocatorA), Clear.On(LocatorA1) },
            new object[] { Click.On(LocatorA), Click.On(LocatorA1) },
            new object[] { Hover.Over(LocatorA), Hover.Over(LocatorA1) },
            new object[] { JavaScriptClick.On(LocatorA), JavaScriptClick.On(LocatorA1) },
            new object[] { MaximizeWindow.ForBrowser(), MaximizeWindow.ForBrowser() },
            new object[] { Navigate.ToUrl("https://wwww.google.com/"), Navigate.ToUrl("https://wwww.google.com/") },
            new object[] { NavigateIfNew.ToUrl("https://wwww.google.com/"), NavigateIfNew.ToUrl("https://wwww.google.com/") },
            new object[] { NavigateIfNew.ToUrl("https://wwww.google.com/", ifNot: new Regex(@"google")), NavigateIfNew.ToUrl("https://wwww.google.com/", ifNot: new Regex(@"google")) },
            new object[] { NavigateIfNew.ToUrl("https://wwww.google.com/").AndAcceptAlerts(false), NavigateIfNew.ToUrl("https://wwww.google.com/").AndAcceptAlerts(false) },
            new object[] { QuitWebDriver.ForBrowser(), QuitWebDriver.ForBrowser() },
            new object[] { Refresh.Browser(), Refresh.Browser() },
            new object[] { ScrollContainer.Reset(LocatorA), ScrollContainer.Reset(LocatorA1) },
            new object[] { ScrollContainer.ToTop(LocatorA, 100), ScrollContainer.ToTop(LocatorA1, 100) },
            new object[] { ScrollContainer.ToLeft(LocatorA, 100), ScrollContainer.ToLeft(LocatorA1, 100) },
            new object[] { ScrollContainer.To(LocatorA, 100, 200), ScrollContainer.To(LocatorA1, 100, 200) },
            new object[] { ScrollToElement.At(LocatorA), ScrollToElement.At(LocatorA1) },
            new object[] { ScrollToElement.At(LocatorA, alignToTop: false), ScrollToElement.At(LocatorA1, alignToTop: false) },
            new object[] { Select.ByIndex(LocatorA, 0), Select.ByIndex(LocatorA1, 0) },
            new object[] { Select.ByText(LocatorA, "this"), Select.ByText(LocatorA1, "this") },
            new object[] { Select.ByText(LocatorA, "this", partialMatch: true), Select.ByText(LocatorA1, "this", partialMatch: true) },
            new object[] { Select.ByValue(LocatorA, "this"), Select.ByValue(LocatorA1, "this") },
            new object[] { SendKeys.To(LocatorA, "hello"), SendKeys.To(LocatorA1, "hello") },
            new object[] { SendKeys.To(LocatorA, "hello").Privately(), SendKeys.To(LocatorA1, "hello").Privately() },
            new object[] { SendKeys.To(LocatorA, "hello").Privately(), SendKeys.To(LocatorA1, "hello").Privately() },
            new object[] { SendKeys.To(LocatorA, "hello").ThenClick(LocatorB), SendKeys.To(LocatorA1, "hello").ThenClick(LocatorB) },
            new object[] { SendKeys.To(LocatorA, "hello").ThenHitEnter(), SendKeys.To(LocatorA1, "hello").ThenHitEnter() },
            new object[] { SendKeys.To(LocatorA, "hello").UsingClearMethod(), SendKeys.To(LocatorA1, "hello").UsingClearMethod() },
            new object[] { SendKeys.To(LocatorA, "hello").WithoutClearing(), SendKeys.To(LocatorA1, "hello").WithoutClearing() },
            new object[] { Submit.On(LocatorA), Submit.On(LocatorA1) },
            new object[] { SwitchWindow.To("window3"), SwitchWindow.To("window3") },
            new object[] { SwitchWindowToLatest.InBrowser(), SwitchWindowToLatest.InBrowser() },
            new object[] { WaitAndRefresh.For(LocatorA), WaitAndRefresh.For(LocatorA1) },
            new object[] { WaitAndRefresh.For(LocatorA).ForUpTo(10), WaitAndRefresh.For(LocatorA1).ForUpTo(10) },
            new object[] { WaitAndRefresh.For(LocatorA).ForAnAdditional(10), WaitAndRefresh.For(LocatorA1).ForAnAdditional(10) },
            new object[] { WaitAndRefresh.For(LocatorA).RefreshWaiting(10), WaitAndRefresh.For(LocatorA1).RefreshWaiting(10) },
        };

        private static readonly object[] DifferentInteractions =
        {
            // Questions

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
            new object[] { DomProperty.Of(LocatorA, "property"), Title.OfPage() },
            new object[] { DomProperty.Of(LocatorA, "property"), DomProperty.Of(LocatorB, "property") },
            new object[] { DomProperty.Of(LocatorA, "property"), DomProperty.Of(LocatorA1, "attribute") },
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

            // Tasks

            new object[] { AcceptAlert.IfItExists(), QuitWebDriver.ForBrowser() },
            new object[] { AcceptAlert.IfItExists(), AcceptAlert.ThatMustExist() },
            new object[] { AddBrowserCookie.Named("cookie", "chips"), QuitWebDriver.ForBrowser() },
            new object[] { AddBrowserCookie.Named("cookie", "chips"), AddBrowserCookie.Named("nomnom", "chips") },
            new object[] { Clear.On(LocatorA), QuitWebDriver.ForBrowser() },
            new object[] { Clear.On(LocatorA), Clear.On(LocatorB) },
            new object[] { Click.On(LocatorA), QuitWebDriver.ForBrowser() },
            new object[] { Click.On(LocatorA), Click.On(LocatorB) },
            new object[] { Hover.Over(LocatorA), QuitWebDriver.ForBrowser() },
            new object[] { Hover.Over(LocatorA), Hover.Over(LocatorB) },
            new object[] { JavaScriptClick.On(LocatorA), QuitWebDriver.ForBrowser() },
            new object[] { JavaScriptClick.On(LocatorA), JavaScriptClick.On(LocatorB) },
            new object[] { MaximizeWindow.ForBrowser(), QuitWebDriver.ForBrowser() },
            new object[] { Navigate.ToUrl("https://wwww.google.com/"), QuitWebDriver.ForBrowser() },
            new object[] { Navigate.ToUrl("https://wwww.google.com/"), Navigate.ToUrl("https://wwww.yahoo.com/") },
            new object[] { NavigateIfNew.ToUrl("https://wwww.google.com/"), QuitWebDriver.ForBrowser() },
            new object[] { NavigateIfNew.ToUrl("https://wwww.google.com/"), NavigateIfNew.ToUrl("https://wwww.yahoo.com/") },
            new object[] { NavigateIfNew.ToUrl("https://wwww.google.com/"), NavigateIfNew.ToUrl("https://wwww.google.com/", ifNot: new Regex(@"google")) },
            new object[] { NavigateIfNew.ToUrl("https://wwww.google.com/"), NavigateIfNew.ToUrl("https://wwww.google.com/").AndAcceptAlerts(false) },
            new object[] { QuitWebDriver.ForBrowser(), AcceptAlert.IfItExists() },
            new object[] { Refresh.Browser(), QuitWebDriver.ForBrowser() },
            new object[] { ScrollContainer.Reset(LocatorA), QuitWebDriver.ForBrowser() },
            new object[] { ScrollContainer.Reset(LocatorA), ScrollContainer.Reset(LocatorB) },
            new object[] { ScrollContainer.ToTop(LocatorA, 100), ScrollContainer.ToTop(LocatorB, 100) },
            new object[] { ScrollContainer.ToTop(LocatorA, 100), ScrollContainer.ToTop(LocatorA1, 200) },
            new object[] { ScrollContainer.ToLeft(LocatorA, 100), ScrollContainer.ToLeft(LocatorB, 100) },
            new object[] { ScrollContainer.ToLeft(LocatorA, 100), ScrollContainer.ToLeft(LocatorA1, 200) },
            new object[] { ScrollContainer.To(LocatorA, 100, 200), ScrollContainer.To(LocatorB, 100, 200) },
            new object[] { ScrollContainer.To(LocatorA, 100, 200), ScrollContainer.To(LocatorA1, 300, 200) },
            new object[] { ScrollContainer.To(LocatorA, 100, 200), ScrollContainer.To(LocatorA1, 100, 300) },
            new object[] { ScrollToElement.At(LocatorA), QuitWebDriver.ForBrowser() },
            new object[] { ScrollToElement.At(LocatorA), ScrollToElement.At(LocatorB) },
            new object[] { ScrollToElement.At(LocatorA), ScrollToElement.At(LocatorA1, alignToTop: false) },
            new object[] { ScrollToElement.At(LocatorA, alignToTop: false), ScrollToElement.At(LocatorB, alignToTop: false) },
            new object[] { Select.ByIndex(LocatorA, 0), QuitWebDriver.ForBrowser() },
            new object[] { Select.ByIndex(LocatorA, 0), Select.ByIndex(LocatorB, 0) },
            new object[] { Select.ByIndex(LocatorA, 0), Select.ByIndex(LocatorA1, 1) },
            new object[] { Select.ByText(LocatorA, "this"), Select.ByText(LocatorB, "this") },
            new object[] { Select.ByText(LocatorA, "this"), Select.ByText(LocatorA1, "that") },
            new object[] { Select.ByText(LocatorA, "this"), Select.ByText(LocatorA, "this", partialMatch: true) },
            new object[] { Select.ByText(LocatorA, "this", partialMatch: true), Select.ByText(LocatorA1, "that", partialMatch: true) },
            new object[] { Select.ByText(LocatorA, "this", partialMatch: true), Select.ByText(LocatorB, "this", partialMatch: true) },
            new object[] { Select.ByValue(LocatorA, "this"), Select.ByValue(LocatorB, "this") },
            new object[] { Select.ByValue(LocatorA, "this"), Select.ByValue(LocatorA1, "that") },
            new object[] { SendKeys.To(LocatorA, "hello"), QuitWebDriver.ForBrowser() },
            new object[] { SendKeys.To(LocatorA, "hello"), SendKeys.To(LocatorB, "hello") },
            new object[] { SendKeys.To(LocatorA, "hello"), SendKeys.To(LocatorA1, "goodbye") },
            new object[] { SendKeys.To(LocatorA, "hello"), SendKeys.To(LocatorA1, "hello").Privately() },
            new object[] { SendKeys.To(LocatorA, "hello"), SendKeys.To(LocatorA1, "hello").ThenClick(LocatorB) },
            new object[] { SendKeys.To(LocatorA, "hello"), SendKeys.To(LocatorA1, "hello").ThenHitEnter() },
            new object[] { SendKeys.To(LocatorA, "hello"), SendKeys.To(LocatorA1, "hello").UsingClearMethod() },
            new object[] { SendKeys.To(LocatorA, "hello"), SendKeys.To(LocatorA1, "hello").WithoutClearing() },
            new object[] { Submit.On(LocatorA), QuitWebDriver.ForBrowser() },
            new object[] { Submit.On(LocatorA), Submit.On(LocatorB) },
            new object[] { SwitchWindow.To("window3"), QuitWebDriver.ForBrowser() },
            new object[] { SwitchWindow.To("window3"), SwitchWindow.To("window2") },
            new object[] { SwitchWindowToLatest.InBrowser(), QuitWebDriver.ForBrowser() },
            new object[] { WaitAndRefresh.For(LocatorA), QuitWebDriver.ForBrowser() },
            new object[] { WaitAndRefresh.For(LocatorA), WaitAndRefresh.For(LocatorB) },
            new object[] { WaitAndRefresh.For(LocatorA), WaitAndRefresh.For(LocatorA1).ForUpTo(10) },
            new object[] { WaitAndRefresh.For(LocatorA).ForUpTo(10), WaitAndRefresh.For(LocatorB).ForUpTo(10) },
            new object[] { WaitAndRefresh.For(LocatorA), WaitAndRefresh.For(LocatorA1).ForAnAdditional(10) },
            new object[] { WaitAndRefresh.For(LocatorA).ForAnAdditional(10), WaitAndRefresh.For(LocatorB).ForAnAdditional(10) },
            new object[] { WaitAndRefresh.For(LocatorA), WaitAndRefresh.For(LocatorA1).RefreshWaiting(10) },
            new object[] { WaitAndRefresh.For(LocatorA).RefreshWaiting(10), WaitAndRefresh.For(LocatorB).RefreshWaiting(10) },
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
