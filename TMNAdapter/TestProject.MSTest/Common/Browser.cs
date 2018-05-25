using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject.Common
{
    public class Browser
    {
        private static Browser currentInstance;

        public static int ImplWait = 30;
        public static double TimeoutForElement = 30;
        private static string browser = "Chrome";

        private static IWebDriver Driver { get; set; }

        private Browser()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(ImplWait);
        }
        
       public static Browser Instance
        {
            get
            {
                return currentInstance = currentInstance ?? (currentInstance = new Browser());
            }
        }

        public IWebDriver GetDriver()
        {
            return Driver;
        }

        public IWebElement FindElement(By locator)
        {
            IWebElement currentElement = null;

            currentElement = Driver.FindElement(locator);
            return currentElement;
        }
        
        /////// Waiters ///////
        public void WaitForPageLoaded()
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(Browser.TimeoutForElement))
                .Until((driver) => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void WaitForElementIsVisible(By locator)
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(Browser.TimeoutForElement))
                .Until(ExpectedConditions.ElementIsVisible(locator));
        }
        ///////////////////////
       
        public void WindowMaximize()
        {
            Driver.Manage().Window.Maximize();
        }

        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
            this.WaitForPageLoaded();
        }
        
        public void Quit()
        {
            Driver.Quit();
            currentInstance = null;
            Driver = null;
            browser = null;
        }
    }
}