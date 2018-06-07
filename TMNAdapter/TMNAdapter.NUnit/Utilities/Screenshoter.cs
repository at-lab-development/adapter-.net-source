using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Tracking.Attributes;
using TMNAdapter.Core.Utilities;
using TMNAdapter.Tracking.Attributes;

namespace TMNAdapter.Utilities
{
    public class Screenshoter : BaseScreenshoter
    {        
        public static void TakeScreenshot()
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

            Type classType = Type.GetType(TestContext.CurrentContext.Test.ClassName);
            string issueKey = AnnotationTracker.GetAttributeInCallStack<JiraIssueKeyAttribute>()?.Key;
            IssueManager.AddIssue(new IssueModel()
            {
                Key = issueKey,
                AttachmentFilePaths = new List<string>() { relativeScreenshotPath }
            });
        }
    }
}
