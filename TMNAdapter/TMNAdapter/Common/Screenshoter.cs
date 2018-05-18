using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

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

        public static string TakeScreenshot()
        {
            if (!IsInitialized()) return null;

            if (!(driverInstance is RemoteWebDriver))
            {
                Console.WriteLine("Unsupported driver type: " + driverInstance.GetType());
                return null;
            }

            var data = DateTime.UtcNow.ToString().Replace(":", "-").Replace("/", ".");
            string screenshotName = data + ".Jpeg";
            string fullScreenshotPath = TestContext.CurrentContext.WorkDirectory + FileUtils.GetAttachmentsDir() + "\\" + screenshotName;
            var screenshot = ((ITakesScreenshot)driverInstance).GetScreenshot();
            screenshot.SaveAsFile(fullScreenshotPath, ScreenshotImageFormat.Jpeg);
            FileInfo screen = new FileInfo(fullScreenshotPath);

            JiraInfoProvider.SaveAttachment(screen);


            return fullScreenshotPath != null ? screenshotName : null;
        }
    }
}