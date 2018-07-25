using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Remote;
using TMNAdapter.Core.Common;
using System.IO;

namespace TMNAdapter.Core.Utilities
{
    public abstract class BaseScreenshoter
    {
        protected static IWebDriver driverInstance;

        public void Initialize(IWebDriver driver)
        {
            driverInstance = driver;
        }

        public bool IsInitialized()
        {
            return driverInstance != null;
        }

        public void TakeScreenshot()
        {
            if (!IsInitialized()) return;

            if (!(driverInstance is RemoteWebDriver))
            {
                Console.WriteLine("Unsupported driver type: " + driverInstance.GetType());
                return;
            }

            string screenshotName = $"screenshot_{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fff}.jpeg";
            string relativeScreenshotPath = Constants.ATTACHMENTS_DIR + "\\" + screenshotName;
            string fullScreenshotPath = FileUtils.Solution_dir + relativeScreenshotPath;

            FileUtils.CheckOrCreateDir(Path.GetDirectoryName(fullScreenshotPath));

            var screenshot = ((ITakesScreenshot)driverInstance).GetScreenshot();
            screenshot.SaveAsFile(fullScreenshotPath, ScreenshotImageFormat.Jpeg);

            string issueKey = GetIssue();

            IssueManager.SetAttachments(issueKey, relativeScreenshotPath);
        }

        public abstract void CloseScreenshoter();

        protected abstract string GetIssue();
    }
}