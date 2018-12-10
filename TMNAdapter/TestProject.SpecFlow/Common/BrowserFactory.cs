using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace TestProject.SpecFlow.Common
{
    public class BrowserFactory
    {
        public enum BrowserType
        {
            Chrome,
            Firefox,
            MicrosoftEdge
        }

        public static IWebDriver GetDriver(BrowserType type, int timeOutSec)
        {
            IWebDriver driver = null;

            switch (type)
            {
                case BrowserType.Chrome:
                    {
                        driver = new ChromeDriver();
                        break;
                    }
                case BrowserType.Firefox:
                    {
                        driver = new FirefoxDriver();
                        break;
                    }
                case BrowserType.MicrosoftEdge:
                    {
                        driver = new EdgeDriver();
                        break;
                    }
            }
            return driver;
        }
    }
}