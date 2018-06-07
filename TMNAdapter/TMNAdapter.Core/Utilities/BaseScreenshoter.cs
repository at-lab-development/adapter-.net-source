using OpenQA.Selenium;

namespace TMNAdapter.Core.Utilities
{
    public class BaseScreenshoter
    {
        protected static IWebDriver driverInstance;

        public static void Initialize(IWebDriver driver)
        {
            driverInstance = driver;
        }

        public static bool IsInitialized()
        {
            return driverInstance != null;
        }
    }
}
