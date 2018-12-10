using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace TestProject.SpecFlow.Common
{
    public class YouTubePage
    {
        private static readonly By videoTitleLocator = By.ClassName("title");
        private static readonly By authorNameLocator = By.XPath("//*[@id = 'owner-name']//*");

        public By Locator { get; private set; }

        protected Browser browser;

        public YouTubePage(Browser browser)
        {
            this.browser = browser;
        }

        public string GetVideoTitle()
        {
            browser.WaitForElementIsVisible(videoTitleLocator);
            return browser.FindElement(videoTitleLocator).Text;
        }
        public string GetAuthorName()
        {
            browser.WaitForElementIsVisible(authorNameLocator);
            return browser.FindElement(authorNameLocator).Text;
        }
    }
}