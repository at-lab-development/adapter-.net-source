using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace TMNAdapter.Common
{
    public class Screenshoter
    {
        private static readonly string SCREENSHOT_FILE = "scr_%s.png";
        private static IWebDriver driverInstance;

        public static void Initialize(IWebDriver driver)
        {
            driverInstance = driver;
        }

        public static bool IsInitialized()
        {
            return driverInstance != null;
        }

        public static String TakeScreenshot()
        {
            if (!IsInitialized()) return null;

            if (!(driverInstance is RemoteWebDriver))
            {
                Console.WriteLine("Unsupported driver type: " + driverInstance.GetType());
                return null;
            }

            var screenshot = ((ITakesScreenshot) driverInstance).GetScreenshot();
            string screenshotName = String.Format(SCREENSHOT_FILE, DateTime.UtcNow.ToString().Replace(":", "-"));
            String filePath = "";
           //    String filePath = FileUtils.saveFile(screenshot, screenshotName);

            return filePath != null ? screenshotName : null;
        }
    }
}