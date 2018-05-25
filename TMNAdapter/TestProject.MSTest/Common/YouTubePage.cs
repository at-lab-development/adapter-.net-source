using OpenQA.Selenium;

namespace TestProject.MSTest.Common
{
    public class YouTubePage
    {
        private static readonly By videoTitleLocator = By.ClassName("title");
        private static readonly By authorNameLocator = By.XPath("//*[@id = 'owner-name']//*");
        
        protected Browser CurrentBrowser;

        public YouTubePage(string url)
        {
            CurrentBrowser = Browser.Instance;
            CurrentBrowser.WindowMaximize();
            CurrentBrowser.NavigateTo(url);
            Browser.Instance.WaitForPageLoaded();
        }

        public string GetVideoTitle()
        {
            Browser.Instance.WaitForElementIsVisible(videoTitleLocator);
            return Browser.Instance.FindElement(videoTitleLocator).Text;
        }
        public string GetAuthorName()
        {
            Browser.Instance.WaitForElementIsVisible(authorNameLocator);
            return Browser.Instance.FindElement(authorNameLocator).Text;
        }
    }
}