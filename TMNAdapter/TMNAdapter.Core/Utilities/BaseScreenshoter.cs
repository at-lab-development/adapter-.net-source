using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;

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
            string relativeScreenshotPath = FileUtils.GetAttachmentsDir() + "\\" + screenshotName;
            string fullScreenshotPath = FileUtils.Solution_dir + relativeScreenshotPath;
            var screenshot = ((ITakesScreenshot)driverInstance).GetScreenshot();
            screenshot.SaveAsFile(fullScreenshotPath, ScreenshotImageFormat.Jpeg);

            string issueKey = GetIssue();
            IssueManager.SetAttachments(issueKey, relativeScreenshotPath);
        }

        public abstract void CloseScreenshoter();

        protected abstract string GetIssue();
    }
}
