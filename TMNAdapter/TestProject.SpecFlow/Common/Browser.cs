using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject.SpecFlow.Common
{
    public class Browser
    {
        private IWebDriver _driver;
        public BrowserFactory.BrowserType CurrentBrowser;
        public static int ImplWait;
        public static double TimeoutForElement;
        private static string browserName;

        public WebDriverWait Wait => new WebDriverWait(_driver, TimeSpan.FromSeconds(TimeoutForElement));

        public Browser()
        {
            ImplWait = Convert.ToInt32(Configuration.ElementTimeout);
            TimeoutForElement = Convert.ToDouble(Configuration.ElementTimeout);
            browserName = Configuration.Browser;
            Enum.TryParse(browserName, out CurrentBrowser);
            _driver = BrowserFactory.GetDriver(CurrentBrowser, ImplWait);
        }

        public IWebDriver Driver => _driver;

        public string Url
        {
            get { return _driver.Url; }
            set { _driver.Url = value; }
        }

        public string GetFormTitle => _driver.Title;

        public void Maximise() => _driver.Manage().Window.Maximize();

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void Quit()
        {
            _driver.Quit();
            _driver = null;
        }

        public IWebElement FindElement(By locator)
        {
            try
            {
                return (new WebDriverWait(_driver, TimeSpan.FromSeconds(TimeoutForElement)))
                    .Until(driver =>
                    {
                        return driver.FindElement(locator);
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            try
            {
                return (new WebDriverWait(_driver, TimeSpan.FromSeconds(TimeoutForElement)))
                    .Until(driver =>
                    {
                        return driver.FindElements(locator);
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

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
    }
}